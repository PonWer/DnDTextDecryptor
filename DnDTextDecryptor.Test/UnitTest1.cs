using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDTextDecryptor.Test
{
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow('A', 0)]
        [DataRow('M', 12)]
        [DataRow('Z', 25)]
        [DataRow('Å', 26)]
        [DataRow('Ä', 27)]
        [DataRow('Ö', 28)]
        public void CharToIntTest(char inChar, int inExpected)
        {
            Assert.AreEqual(inExpected, Program.CharToInt(inChar));
        }

        [DataTestMethod]
        [DataRow(0, 'A')]
        [DataRow(29, 'A')]
        [DataRow( 12,'M')]
        [DataRow( 25,'Z')]
        [DataRow( 26,'Å')]
        [DataRow( 27,'Ä')]
        [DataRow(28, 'Ö')]
        [DataRow(57, 'Ö')]
        public void IntToCharTest(int inInt, char inExpected)
        {
            Assert.AreEqual(inExpected, Program.IntToChar(inInt));
        }
    }
}
