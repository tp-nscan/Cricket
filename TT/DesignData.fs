namespace TT

module DesignData =

    let Grid2DTestData (strides:Sz2<int>) =
        let denom = ( 0.5f * (float32 (strides.X * strides.Y)))
        CT.Raster2d strides |> Seq.map(fun p ->
           { P2V.X=p.X; Y=p.Y; 
             V = ( float32 (p.X + p.Y * strides.X)) / denom - 1.0f})