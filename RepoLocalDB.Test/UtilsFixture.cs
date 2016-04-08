using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepoLocalDB.Test
{
    [TestClass]
    public class UtilsFixture
    {
        [TestMethod]
        public void TestIntToBytes()
        {
            var intsIn = Enumerable.Range(0, 10).ToArray();
            var bytes = intsIn.ToByteArray();
            var intsOut = bytes.ToIntArray();
            Assert.IsTrue(intsIn.IsSameAs(intsOut));
        }

        [TestMethod]
        public void TestIntToBase64()
        {
            var intsIn = Enumerable.Range(0, 10).ToArray();
            var base64 = intsIn.Base64Encode();
            var intsOut = base64.Base64ToInts();
            Assert.IsTrue(intsIn.IsSameAs(intsOut));
        }

        [TestMethod]
        public void TestFloatToBase64()
        {
            var floatsIn = Enumerable.Range(0, 10).Select(t=>(float)t * 1.778f).ToArray();
            var base64 = floatsIn.Base64Encode();
            var floatsOut = base64.Base64ToFloats();
            Assert.IsTrue(floatsIn.IsSameAs(floatsOut));
        }
    }
}
