#I "../../packages"
#r @"FSharp.Data/lib/net40/FSharp.Data.dll"
#r @"FParsec/lib/net40-client/FParsecCS.dll"
#r @"FParsec/lib/net40-client/FParsec.dll"
#load @"../../paket-files/evelinag/fsharp-swapi/swapi.fs"

#load "Core.fs"
#load "WorldBankQuestions.fs"
#load "StarWarsQuestions.fs"

open System
open QuizBot
open QuizBot.WorldBankQuestions
open QuizBot.StarWarsQuestions

type Cleaner = string -> string

// remove leading and tailing whitespace
let trim (txt:string) =
  txt.Trim()

// remove caps
let decap (txt:string) =
  txt.ToLower()

let clean (cleaner:Cleaner) (txt:string) =
  txt
  |> cleaner

trim " answer  " = "answer"
decap "AnSwer" = "answer"
decap "Änswer" = "änswer"

clean (trim >> decap) "ÄnsWer " = "änswer"

open System.Text.RegularExpressions

//remove double spaces
let removeDoubleWhitespace (txt:string) =
  Regex.Replace(txt, "  ", " ", RegexOptions.IgnoreCase)

removeDoubleWhitespace "whitespaced  answer" = "whitespaced answer"

clean (trim >> removeDoubleWhitespace >> decap ) "WhîteSpaced  anSwer" = "whîtespaced answer"

// remove special chars
//let removeSpecialChars (txt:string) =
//
//['$'; '%'; '&'; '\''; '('; ')'; '*'; '+'; ','; '-'; '.'; '/']




