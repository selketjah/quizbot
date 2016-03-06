namespace QuizBot

module Participant =

  type Participant = Participant of string  

  val value: Participant -> string

module Core =

  open System
  open Participant

  type ExpectedAnswerType =
  | Compare
  | XOf of int
  | Closest

  type Question = { 
    Question: string
    ExpectedAnswer: string
    ExpectedAnswerType: ExpectedAnswerType
  }

  type Answer = {
    Participant:Participant
    Timestamp:DateTime
    Answer:string
  }

  val determineWinner: Question -> Answer[] -> Answer option