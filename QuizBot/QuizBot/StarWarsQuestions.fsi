namespace QuizBot

module StarWarsQuestions =

  open QuizBot.Core

  val guessThreePlanetsQuestion: unit -> Question<string>

  val guessPlanetPopulation: unit -> Question<int64>