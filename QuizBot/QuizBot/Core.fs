namespace QuizBot

module Participant =
  type Participant = Participant of string

  let value (Participant p) = p

module Core =

  open System
  open Participant

  type Category<'T when 'T : comparison> =
    | Exact of 'T
    | XOf of int * Set<'T>
    | Closest of 'T

  type Question<'T when 'T : comparison> = {  
    Question: string
    ExpectedAnswer: Category<'T>
  }

  type Guess = {
    Participant:Participant
    Timestamp:DateTime
    Answer:string
  }

  let validateExactAnswer (expectedAnswer:'a) (guesses:Guess[]) =
    guesses 
    |> Array.filter (fun guess -> guess.Answer.Equals(expectedAnswer))


  let validateXOfAnswer numberOf (expectedAnswer:'a) (guesses:Guess[])  =
    guesses

  let validateClosestAnswer (expectedAnswer:'a) (guesses:Guess[]) =
    guesses
  //  |> Array.groupBy (fun g -> difference question.ExpectedAnswer g.Answer)
  //  |> Array.sortBy fst
    

  let determineCandidates question guesses =
    match question.ExpectedAnswer with
    | Exact(x) -> validateExactAnswer x guesses
    | XOf(x, y) -> validateXOfAnswer x y guesses
    | Closest(x) -> validateClosestAnswer x guesses

  let determineWinner question guesses =
    match (determineCandidates question guesses) with
    | [||] -> None
    | candidates -> 
      candidates
      |> Array.minBy (fun answer -> answer.Timestamp)
      |> Some