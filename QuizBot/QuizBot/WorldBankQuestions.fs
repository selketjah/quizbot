namespace QuizBot

module WorldBankQuestions =

  open System
  open FSharp.Data
  open QuizBot.Core

  let wb = WorldBankData.GetDataContext()

  let countriesCapitals =
    wb.Countries
    |> Seq.map (fun country -> (country.Name, country.CapitalCity))
    |> Seq.filter (fun (country, capital) -> capital <> "")
    |> Seq.toArray

  let countriesCount = countriesCapitals.Length

  let rng = Random()

  let getRandomCountryCapital () =
    let i = rng.Next(countriesCount)
    countriesCapitals.[i]

  let guessCapitalOfCountryQuestion () =
    let country, capital = getRandomCountryCapital ()
    { Question = sprintf "What is the capital of %s?" country
      ExpectedAnswer = Answer.S capital
      Type = QuestionType.Compare }

  let guessCountryWithCapitalQuestion () =
    let country, capital = getRandomCountryCapital ()
    { Question = sprintf "What is the country with capital city %s?" capital
      ExpectedAnswer = Answer.S country
      Type = QuestionType.Compare }