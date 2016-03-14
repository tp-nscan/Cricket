namespace TT

type P2<'c>  = { X:'c; Y:'c }
type Sz2<'c>  = { X:'c; Y:'c }
type P3<'c>  = { X:'c; Y:'c; Z:'c }
type I<'c>  = { Min:'c; Max:'c }
type R<'c>  = { MinX:'c; MaxX:'c; MinY:'c; MaxY:'c } 
type M2x2<'c>  = { X1:'c; Y1:'c; X2:'c; Y2:'c } 

type P1V<'c,'v>  = { X:'c; V:'v }
type P2V<'c,'v>  = { X:'c; Y:'c; V:'v }
type P3V<'c,'v>  = { X:'c; Y:'c; Z:'c; V:'v }
type IV<'c,'v>  = { Min:'c; Max:'c; V:'v  } 
type RV<'c,'v>  = { MinX:'c; MaxX:'c; MinY:'c; MaxY:'c; V:'v } 
type M2x2V<'c,'v>  = { X1:'c; Y1:'c; X2:'c; Y2:'c; V:'v } 

module NumUt =

    let Epsilon = 0.00001f

    let Fraction32Of (num:int) (frac:float32) =
        System.Convert.ToInt32(((float32 num) * frac))


    let AbsF32 (v:float32) =
        if v < 0.0f then -v
        else v


    let MapUF (min:float) (max:float) (value:float) =
        let span = max-min
        min + value*span


    let inline AddInRange min max a b =
        let res = a + b
        if res < min then min
        else if res > max then max
        else res


    let inline FlipWhen a b flipProb draw current =
        match (flipProb < draw) with
        | true -> if a=current then b else a
        | false -> current


    let ToUF value =
        if value < 0.0f then 0.0f
        else if value > 1.0f then 1.0f
        else value


    let ToSF value =
        if value < -1.0f then -1.0f
        else if value > 1.0f then 1.0f
        else value


    let FloatToUF value =
        if value < 0.0 then 0.0f
        else if value > 1.0 then 1.0f
        else (float32 value)


    let FloatToSF value =
        if value < -1.0 then -1.0f
        else if value > 1.0 then 1.0f
        else (float32 value)


    let inline AorB a b thresh value =
        if value < thresh then a
        else b



module BT =

    let AntiLofS = 
        { 
            I.Min = System.Single.PositiveInfinity; 
            Max = System.Single.NegativeInfinity; 
        }

    let AntiLofInt = 
        { 
            I.Min = System.Int32.MaxValue; 
            Max = System.Int32.MinValue;
        }

    let AntiRofS = 
        { 
            R.MinX = System.Single.PositiveInfinity; 
            R.MaxX = System.Single.NegativeInfinity; 
            MinY = System.Single.PositiveInfinity;  
            MaxY = System.Single.NegativeInfinity; 
        }

    let AntiRofInt = 
        { 
            R.MinX = System.Int32.MaxValue; 
            R.MaxX = System.Int32.MinValue; 
            MinY = System.Int32.MaxValue;  
            MaxY = System.Int32.MinValue; 
        }



    let inline walk_the_creature_2 (creature:^a when ^a:(member Walk : unit -> unit)) =
        (^a : (member Walk : unit -> unit) creature)


    let inline Span (range:^a when ^a:(member Min : ^b) and ^a:(member Max : ^b)) =
       (^a : (member Max : ^b) range) - (^a : (member Min : ^b) range)


    let inline Mid (range:^a when ^a:(member Min : ^b) and ^a:(member Max : ^b)) 
                   (p1:^b) =
       ((^a : (member Max : ^b) range) + (^a : (member Min : ^b) range) ) / 2


    let inline InL (range:^a when ^a:(member Min : ^b) and ^a:(member Max : ^b)) 
                   (p1:^b) =
       ((^a : (member Max : ^b) range) >= p1) && ((^a : (member Min : ^b) range) <= p1)


    let inline Area (range:^a when 
                        ^a:(member MinX : ^b) and ^a:(member MaxX : ^b) 
                        and
                        ^a:(member MinY : ^b) and ^a:(member MaxY : ^b)
                    ) =

       ((^a : (member MaxX : ^b) range) - (^a : (member MinX : ^b) range))
       *
       ((^a : (member MaxY : ^b) range) - (^a : (member MinY : ^b) range))


    let inline SpanX (range:^a when ^a:(member MinX : ^b) and ^a:(member MaxX : ^b) ) =
       ((^a : (member MaxX : ^b) range) - (^a : (member MinX : ^b) range))


    let inline SpanY (range:^a when ^a:(member MinY : ^b) and ^a:(member MaxY : ^b) ) =
       ((^a : (member MaxY : ^b) range) - (^a : (member MinY : ^b) range))


    let inline InR (range:^a when 
                        ^a:(member MinX : ^b) and ^a:(member MaxX : ^b) 
                        and
                        ^a:(member MinY : ^b) and ^a:(member MaxY : ^b)
                    ) 
                    (p2:^c when 
                        ^c:(member X : ^b) and ^c:(member Y : ^b) 
                    ) =

       (^a : (member MaxX : ^b) range) >= (^c : (member X : ^b) p2)
       &&
       (^a : (member MinX : ^b) range) <= (^c : (member X : ^b) p2)
       &&
       (^a : (member MaxY : ^b) range) >= (^c : (member Y : ^b) p2)
       &&
       (^a : (member MinY : ^b) range) <= (^c : (member Y : ^b) p2)


    let inline StretchI (range:I< ^b>) (p1:^b ) =
        { 
            I.Min = if (range.Min > p1) then p1 else range.Min
            Max   = if (range.Max < p1) then p1 else range.Max
        }


    let inline StretchRP (box:R< ^b>) (p2:^c when ^c:(member X : ^b) and ^c:(member Y : ^b) ) =
        { 
            R.MinX = if (box.MinX > (^c : (member X : ^b) p2)) then (^c : (member X : ^b) p2) else box.MinX
            MaxX   = if (box.MaxX < (^c : (member X : ^b) p2)) then (^c : (member X : ^b) p2) else box.MaxX 
            MinY   = if (box.MinY > (^c : (member Y : ^b) p2)) then (^c : (member Y : ^b) p2) else box.MinY
            MaxY   = if (box.MaxY < (^c : (member Y : ^b) p2)) then (^c : (member Y : ^b) p2) else box.MaxY
        }


    let inline StretchRR (box:R< ^b>) 
                           (rect:^c when ^c:(member MinX : ^b) and ^c:(member MaxX : ^b) 
                                     and ^c:(member MinY : ^b) and ^c:(member MaxY : ^b)) =
        { 
            R.MinX = if (box.MinX > (^c : (member MinX : ^b) rect)) then (^c : (member MinX : ^b) rect) else box.MinX
            MaxX   = if (box.MaxX < (^c : (member MaxX : ^b) rect)) then (^c : (member MaxX : ^b) rect) else box.MaxX 
            MinY   = if (box.MinY > (^c : (member MinY : ^b) rect)) then (^c : (member MinY : ^b) rect) else box.MinY
            MaxY   = if (box.MaxY < (^c : (member MaxY : ^b) rect)) then (^c : (member MaxY : ^b) rect) else box.MaxY
        }


    let inline StretchRL (box:R< ^b>) 
                           (line:^c when ^c:(member X1 : ^b) and ^c:(member X2 : ^b) 
                                     and ^c:(member Y1 : ^b) and ^c:(member Y2 : ^b)) =
        { 
            R.MinX = if (box.MinX > (^c : (member X1 : ^b) line)) then (^c : (member X1 : ^b) line) else box.MinX
            MaxX   = if (box.MaxX < (^c : (member X2 : ^b) line)) then (^c : (member X2 : ^b) line) else box.MaxX 
            MinY   = if (box.MinY > (^c : (member Y1 : ^b) line)) then (^c : (member Y1 : ^b) line) else box.MinY
            MaxY   = if (box.MaxY < (^c : (member Y2 : ^b) line)) then (^c : (member Y2 : ^b) line) else box.MaxY
        }


    let inline BoundingBoxP (box:R< ^b>) (p2:seq< ^c> when ^c:(member X : ^b) and ^c:(member Y : ^b) ) =
            p2 |> Seq.fold (fun acc elem -> StretchRP acc elem ) box


    let inline BoundingBoxR (box:R< ^b>) 
                            (rects:seq< ^c> when ^c:(member MinX : ^b) and ^c:(member MaxX : ^b) 
                                             and ^c:(member MinY : ^b) and ^c:(member MaxY : ^b)) =
            rects |> Seq.fold (fun acc elem -> StretchRR acc elem ) box


    let inline BoundingBoxL (box:R< ^b>) 
                            (lines:seq< ^c> when ^c:(member X1 : ^b) and ^c:(member X2 : ^b) 
                                             and ^c:(member Y1 : ^b) and ^c:(member Y2 : ^b)) =
            lines |> Seq.fold (fun acc elem -> StretchRL acc elem ) box


    let inline BoundingI (range:I< ^b>) (p1:seq< ^b>) =
            p1 |> Seq.fold (fun acc elem -> StretchI acc elem ) range



module AUt =

    let Hamming s1 s2 = Array.map2((=)) s1 s2 |> Seq.sumBy(fun b -> if b then 0 else 1)


    let inline CompareArrays<'a> comp (seqA:'a[]) (seqB:'a[]) =
        Seq.fold (&&) true (Seq.zip seqA seqB |> Seq.map (fun (aa,bb) -> comp aa bb))


    let inline LenSq zeroVal (array:'a[]) =
        array |> Array.fold(fun acc v -> acc + v*v) zeroVal  


    let Len (array:float32[]) =
        array |> LenSq 0.0f
              |> float
              |> sqrt
              |> float32


    let ScaleF32 (scale:float32) (array:float32[]) =
        array |> Array.map(fun v -> scale *v)


    let Scale (scale:float) (array:float32[]) =
        array |> ScaleF32 (float32 scale)


module A2dUt = 

    let SymRowMajorIndex x y =
        if (x >= y) then x*(x+1)/2 + y
        else y*(y+1)/2 + x

    let UtCoords stride =
       let rec qq stride coords =
           seq {
                    match coords with
                    | a, b when b < a -> 
                        yield (a, b)
                        yield! (qq stride (a, b + 1))
                    | a, b when b < (stride- 1) -> 
                        yield (a, b)
                        yield! qq stride (a + 1, 0)
                    | a, b ->
                        yield (a, b)
           }
       seq {
             yield!  qq stride (0, 0)
       }
         
    // Creates a 1D array that represents a 2d upper triangular matrix
    let UpperTriangulate sqSize f =
        let cached = (UtCoords sqSize) 
                        |> Seq.map(fun (a,b) -> f a b)
                        |> Seq.toArray                            
        (fun x y -> cached.[SymRowMajorIndex x y] )

  // Creates a 1D array that represents a 2d upper triangular matrix with uniform diagonal
    let UpperTriangulateDiag (diagVal:'a) stride f =
        let cached = (UtCoords stride) 
                        |> Seq.map(fun (a,b) -> 
                                        match (a,b) with
                                        | (a,b) when a=b -> diagVal
                                        | (a,b) -> f a b)
                        |> Seq.toArray                            

        (fun x y -> cached.[SymRowMajorIndex x y] )


    let inline SetUniformDiagonal (mtx:'a[,]) (diagVal) = 
        Array2D.init (mtx.GetLength 0) 
                     (mtx.GetLength 1) 
                     (fun x y -> if(x=y) then diagVal  else mtx.[x,y])


    let Transpose (mtx : _ [,]) = 
        Array2D.init (mtx.GetLength 1) (mtx.GetLength 0) (fun x y -> mtx.[y,x])
    

    let GetRowsForArray2D (array:'a[,]) =
      let rowC = array.GetLength(0)
      let colC = array.GetLength(1)
      Array.init rowC (fun i ->
        Array.init colC (fun j -> array.[i,j]))


    let FillRowMajor rowCount colCount (vals:seq<'a>) =
        let data = vals |> Seq.take(rowCount*colCount) |> Seq.toArray
        Array2D.init rowCount colCount (fun x y -> data.[y+x*colCount])
    

    let FillColumnMajor rowCount colCount (vals:seq<'a>) =
        let data = vals |> Seq.take(rowCount*colCount) |> Seq.toArray
        Array2D.init rowCount colCount (fun x y -> data.[x+y*colCount])


    let flattenRowMajor (A:'a[,]) = A |> Seq.cast<'a>


    let flattenColumnMajor (A:'a[,]) = A |> Transpose |> Seq.cast<'a>


    let getColumn c (A:_[,]) = A.[*,c] |> Seq.toArray


    let getRow r (A:_[,]) = A.[r,*] |> Seq.toArray


module SeqUt = 

    let IterB (s:System.Collections.Generic.IEnumerator<'a>) =
        function () ->
                    s.MoveNext() |> ignore
                    s.Current


    let inline ZipMap f a b = Seq.zip a b |> Seq.map (fun (x,y) -> f x y)


//    let SeqToI (vals:seq<'a>) =
//        
//        let itsy = vals.GetEnumerator()
//        let mutable movin = itsy.MoveNext()
//        if not (itsy.MoveNext()) then
//            Seq.empty<I<'a>>
//        else
//            let mutable first = itsy.Current
//            let mutable second = itsy.Current
//            seq {
//                    while itsy.MoveNext() do
//                        second <- itsy.Current
//                        yield {I.Min = first; I.Max =second}
//                        first <- second
//                }
//        

    let AddInRange min max offsets baseSeq =
        Seq.map2 (NumUt.AddInRange min max) baseSeq offsets


    let AddInUF32 offsets baseSeq =
        Seq.map2 (NumUt.AddInRange 0.0f 1.0f) baseSeq offsets


    let AddInSF32 offsets baseSeq =
        Seq.map2 (NumUt.AddInRange -1.0f 1.0f) baseSeq offsets
