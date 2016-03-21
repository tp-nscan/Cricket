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
        (byte (iR - iL))

    let trib =
        Colors.BlanchedAlmond.G

    let boink value =
        (byte value)

//    let ColorSpan (stepCount:int) (colStart:Color) (colEnd:Color) =
//        let colStep (step:int) = 
//            let newR = colStart.R - colEnd.R
//            
//        {0 .. stepCount-1} |> Seq.map(fun st -> colStep st)


    // Exp distributes 14 colors over the range [1, 196]
    let WcLegend = 
        { minC=Colors.Transparent; maxC=Colors.HotPink; 
          spanC=WcColors; mapper=GenSteps.InvExpStepOne; 
          minV=1.0; maxV=196.0; tics=GenSteps.ExpStepTicsOne }


    let GetColor<'a when 'a:comparison> (lm:LegendMap1d<'a>) (value:'a) =
        if (value < lm.minV) then lm.minC
        else if (value > lm.maxV) then lm.maxC
        else lm.spanC.[lm.mapper value]