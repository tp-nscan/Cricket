namespace TT

module DesignData =

    let Grid2DTestData (strides:Sz2<int>) =
        CT.Raster2d strides |> Seq.map(fun p ->
           { P2V.X=p.X; Y=p.Y; 
             V= ( float32 (p.X + p.Y * strides.X)) /
                ( 0.5f * (float32 (strides.X * strides.Y)) - 1.0f)
           } )