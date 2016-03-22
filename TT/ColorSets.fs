namespace TT
open System
open System.Windows.Media

type LegendMap1d<'a> = 
    { minC:Color; maxC:Color; spanC:Color[]; mapper:'a->int; minV:'a; maxV:'a;
      tics: 'a[] }


module ColorSets =
    
    let WcColors = 
        [|
           Colors.DarkSlateGray;  Colors.SteelBlue; Colors.DodgerBlue;  Colors.MediumBlue;
           Colors.DarkBlue; Colors.Chartreuse; Colors.Green;  Colors.DarkGreen;
           Colors.LightYellow;  Colors.Yellow; Colors.Orange;  Colors.OrangeRed;
           Colors.Red;  Colors.DarkRed;
        |]


    let ColorFade (stepCount:int) (refColor:Color) =
        let bite ob = (byte (ob * 255 / stepCount))
        {0 .. stepCount-1} |> Seq.map(fun st -> Color.FromArgb(bite st, refColor.R, refColor.G, refColor.B))


    let ByteInterp (bL:byte) (bR:byte) (stepMax:int) (step:int) =
        let iL = (int bL)
        let iR = (int bR)
        let incy = (step *  (iR - iL)) / stepMax
        (byte (iL + incy))


    let ColorSpan (stepCount:int) (colStart:Color) (colEnd:Color) =
        {1 .. stepCount } |> Seq.map(fun step -> 
            Color.FromArgb(
                ByteInterp colStart.A colEnd.A (stepCount + 1) step,
                ByteInterp colStart.R colEnd.R (stepCount + 1) step,
                ByteInterp colStart.G colEnd.G (stepCount + 1) step,
                ByteInterp colStart.B colEnd.B (stepCount + 1) step))


    let TriColorStrip (interStep:int) (colorA:Color) (colorB:Color) (colorC:Color) =
        let btwStp = ColorSpan interStep
        [| colorC |] 
            |> Array.append (btwStp colorB colorC |> Seq.toArray)
            |> Array.append [| colorB |] 
            |> Array.append (btwStp colorA colorB |> Seq.toArray)
            |> Array.append [| colorA |]

    
    let TriColorRing (interStep:int) (colorA:Color) (colorB:Color) (colorC:Color) =
        let btwStp = ColorSpan interStep
        (btwStp colorC colorA |> Seq.toArray)
            |> Array.append  [| colorC |] 
            |> Array.append (btwStp colorB colorC |> Seq.toArray)
            |> Array.append [| colorB |] 
            |> Array.append (btwStp colorA colorB |> Seq.toArray)
            |> Array.append [| colorA |]


    let QuadColorRing (interStep:int) (colorA:Color) (colorB:Color) 
                  (colorC:Color) (colorD:Color) =
        let btwStp = ColorSpan interStep
        (btwStp colorD colorA |> Seq.toArray)
            |> Array.append  [| colorD |] 
            |> Array.append (btwStp colorC colorD |> Seq.toArray)
            |> Array.append  [| colorC |] 
            |> Array.append (btwStp colorB colorC |> Seq.toArray)
            |> Array.append [| colorB |] 
            |> Array.append (btwStp colorA colorB |> Seq.toArray)
            |> Array.append [| colorA |]


    let ColStr (col:Color) = 
        sprintf "[%i, %i, %i]" col.R col.G col.B
        

    // Exp distributes 14 colors over the range [1, 196]
    let WcLegend = 
        { minC=Colors.Transparent; maxC=Colors.HotPink; 
          spanC=WcColors; mapper=GenSteps.InvExpStepOne; 
          minV=1.0; maxV=196.0; tics=GenSteps.ExpStepTicsOne }


    let GetColor<'a when 'a:comparison> (lm:LegendMap1d<'a>) (value:'a) =
        if (value < lm.minV) then lm.minC
        else if (value > lm.maxV) then lm.maxC
        else lm.spanC.[lm.mapper value]