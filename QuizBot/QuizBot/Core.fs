namespace QuizBot

module Participant =
  type Participant = Participant of string

  let value (Participant p) = p

module Core =

  open System
  open Participant

  type QuestionType =
  | Compare
  | XOf of int
  | Closest

  type Answer = S of string | I of int64

  type Question = { 
    Question: string
    ExpectedAnswer: Answer
    Type: QuestionType
  }

  type Guess = {
    Participant:Participant
    Timestamp:DateTime
    Answer:Answer
  }

  let validateCompareAnswer (question:Question) (guesses:Guess[]) =
    guesses
    |> Array.filter (fun guess -> (question.ExpectedAnswer = guess.Answer))

  let validateXOfAnswer (question:Question) (guesses:Guess[]) numberOf =
    guesses

  let validateClosestAnswer (question:Question) (guesses:Guess[]) =
    guesses


  let determineCandidates question guesses =
    match question.Type with
    | Compare -> validateCompareAnswer question guesses
    | XOf(x) -> validateXOfAnswer question guesses x
    | Closest -> validateClosestAnswer question guesses

  let determineWinner question guesses =
    match (determineCandidates question guesses) with
    | [||] -> None
    | candidates -> 
      candidates
      |> Array.minBy (fun answer -> answer.Timestamp)
      |> Some