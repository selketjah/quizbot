namespace QuizBot

module Participant =

  type Participant = Participant of string  

  val value: Participant -> string

module Core =

  open System
  open Participant

  type Category =
    | Exact of string
    | XOf of int * Set<string>
    | Closest of float

  type Question= {  
    Question: string
    ExpectedAnswer: Category
  }

  type Answer = {
    Participant:Participant
    Timestamp:DateTime
    Answer:string
  }

  val determineWinner: Question -> Answer[] -> Answer option