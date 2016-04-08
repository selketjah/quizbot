namespace QuizBot

module Participant =
  type Participant = Participant of string

  let value (Participant p) = p

module Core =

  open System
  open FParsec
  open Participant
  open Normalization

  type Category =
    | Exact of string
    | XOf of int * Set<string>
    | Closest of float

  type Question = {  
    Question: string
    ExpectedAnswer: Category
  }

  type Answer = {
    Participant:Participant
    Timestamp:DateTime
    Answer:string
  }

  let parseFloat text =
    match (run pfloat text) with
    | Success(result, _, _)   -> result
    | Failure(errorMsg, _, _) -> failwith errorMsg


  let validateExactAnswer expectedAnswer (guesses:Answer[]) =
    guesses 
    |> Array.filter (fun guess -> guess.Answer
                                  |> Normalization.cleanText
                                  |> fun answer -> answer.Equals (expectedAnswer |> Normalization.cleanText ))

  let validateXOfAnswer (numberOf, expectedAnswer) (guesses:Answer[])  =
    guesses
    |> Array.filter(fun x -> 
      x.Answer.Split(',')
      |> Set.ofArray
      |> Set.map Normalization.cleanText
      |> Set.intersect (expectedAnswer 
                        |> Set.map Normalization.cleanText)
      |> Set.count
      |> fun count -> count >= numberOf)

  let validateClosestAnswer expectedAnswer (guesses:Answer[]) =
    guesses
    |> Array.map (fun guess -> (guess, abs(expectedAnswer -  parseFloat guess.Answer)))
    |> Array.groupBy snd
    |> Array.minBy fst
    |> snd
    |> Array.map (fun (guess, diff) -> guess)

  let determineCandidates question guesses =
    match question.ExpectedAnswer with
    | Exact(x) -> validateExactAnswer x guesses
    | XOf(x, y) -> validateXOfAnswer (x, y) guesses
    | Closest(x) -> validateClosestAnswer x guesses

  let determineWinner question guesses =
    match (determineCandidates question guesses) with
    | [||] -> None
    | candidates -> 
      candidates
      |> Array.minBy (fun answer -> answer.Timestamp)
      |> Some