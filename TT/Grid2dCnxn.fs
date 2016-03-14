namespace TT
open System
open MathNet.Numerics
open MathNet.Numerics.Random
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Matrix
open MathNetUt

module Grid2dCnxn =
    
    type CnxPattern =
        | Star
        | Ring
        | Block of int
        | Disk of int

 
    let StarNbrsF<'T> fV = 
        [| 
            { P2V.X =  0; Y = -1; V = fV  0 -1};
            { P2V.X = -1; Y =  0; V = fV -1  0}; 
            { P2V.X =  0; Y =  1; V = fV  0  1};
            { P2V.X = -1; Y =  0; V = fV -1  0}
        |]


    let RingNbrsF<'T> fV = 
        [|  
            { P2V.X = -1; Y = -1; V = fV -1 -1};
            { P2V.X =  0; Y = -1; V = fV  0 -1};
            { P2V.X =  1; Y = -1; V = fV  1 -1};

            { P2V.X = -1; Y =  0; V = fV -1  0};
            { P2V.X =  1; Y =  0; V = fV  1  0};

            { P2V.X = -1; Y =  1; V = fV -1  1};
            { P2V.X =  0; Y =  1; V = fV  0  1};
            { P2V.X =  1; Y =  1; V = fV  1  1};
        |]


    let BlockNeighbors (radius:int) fV =
        [|
            for row in -radius .. radius do
                    for col in -radius .. radius do
                        yield { P2V.X = col; Y=row; V= fV col row }
        |]


    let DiskNbrs (radius:int) fV =
        let rFloat = (float radius)
        [|
            for row in -radius .. radius do
                    let rowsq = float ( row*row )
                    for col in -radius .. radius do
                        let colsq =  float ( col*col )
                        let dsq = Math.Sqrt(colsq + rowsq)
                        if (dsq < rFloat + 0.415) then
                            yield { P2V.X = col; Y=row; V= fV col row }
        |]


    let GetZ4VsForNodeF (offsets: (int->int->float32) -> P2V<int,float32>[]) 
                        (valuator:int->int->float32) 
                        (stride:int) (x:int) (y:int) =
        (offsets valuator) |> Array.map( 
            fun p -> { 
                      M2x2V.X1 = x; 
                            Y1 = y; 
                            X2 = (p.X + x + stride) % stride; 
                            Y2 = (p.Y + y + stride) % stride;
                            V = p.V
                     } 
            )


//    let GetAllD4s (offsets: V2<float32>[]) (stride:int) =
//        (Array2dEx.D2sOfArray2D stride stride |> Seq.cast<Z2<int>>)
//              |> Seq.map(fun c-> (GetZ4VsForNode offsets stride c.X c.Y) |> Array.toSeq)
//              |> Seq.concat
 

//    let MkArray =
//        [| 1;2;3 |] |> Array.toSeq
//
//    let MakeZ4Vs (stride:int) =
//       seq {
//             for row in 0 .. stride - 1 do
//                for col in 0 .. stride - 1 do
//                    yield! MkArray
//        }


    let MatrixEntryToVectorEntry colCount (mE:int*int*float32) =
        match mE with
        | (r,c,v) -> (r*colCount + c, v)


    let Coordify stride dex =
        let modus = dex % stride
        {
            P2.X = modus;
            P2.Y = (dex - modus) / stride;
        }


    let Cnx4dToCnx2d (stride:int) (coords:seq<M2x2<int>>) =
        coords |> Seq.map(fun d4->{P2.X = d4.Y1*stride + d4.X1; 
                                      Y = d4.Y2*stride + d4.X2})


    let Z4VTo3Tuple (stride:int) (coords:seq<M2x2V<int,float32>>) =
        coords |> Seq.map(fun z4V-> 
            (z4V.Y1*stride + z4V.X1, z4V.Y2*stride + z4V.X2, z4V.V))

    
    let CnxZ4To3Tuple (stride:int) (coords:seq<M2x2V<int, float32>>) =
        coords |> Seq.map(fun z4-> 
            (z4.Y1*stride + z4.X1, z4.Y2*stride + z4.X2, z4.V))


    let Matrix3TupleToZ4 (stride:int) (tuples: seq<Tuple<int,int,float32>>) =
        tuples |> Seq.map(fun t-> 
            let ax1 = Coordify stride t.Item1
            let ax2 = Coordify stride t.Item2
            {
                M2x2V.X1  = ax1.X; 
                      X2  = ax1.Y; 
                      Y1  = ax2.X;
                      Y2  = ax2.Y;
                      V = t.Item3
             })


//    let Make3TuplesRand offsets (stride:int) (seed:int) (mean:float32) (sd:float32) =
//        let rng = Random.MersenneTwister(seed)
//        let seqVals = Generators.NormalF32 rng mean sd
//        let enumer = seqVals.GetEnumerator()
//        let fund4 x =
//            enumer.MoveNext() |> ignore
//            enumer.Current
//        CnxZ4To3Tuple stride (GetAllD4s offsets stride)


//    let Z4sForForGridWithRingNbrs (stride:int) =
//        (GetAllD4s RingNbrs stride) |> Seq.map(fun v -> 
//            {V2_2.X1 = v.X1;
//             V2_2.Y1 = v.Y1; 
//             V2_2.X2 = v.X2; 
//             V2_2.Y2 = v.Y2;
//             V2_2.Val = 1.0f})


//    let Z4sForForGridWithRingNbrs (stride:int) =
//        let rng = Random.MersenneTwister(123)
//        let seqVals = Generators.NormalF32 rng 0.0f 0.31f
//
//        ((GetAllD4s RingNbrs stride), seqVals) ||> Seq.map2(fun v r -> 
//            {V2_2.X1 = v.X1;
//             V2_2.Y1 = v.Y1; 
//             V2_2.X2 = v.X2; 
//             V2_2.Y2 = v.Y2;
//             V2_2.Val = r})


//    let Z4sForForGridWithRingNbrs (stride:int) =
//        let vals = Make3TuplesRand (RingNbrsF (ConstF 1.0f)) stride 123 10.9f 0.5f
//                        |> Seq.toList
//        let builder = Matrix<float32>.Build
//        let matrix = builder.SparseOfIndexed (stride*stride, stride*stride, vals)
//        matrix |> ToZ4Vals
//
//
//    let Z4sForForGridWithRingNbrsB (stride:int) =
//        let flats = MatrixEntryToVectorEntry (stride * stride)
//        let vals = Make3TuplesRand (RingNbrsF (fun i j -> 1.0f)) stride 123 10.9f 0.5f
//                        |> Seq.map(fun x -> flats x)
//        let builder = Vector<float32>.Build
//        let vec = builder.SparseOfIndexed (stride*stride*stride*stride, vals)
//        vec