namespace QuizBot

module Participant =

  type Participant = Participant of string  

  val value: Participant -> string

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

  val determineWinner: Question<'T> -> Guess[] -> Guess option