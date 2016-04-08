using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}