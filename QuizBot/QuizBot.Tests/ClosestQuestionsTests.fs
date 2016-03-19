module ClosestQuestionsTests

  open System
  open Xunit
  open Swensen.Unquote
  open QuizBot.Participant
  open QuizBot.Core

  let question = { 
    Question = "What is the population of Star War Planet?"
    ExpectedAnswer = Closest (float 10000)
  }

  let mathias = {
    Participant = Participant "Mathias"
    Timestamp = DateTime(2016,1,1)
    Answer = "9900"
  }

  let gien = {
    Participant = Participant "Gien"
    Timestamp = DateTime(2016,1,2)
    Answer = "9800"  
  }

  let don = {
    Participant = Participant "Don"
    Timestamp = DateTime(2016,1,2)
    Answer = "9900"  
  }

  [<Fact>]
  let ``Given participant with closest answer Then returns participant`` () =
    let expected = Some(mathias)
    let actual = determineWinner question [| mathias; gien; don |]
    test <@ expected = actual @>