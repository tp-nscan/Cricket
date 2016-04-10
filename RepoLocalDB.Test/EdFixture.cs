using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TT;

namespace RepoLocalDB.Test
{
    [TestClass]
    public class EdFixture
    {
        [TestMethod]
        public void TestIntAToBase64()
        {
            var intsIn = Enumerable.Range(0, 10).ToArray();
            var base64 =  BTconv.IntAtoBase64(intsIn);
            var intsOut = BTconv.Base64ToIntA(base64);

            Assert.IsTrue(intsIn.IsSameAs(intsOut));
        }

        [TestMethod]
        public void TestF32AToBase64()
        {
            var f32A = Enumerable.Range(0, 10)
                    .Select(x => x + 0.77f) .ToArray();
            var base64 = BTconv.F32AtoBase64(f32A);
            var toF32A = BTconv.Base64ToF32A(base64);

            Assert.IsTrue(f32A.IsSameAs(toF32A));
        }


        [TestMethod]
        public void TestIntA2ToBase64()
        {
            var bounds = new Sz2<int>(3, 5);
            var aIn = GenA2.RandInt(bounds, 173, 123);
            var base64 = BTconv.IntA2toBase64(aIn);
            var aOut = BTconv.Base64ToIntA2(bounds, base64);

            Assert.IsTrue(aIn.IsSameAs(aOut));
        }

        [TestMethod]
        public void TestFloatA2ToBase64()
        {
            var bounds = new Sz2<int>(3, 5);
            var aIn = GenA2.RandF32(bounds, 123);
            var base64 = BTconv.F32A2toBase64(aIn);
            var aOut = BTconv.Base64ToF32A2(bounds, base64);

            Assert.IsTrue(aIn.IsSameAs(aOut));
        }

        [TestMethod]
        public void TestFloatDenseVectorToBase64()
        {
            var bound = 23;
            var aIn = GenVec.DenseF32(bound, 123);
            var base64 = MathNetConv.DenseF32VectorToBase64(aIn);
            var aOut = MathNetConv.Base64ToDenseF32Vector(bound, base64);

            Assert.IsTrue(aIn.IsSameAs(aOut));
        }


        [TestMethod]
        public void TestIntDenseMatrixToBase64()
        {
            var bounds = new Sz2<int>(3, 5);
            var aIn = GenMatrix.DenseF32(bounds, 123);
            var base64 = MathNetConv.DenseF32MatrixtoBase64(aIn);
            var aOut = MathNetConv.Base64ToDenseF32Matrix(bounds, base64);

            Assert.IsTrue(aIn.IsSameAs(aOut));
        }

    }
}
