namespace QuizBot

module Participant =

  type Participant = Participant of string  

  val value: Participant -> string

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

  val determineWinner: Question -> Guess[] -> Guess option