namespace QuizBot

module Participant =

  type Participant = Participant of string  

  val value: Participant -> string

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

  val determineWinner: Question -> Answer[] -> Answer option