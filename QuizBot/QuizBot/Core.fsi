namespace QuizBot

module Core =

  open System

  type Question = { 
    Question: string
    ExpectedAnswer: string 
  }

  type Participant = Participant of string

  type Answer = {
    Participant:Participant
    Timestamp:DateTime
    Answer:string
  }

  val determineWinner: Question -> Answer[] -> Answer option