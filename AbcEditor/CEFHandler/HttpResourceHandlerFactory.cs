using AbcEditor.TypeShoot;
using CefSharp;
using SwfDec;
using SwfDec.AVM2.ByteCode;
using SwfDec.AVM2.ByteCode.Instructions.Push;
using SwfDec.AVM2.Types;
using SwfDec.AVM2.Types.Traits;
using System.IO;

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
                                    var words = new Words(constantPool);
                                    words.Constructor(instructions);
                                    initializer.MethodBody.Code = byteCode.GetBytes();
                                }
                            }
                        }
                    }

                    var compileSwf = swf.Compile();

                    // File.WriteAllBytes("-TypeShoot.swf", compileSwf);

                    return compileSwf;
                });
            }
            return null;
        }
    }
}
