namespace TT
open System
open MathNet.Numerics
open MathNet.Numerics.Distributions
open MathNet.Numerics.Random
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Matrix

module BTconv =

    type ValBound =
        | N       //[minVal, maxVal]
        | U       //[0, maxVal]
        | B       //[-1, 1]
        | UB      //[0, 1]

    type NumberType =
        | Int
        | F32

    
    type ArrayShape =
        | D1
        | D2
        | D4
        | S1
        | S2
        | S4


    let Base64ToIntA strData =
        let bytes = Convert.FromBase64String strData
        Array.init (bytes.Length / 4) (fun x  -> BitConverter.ToInt32(bytes, x * 4))


    let Base64ToF32A strData =
        let bytes = Convert.FromBase64String strData
        Array.init (bytes.Length / 4) (fun x  -> BitConverter.ToSingle(bytes, x * 4))


    let Base64ToIntA2 (bounds:Sz2<int>) strData =
        let bytes = Convert.FromBase64String strData
        Array2D.init (bounds.Y) 
                     (bounds.X) 
                     (fun x y -> BitConverter.ToInt32(bytes, (x * bounds.X + y) * 4))


    let Base64ToF32A2 (bounds:Sz2<int>) strData =
        let bytes = Convert.FromBase64String strData
        Array2D.init (bounds.Y) 
                     (bounds.X) 
                     (fun x y -> BitConverter.ToSingle(bytes, (x * bounds.X + y) * 4))


    let IntAtoBase64 (intA:int[]) =
        Seq.init (intA.GetLength(0)) (fun i -> BitConverter.GetBytes(intA.[i]))
        |> Seq.concat 
        |> Seq.toArray
        |> Convert.ToBase64String


    let F32AtoBase64 (f32A:float32[]) =
        Seq.init (f32A.GetLength(0)) (fun i -> BitConverter.GetBytes(f32A.[i]))
        |> Seq.concat 
        |> Seq.toArray
        |> Convert.ToBase64String


    let IntA2toBase64 (intA:int[,]) =
        intA |> A2dUt.flattenRowMajor
        |> Seq.map (fun i -> BitConverter.GetBytes(i))
        |> Seq.concat 
        |> Seq.toArray
        |> Convert.ToBase64String


    let F32A2toBase64 (f32A:float32[,]) =
        f32A |> A2dUt.flattenRowMajor
        |> Seq.map (fun i -> BitConverter.GetBytes(i))
        |> Seq.concat 
        |> Seq.toArray
        |> Convert.ToBase64String





module MathNetConv =

    let Base64ToDenseIntVector length strData =
        let bytes = Convert.FromBase64String strData
        DenseVector.init length (fun x  -> BitConverter.ToInt32(bytes, x * 4))


    let Base64ToDenseF32Vector length strData =
        let bytes = Convert.FromBase64String strData
        DenseVector.init length (fun x  -> BitConverter.ToSingle(bytes, x * 4))


    let Base64ToDenseIntMatrix (bounds:Sz2<int>) strData =
        let bytes = Convert.FromBase64String strData
        DenseMatrix.init bounds.Y bounds.X (fun r c  -> 
            BitConverter.ToInt32(bytes, (r * bounds.X + c ) * 4))


    let Base64ToDenseF32Matrix (bounds:Sz2<int>) strData =
        let bytes = Convert.FromBase64String strData
        DenseMatrix.init bounds.Y bounds.X (fun r c -> 
            BitConverter.ToSingle(bytes, (r * bounds.X + c ) * 4))


    let DenseIntVectorToBase64 (intV:Vector<int>) =
        Seq.init (intV.Count) (fun i -> BitConverter.GetBytes(intV.[i]))
        |> Seq.concat 
        |> Seq.toArray
        |> Convert.ToBase64String


    let DenseF32VectorToBase64 (f32V:Vector<float32>) =
        Seq.init (f32V.Count) (fun i -> BitConverter.GetBytes(f32V.[i]))
        |> Seq.concat 
        |> Seq.toArray
        |> Convert.ToBase64String


    let DenseIntMatrixtoBase64 (intM:Matrix<int>) =
        intM |> MatrixUt.ToRowMajorSequence
        |> Seq.map (fun i -> BitConverter.GetBytes(i))
        |> Seq.concat 
        |> Seq.toArray
        |> Convert.ToBase64String


    let DenseF32MatrixtoBase64 (intM:Matrix<float32>) =
        intM |> MatrixUt.ToRowMajorSequence
        |> Seq.map (fun i -> BitConverter.GetBytes(i))
        |> Seq.concat 
        |> Seq.toArray
        |> Convert.ToBase64String