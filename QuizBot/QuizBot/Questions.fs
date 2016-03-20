namespace QuizBot

module Questions =
  
  open System
  open QuizBot.Core
  open QuizBot.WorldBankQuestions
  open QuizBot.StarWarsQuestions

  let questions = 
    [| WorldBankQuestions.guessCapitalOfCountryQuestion ();
       WorldBankQuestions.guessCountryWithCapitalQuestion ();
       StarWarsQuestions.guessPlanetPopulation ();
       StarWarsQuestions.guessThreePlanetsQuestion () |]

  let questionCount = questions.Length

  let rng = Random()

  let next () =
    let i = rng.Next(questionCount)
    questions.[i]
