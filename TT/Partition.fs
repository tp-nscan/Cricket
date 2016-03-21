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

    let Mid (span:I<float32>) =
        (span.Max - span.Min) / 2.0f


    let TicMarks (interval:I<float32>) (segments:int) =
        let midge = (BT.Span interval) / (float32 segments)
        let tock tic =
            interval.Min + midge * (float32 tic)
        seq {0 .. segments} |> Seq.map(fun s -> tock s)


    let TileInterval (inter:I<float32>) (tileCount:int) =
        (TicMarks inter tileCount) |> GenBT.IofSeqSlide
        

//    let inline MinMaxUnion (values: seq< ^a> when ^a:(member Min:float32) 
//                                    and ^a:(member Max:float32)) =
//        let lstMin = values |> Seq.map(fun v-> (^a : (member Min : float32) v))
//                            |> Seq.toList
//        let lstMax = values |> Seq.map(fun v-> (^a : (member Max : float32) v))
//                            |> Seq.toList
//        { 
//            I.Min = lstMin |> List.min;
//            I.Max = lstMax |> List.max;
//        }

        

