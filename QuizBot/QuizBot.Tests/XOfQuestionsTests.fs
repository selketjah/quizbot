module XOfQuestionsTests

  open System
  open Xunit
  open Swensen.Unquote
  open QuizBot.Participant
  open QuizBot.Core

  let planets = set [ "one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine"; "ten"]

  let question = { 
    Question = "Name three Star War Planets. [1,2,3]"
    ExpectedAnswer = Category.XOf (3, planets)
  }

  let mathias = {
    Participant = Participant "Mathias"
    Timestamp = DateTime(2016,3,17)
    Answer = "three,six,eight"
  }

  let gien = {
    Participant = Participant "Gien"
    Timestamp = DateTime(2016,3,18)
    Answer = "one,nine,ten"  
  }

  let don = {
    Participant = Participant "Don"
    Timestamp = DateTime(2016,3,19)
    Answer = "two,five,eleven"  
  }

  [<Fact>]
  let ``Given participant with XOf answer Then returns participant`` () =
    let expected = Some(mathias)
    let actual = determineWinner question [| mathias; gien; don |]
    test <@ expected = actual @>
