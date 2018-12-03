using CefSharp;
using SwfDec;
using SwfDec.AVM2.ByteCode;
using SwfDec.AVM2.ByteCode.Instructions.Push;
using SwfDec.AVM2.Types;
using SwfDec.AVM2.Types.Traits;

namespace AbcEditor.CEFHandler
{
    public class HttpResourceHandlerFactory : IResourceHandlerFactory
    {
        bool IResourceHandlerFactory.HasHandlers
        {
            get { return true; }
        }

        IResourceHandler IResourceHandlerFactory.GetResourceHandler(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request)
        {
            // Debug.WriteLine(request.Url);

            if (request.Url.Contains("TypeShoot.swf"))
            {
                return new SwfResourceHandler(raw =>
                {
                    var swf = new Swf();

                    // File.WriteAllBytes("TypeShoot.swf", raw);

                    swf.Decompile(raw);

                    foreach (var doAbcTag in swf.DoAbcTagList)
                    {
                        var abc = doAbcTag.AbcData;
                        var constantPool = abc.ConstantPool;

                        foreach (var scriptArray in abc.ScriptArray)
                        {
                            foreach (var traits in scriptArray.TraitsArray)
                            {
                                // Words Class
                                if (traits.Name.MKQName.Name == "Words")
                                {
                                    var initializer = ((TraitClass)traits.Trait).Class.StaticInitializer;
                                    var bin = initializer.MethodBody.Code;
                                    var byteCode = new ByteCode(bin, abc);
                                    var instructions = byteCode.Instructions;

                                    var stringArray = constantPool.StringArrayLength;
                                    var stringInfo = new StringInfo(
                                        stringArray, "github.com/BiasHacker");
                                    constantPool.SetStringAt(stringInfo, stringArray);

                                    for (var count = 0; count < instructions.Count; count++)
                                    {
                                        if (instructions[count] is As3PushString str)
                                            instructions[count] = new As3PushString(stringInfo);
                                    }

                                    initializer.MethodBody.Code = byteCode.GetBytes();
                                }
                            }
                        }
                    }

                    return swf.Compile();
                });
            }
            return null;
        }
    }
}
