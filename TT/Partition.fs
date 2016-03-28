namespace TT
open System

module GenSteps =
    
    //Used to map left bounded reals to exponentialy increasing steps
    //Used to make the WC legends

    let ExpStepSeq (div:float) (ofBase:float) =
        
        Seq.initInfinite(fun x -> Math.Pow(ofBase, (float x+1.0) / div))

    let ExpStepTicsOne = ExpStepSeq 7.0 14.0 |> Seq.take(14) |> Seq.toArray


    let InvExpStep (div:float) (ofBase:float) (value:float) =
        Math.Log(value, ofBase) * div - 1.0

    let InvExpStepOne value = (int (InvExpStep 7.0 14.0 value))


    //Used to map [0.0, 1.0f] to [0 .. max-1]

    let UniformUbTics max (value:float32) =
        {0.0f .. max-1.0f} |> Seq.map(fun x-> x/max) |> Seq.toArray

    let USqurdUbTics max (value:float32) =
        {0.0f .. max-1.0f} |> Seq.map(fun x-> (x * x)/(max * max)) |> Seq.toArray

    let USqurtUbTics max (value:float32) =
        {0.0f .. max-1.0f} |> Seq.map(fun x-> (float32 (Math.Sqrt(float value))) / max) 
                           |> Seq.toArray

    let UniformUbMap (max:int) (value:float32) =
        (int (value * (float32 max)))

    let SqrdUbMap (max:int) (value:float32) =
        (int (value * value * (float32 max)))

    let SqrtUbMap (max:int) (value:float32) =
        (int (Math.Sqrt(float value) * (float max)))

    let UniformUbTo256 (max:int) (value:float32) =
        UniformUbMap 256

    let SqrdUbTo256 (max:int) (value:float32) =
        SqrtUbMap 256

    let SqrtUbTo256 (max:int) (value:float32) =
        SqrtUbMap 256


module CT =

    let TicMarks (interval:I<float32>) (segs:int) =
        let midge = (BT.Span interval) / (float32 segs)
        let tock tic = interval.Min + midge * (float32 tic)
        seq {0 .. segs} |> Seq.map(fun s -> tock s)


    let TicMarks2d (region:R<float32>) (segs:Sz2<int>) =
        let midgeX = (BT.SpanX region) / (float32 segs.X)
        let tockX tic = region.MinX + midgeX * (float32 tic)
        let midgeY = (BT.SpanY region) / (float32 segs.Y)
        let tockY tic = region.MinY + midgeY * (float32 tic)
        seq { for row in 0 .. segs.Y do
                for col in 0 .. segs.X do
                    yield {P2.X = tockX col; Y= tockY row;}
            }


    let TileInterval (inter:I<float32>) (tileCount:int) =
        (TicMarks inter tileCount) |> BTmap.IofSeqSlide
        

    let Raster2d (strides:Sz2<int>) =
        seq { for col in 0 .. strides.X - 1 do
                for row in 0 .. strides.Y - 1 do
                    yield {P2.X= col; Y= row;}
            }
        

