namespace QuizBot

module WorldBankQuestions =

  open QuizBot.Core

  val guessCapitalOfCountryQuestion : unit -> Question<string>

  val guessCountryWithCapitalQuestion : unit -> Question<string>