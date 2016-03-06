namespace QuizBot

module StarWarsQuestions =

  open StarWars.API
  open QuizBot.Core

  let movies = getAllFilms()

  let planets =
    getAllPlanets()
    |> Array.map(fun m -> m.Name)
    |> String.concat ", "

  let guessThreePlanetsQuestion () =
    { Question = sprintf "Name three planets from Star Wars. [1,2,3]"
      ExpectedAnswer = planets }
