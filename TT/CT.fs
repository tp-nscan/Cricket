namespace TT


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
        

    let inline MinMaxUnion (values: seq< ^a> when ^a:(member Min:float32) 
                                    and ^a:(member Max:float32)) =
        let lstMin = values |> Seq.map(fun v-> (^a : (member Min : float32) v))
                            |> Seq.toList
        let lstMax = values |> Seq.map(fun v-> (^a : (member Max : float32) v))
                            |> Seq.toList
        { 
            I.Min = lstMin |> List.min;
            I.Max = lstMax |> List.max;
        }

        

