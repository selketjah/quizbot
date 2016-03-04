namespace QuizBot

module Participant =
  type Participant = Participant of string

  let value (Participant p) = p

module Core =

  open System
  open Participant

  type Question = { 
    Question: string
    ExpectedAnswer: string 
  }

  type Answer = {
    Participant:Participant
    Timestamp:DateTime
    Answer:string
  }

  let validateAnswer (question:Question) answer =
    (question.ExpectedAnswer = answer)

  let determineCandidates question answers = 
      answers
      |> Array.filter (fun answer -> 
        validateAnswer question answer.Answer)

  let determineWinner question answers =
    match (determineCandidates question answers) with
    | [||] -> None
    | candidates -> 
      candidates
      |> Array.minBy (fun answer -> answer.Timestamp)
      |> Some



