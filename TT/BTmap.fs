namespace TT

module F32toF32 =

    open System
    
    let Quantize (cs:seq<float>) =
        let qv sv rv =
            let iv = (int rv)
            if (sv + 1) > iv then sv + 1
            else iv
        cs |> Seq.mapi(qv)


    let Quantize32 (cs:seq<float32>) =
        let qv sv rv =
            let iv = (int rv)
            if (sv + 1) > iv then sv + 1
            else iv
        cs |> Seq.mapi(qv)

    // produces a sequence within [0.0,1.0] for 0.25 < halfVal < 0.75
    let QuadInterp halfVal count =
        let stepSize = 1.0 / (float count)
        seq {0.0 .. stepSize .. 1.0 }
        |> Seq.map(fun x -> x * x * (2.0 - 4.0 * halfVal) + x * (4.0 * halfVal - 1.0))


    // returns the bin index for the value, for QuadInterp bins distributed over [0.0,1.0]
    let InvQuadInterp halfVal (count:int) value =
        let IntStep v =
            (int (Math.Floor((float count) * v )))

        if(Math.Abs(halfVal - 0.5) < 0.005) then
            IntStep(value / (2.0 * halfVal))
            else
            IntStep((
                        (1.0 - 4.0 * halfVal) 
                        +
                        Math.Sqrt(8.0 * halfVal * (2.0 * halfVal - 1.0 ) + 1.0 +  8.0 * value * (1.0 - 2.0 * halfVal))
                    )
                    / 
                    (4.0 - 8.0 * halfVal)
                  )

   // QuadInterp 0.25 14 |> Seq.map(fun x -> x * 200.0) |>  Quantize  |> Seq.toArray

    let tt =
        let q = InvQuadInterp 0.25 10
        seq {0.8 .. 0.0025 .. 1.0 } |> Seq.map(q) |> Seq.toArray



    let Elp a pow steps =
        let la = Math.Log a
        Seq.initInfinite(fun x -> Math.Exp (la * Math.Pow( (float x), pow )))
        |> Seq.take(steps)


    let BinOfA<'a when 'a:comparison> (bins:'a[]) (value:'a) = 
        let rec binf (a:'a[]) (dex:int) =
            if dex < 0 then -1
            else if a.[dex] <= value then dex
            else binf a (dex-1)
        binf bins (bins.Length - 1)


    let BinOfL<'a when 'a:comparison> (bins:'a list) (value:'a) = 
        let rec binf (a:'a list) (dex:int) =
            match a with
            | [] -> dex
            | a::b when a <= value -> binf b (dex + 1)
            | _ -> dex - 1

        binf bins 0

    let ExpTics aBase aPow steps =
         Quantize (Elp aBase aPow steps)


    let resy = ExpTics 1.5 0.7 20


//  [|1; 2; 3; 5; 9; 15; 24; 37; 57; 88; 135; 204; 307; 460|]
    let Step14FromOneTo180 =
        Elp 2.0 0.85 14 |> Seq.map(fun x -> (int x)) |>Seq.toArray

    let Step14FromOneTo180L =
        Elp 2.0 0.85 14 |> Seq.map(fun x -> (int x)) |>Seq.toList


    let mappy = BinOfA Step14FromOneTo180

    let slappy = BinOfL Step14FromOneTo180L