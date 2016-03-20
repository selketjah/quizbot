namespace QuizBot

module Questions =
  
  open System
  open QuizBot.Core
  open QuizBot.WorldBankQuestions
  open QuizBot.StarWarsQuestions

  val next: unit -> Question
