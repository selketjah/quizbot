namespace QuizBot

module Announcement =
  
  open System
  open QuizBot.Core
  
  val announceWinner: Answer -> Question -> string

  val announceNoWinner: Question -> string
