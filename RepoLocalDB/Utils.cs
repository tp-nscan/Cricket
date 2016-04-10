using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using TT;

namespace RepoLocalDB
{
    public static class Utils
    {

        #region string to Base64

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64ToString(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion


        #region int[] to Base64

        public static string Base64Encode(this int[] arr)
        {
            return Convert.ToBase64String(arr.ToByteArray());
        }

        public static int[] Base64ToInts(this string base64EncodedData)
        {
            return Convert.FromBase64String(base64EncodedData).ToIntArray();
        }

        public static byte[] ToByteArray(this int[] arr)
        {
            var bytes = new byte[arr.Length * 4];
            for (var ctr = 0; ctr < arr.Length; ctr++)
            {
                Array.Copy(BitConverter.GetBytes(arr[ctr]), 0,
                           bytes, ctr * 4, 4);
            }
            return bytes;
        }

        public static int[] ToIntArray(this byte[] arr)
        {
            var newArr = new int[arr.Length / 4];
            for (var ctr = 0; ctr < arr.Length / 4; ctr++)
                newArr[ctr] = BitConverter.ToInt32(arr, ctr * 4);
            return newArr;
        }

        #endregion


        #region float[] to Base64

        public static string Base64Encode(this float[] arr)
        {
            return Convert.ToBase64String(arr.ToByteArray());
        }

        public static float[] Base64ToFloats(this string base64EncodedData)
        {
            return Convert.FromBase64String(base64EncodedData).TofloatArray();
        }

        public static byte[] ToByteArray(this float[] arr)
        {
            var bytes = new byte[arr.Length * 4];
            for (var ctr = 0; ctr < arr.Length; ctr++)
            {
                Array.Copy(BitConverter.GetBytes(arr[ctr]), 0,
                           bytes, ctr * 4, 4);
            }
            return bytes;
        }

        public static float[] TofloatArray(this byte[] arr)
        {
            var newArr = new float[arr.Length / 4];
            for (var ctr = 0; ctr < arr.Length / 4; ctr++)
                newArr[ctr] = BitConverter.ToSingle(arr, ctr * 4);
            return newArr;
        }

        #endregion


        public static bool IsSameAs<T>(this IEnumerable<T> ableA, IEnumerable<T> ableB)
        {
            var atorA = ableA.ToList();
            var atorB = ableB.ToList();
            if (atorA.Count != atorB.Count)
            {
                return false;
            }
            return !atorA.Where((t, i) => !t.Equals(atorB[i])).Any();
        }

        public static bool IsSameAs(this int[,] ableA, int[,] ableB)
        {
            if(ableA.GetLength(0) != ableB.GetLength(0)) return false;
            if(ableA.GetLength(1) != ableB.GetLength(1)) return false;

            for(var r = 0; r< ableA.GetLength(0); r++)
                for(var c=0; c< ableA.GetLength(1); c++)
                    if (ableA[r, c] != ableB[r, c]) return false;

            return true;
        }

        public static bool IsSameAs(this float[,] ableA, float[,] ableB)
        {
            if (ableA.GetLength(0) != ableB.GetLength(0)) return false;
            if (ableA.GetLength(1) != ableB.GetLength(1)) return false;

            for (var r = 0; r < ableA.GetLength(0); r++)
                for (var c = 0; c < ableA.GetLength(1); c++)
                    if (Math.Abs(ableA[r, c] - ableB[r, c]) > NumUt.Epsilon) return false;

            return true;
        }

        public static bool IsSameAs(this Vector<int> ableA, Vector<int> ableB)
        {
            if (ableA.Count() != ableB.Count()) return false;

            for (var r = 0; r < ableA.Count(); r++)
                    if (ableA[r] != ableB[r]) return false;

            return true;
        }


        public static bool IsSameAs(this Vector<float> ableA, Vector<float> ableB)
        {
            if (ableA.Count() != ableB.Count()) return false;

            for (var r = 0; r < ableA.Count(); r++)
                if (Math.Abs(ableA[r] - ableB[r]) > NumUt.Epsilon) return false;

            return true;
        }


        public static bool IsSameAs(this Matrix<int> ableA, Matrix<int> ableB)
        {
            if (ableA.RowCount != ableB.RowCount) return false;
            if (ableA.ColumnCount != ableB.ColumnCount) return false;

            for (var r = 0; r < ableA.RowCount; r++)
                for (var c = 0; c < ableA.ColumnCount; c++)
                    if (ableA[r,c] != ableB[r,c]) return false;

            return true;
        }


        public static bool IsSameAs(this Matrix<float> ableA, Matrix<float> ableB)
        {
            if (ableA.RowCount != ableB.RowCount) return false;
            if (ableA.ColumnCount != ableB.ColumnCount) return false;

            for (var r = 0; r < ableA.RowCount; r++)
                for (var c = 0; c < ableA.ColumnCount; c++)
                    if (Math.Abs(ableA[r,c] - ableB[r,c]) > NumUt.Epsilon) return false;

            return true;
        }
    }
}