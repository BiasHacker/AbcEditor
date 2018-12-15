using System.Collections.Generic;
using System.Linq;

#region "avm2 instructions"
using SwfDec.AVM2.ByteCode.Instructions;
using SwfDec.AVM2.ByteCode.Instructions.Create;
using SwfDec.AVM2.ByteCode.Instructions.Find;
using SwfDec.AVM2.ByteCode.Instructions.Locals;
using SwfDec.AVM2.ByteCode.Instructions.Push;
using SwfDec.AVM2.ByteCode.Instructions.Return;
using SwfDec.AVM2.ByteCode.Instructions.Set;
using SwfDec.AVM2.Types;
using SwfDec.AVM2.Types.Multinames;
#endregion

namespace AbcEditor.TypeShoot
{

    public class Words
    {
        private ConstantPoolInfo constantPool { get; set; }

        public Words(ConstantPoolInfo constantPool)
        {
            this.constantPool = constantPool;
        }

        public void Constructor(IList<As3Instruction> as3)
        {
            as3.Clear();

            var multinameArray = constantPool.GetMultinameArray();
            var multinameInfos = new MultinameInfo[] {
                multinameArray.FirstOrDefault(
                    multiname => multiname.MKQName?.Name?.String == "jKanjiList"),
                multinameArray.FirstOrDefault(
                    multiname => multiname.MKQName?.Name?.String == "jList"),
                multinameArray.FirstOrDefault(
                    multiname => multiname.MKQName?.Name?.String == "list")
            };

            as3.Add(new As3GetLocal0());
            as3.Add(new As3PushScope());

            var typingTable = new string[] {
                "さあ",
                "げーむをはじめよう"
            };

            var count = (uint)typingTable.Length;

            foreach (var multinameInfo in multinameInfos)
            {
                as3.Add(new As3FindProperty(multinameInfo));

                foreach (var typingData in typingTable)
                {
                    var stringArray = constantPool.StringArrayLength;
                    var stringInfo = new StringInfo(stringArray, typingData);
                    constantPool.SetStringAt(stringInfo, stringArray);
                    as3.Add(new As3PushString(stringInfo));
                }

                as3.Add(new As3NewArray(count));
                as3.Add(new As3InitProperty(multinameInfo));
            }

            as3.Add(new As3ReturnVoid());
        }
    }
}
