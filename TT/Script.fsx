

//#load "BT.fs"
//open TT
//
//#time
//let sp = { I.Min= 3.0f; Max = 4.0f }
//let pt = 3.5f
//let ans = BT.InL sp pt
//ans
//let res = BT.Span sp 
//res
//
//
//#load "BT.fs"
//open TT
//
//let mutable res = true
//let sp = { I.Min= 3.0f; Max = 4.0f }
//#time
//for i in 0 .. 100000000 do
//    let pt = 3.4f
//    res <- BT.InL sp pt
//
//
//#load "BT.fs"
//open TT
//
//#time
//let rect = { R.MinX= 3.0f; MaxX = 4.0f; R.MinY= 3.0f; MaxY = 4.8f; }
//let mutable res = 0.0f
//for i in 0 .. 100000000 do
//    let pt = 3.4f
//    res <- BT.Area rect
//
//
//#load "BT.fs"
//open TT
//
//#time
//let rect = { R.MinX= 3.0f; MaxX = 4.0f; R.MinY= 3.0f; MaxY = 4.8f; }
//let pt = { P2.X= 3.3f; Y = 4.0f; }
//let mutable res = true
//for i in 0 .. 100000000 do
//    res <- BT.InR rect pt
//
//
//#load "BT.fs"
//open TT
//
//#time
//let rect = { R.MinX= 3.0f; MaxX = 4.0f; R.MinY= 3.0f; MaxY = 4.8f; }
//let pt = { P2.X= 5.3f; Y = 2.0f; }
//let mutable res = rect
//for i in 0 .. 100000000 do
//    res <- BT.StretchRP rect pt


#r @"C:\Users\tpnsc_000\Documents\GitHub\DonutDevil\packages\MathNet.Numerics.3.7.0\lib\net40\MathNet.Numerics.dll"
#r @"C:\Users\tpnsc_000\Documents\GitHub\DonutDevil\packages\MathNet.Numerics.FSharp.3.7.0\lib\net40\MathNet.Numerics.FSharp.dll"
#r @"C:\Users\tpnsc_000\Documents\GitHub\HopAlong\Utils\bin\Debug\Utils.dll"

open System
open MathNet.Numerics
open MathNet.Numerics.Distributions
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.Random

#load "BT.fs"
#load "Gen.fs"
open TT

let k = GenBT.TestL 123 3 |> Seq.toArray

//let a = GenS.NormalF32 (GenV.Twist 5) 0.5f 0.5f 
//        |> Seq.take(10)
//        |> Seq.toArray
//
//let mutable b =  a |> GenS.FlipBSF32Seq 0.5f (GenV.Twist 52)
//
//#time
//for i in 0 .. 1000000 do
//    b <- a |> GenS.FlipBUF32Seq 0.5f (GenV.Twist 52)


//let j = seq {1 .. 10} |> Seq.windowed 3 |> Seq.map(fun v-> (v.[0], v.[1])) |> Seq.toList
//
//#load "BT.fs"
//#load "CT.fs"
//open TT
//
//let jj = {I.Min=0.2f; Max=0.5f;}
//let k = CT.TicMarks jj 22 |> Seq.toArray


#r @"C:\Users\tpnsc_000\Documents\GitHub\DonutDevil\packages\MathNet.Numerics.3.7.0\lib\net40\MathNet.Numerics.dll"
#r @"C:\Users\tpnsc_000\Documents\GitHub\DonutDevil\packages\MathNet.Numerics.FSharp.3.7.0\lib\net40\MathNet.Numerics.FSharp.dll"
#r @"C:\Users\tpnsc_000\Documents\GitHub\HopAlong\Utils\bin\Debug\Utils.dll"

open System
open MathNet.Numerics
open MathNet.Numerics.Distributions
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.Random
#load "BT.fs"
#load "Gen.fs"
#load "CT.fs"
#load "MathNetUtils.fs"

open TT

let res = CT.TileInterval {I.Min=1.0f; Max=3.7f } 4 |> Seq.toArray

let sq =  seq {0.0f .. 10000000.0f }
//let res = sq |> GenBT.IofSeq |> Seq.take(100) |> Seq.toArray


//let mutable res = sq |> SeqUt.SeqToI |> Seq.take(100) |> Seq.toArray