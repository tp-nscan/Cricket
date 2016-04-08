using System;
using TT;

namespace RepoLocalDB
{
    public enum ArrayType
    {
        Dense1DIntN,
        Dense1DIntU,
        Dense1DIntB,
        Dense1DIntUb,
        Dense1dF32N,
        Dense1dF32U,
        Dense1dF32B,
        Dense1dF32Ub,
        Sparse1DIntN,
        Sparse1DIntU,
        Sparse1DIntB,
        Sparse1DIntUb,
        Sparse1dF32N,
        Sparse1dF32U,
        Sparse1dF32B,
        Sparse1dF32Ub,
    }

    public class ArrayRecord
    {
        public ArrayType ArrayType { get; set; }

        public int Dim1 { get; set; }
        public int Dim2 { get; set; }
        public int Dim3 { get; set; }
        public int Dim4 { get; set; }

        public string Coords1 { get; set; }
        public string Coords2 { get; set; }
        public string Coords3 { get; set; }
        public string Coords4 { get; set; }

        public string Values { get; set; }
    }



    public static class ArConv
    {
        public static Data.ValBnd ToValBnd(this ArrayType art)
        {
            switch (art)
            {
                case ArrayType.Dense1DIntN:
                    return Data.ValBnd.N;
                case ArrayType.Dense1DIntU:
                    return Data.ValBnd.U;
                case ArrayType.Dense1DIntB:
                    return Data.ValBnd.B;
                case ArrayType.Dense1DIntUb:
                    return Data.ValBnd.UB;
                case ArrayType.Dense1dF32N:
                    return Data.ValBnd.N;
                case ArrayType.Dense1dF32U:
                    return Data.ValBnd.U;
                case ArrayType.Dense1dF32B:
                    return Data.ValBnd.B;
                case ArrayType.Dense1dF32Ub:
                    return Data.ValBnd.UB;
                case ArrayType.Sparse1DIntN:
                    return Data.ValBnd.N;
                case ArrayType.Sparse1DIntU:
                    return Data.ValBnd.U;
                case ArrayType.Sparse1DIntB:
                    return Data.ValBnd.B;
                case ArrayType.Sparse1DIntUb:
                    return Data.ValBnd.UB;
                case ArrayType.Sparse1dF32N:
                    return Data.ValBnd.N;
                case ArrayType.Sparse1dF32U:
                    return Data.ValBnd.U;
                case ArrayType.Sparse1dF32B:
                    return Data.ValBnd.B;
                case ArrayType.Sparse1dF32Ub:
                    return Data.ValBnd.UB;
                default:
                    throw new Exception("unhandled ArrayType");
            }
        }

        public static Data.GenArray ToGenArray(this ArrayRecord ar)
        {
            switch (ar.ArrayType)
            {
                case ArrayType.Dense1DIntN:
                    return Data.MakeIntDense1D(ar.ArrayType.ToValBnd(), 1, new int[] {});
                case ArrayType.Dense1DIntU:
                    return null;
                case ArrayType.Dense1DIntB:
                    return null;
                case ArrayType.Dense1DIntUb:
                    return null;
                case ArrayType.Dense1dF32N:
                    return null;
                case ArrayType.Dense1dF32U:
                    return null;
                case ArrayType.Dense1dF32B:
                    return null;
                case ArrayType.Dense1dF32Ub:
                    return null;
                case ArrayType.Sparse1DIntN:
                    return null;
                case ArrayType.Sparse1DIntU:
                    return null;
                case ArrayType.Sparse1DIntB:
                    return null;
                case ArrayType.Sparse1DIntUb:
                    return null;
                case ArrayType.Sparse1dF32N:
                    return null;
                case ArrayType.Sparse1dF32U:
                    return null;
                case ArrayType.Sparse1dF32B:
                    return null;
                case ArrayType.Sparse1dF32Ub:
                    return null;
                default:
                    throw new Exception("unhandled ArrayType");
            }
        }
    }
}