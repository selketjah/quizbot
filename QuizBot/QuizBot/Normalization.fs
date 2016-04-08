namespace QuizBot

module Normalization =

  open System
  open System.Text.RegularExpressions

  type Cleaner = string -> string

  let clean (cleaner:Cleaner) (txt:string) =
    txt
    |> cleaner

  let trim (txt:string) =
    txt.Trim()

  let decap (txt:string) =
    txt.ToLower()

  let removeDoubleWhitespace (txt:string) =
    Regex.Replace(txt, "  ", " ", RegexOptions.IgnoreCase)

  let textCleaner = (trim >> removeDoubleWhitespace >> decap)

  let cleanText (txt:string) =
    clean textCleaner txt
