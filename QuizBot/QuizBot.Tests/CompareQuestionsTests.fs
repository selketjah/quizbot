module CompareQuestionTests
//
//  open System
//  open Xunit
//  open Swensen.Unquote
//  open QuizBot.Participant
//  open QuizBot.Core
//
//  let question = { 
//    Question = "What is the capital of Belgium?"
//    ExpectedAnswer = Answer.S "Brussels"
//    Type = QuestionType.Exact }
//
//  let mathias = {
//    Participant = Participant "Mathias"
//    Timestamp = DateTime(2010,1,1)
//    Answer = Answer.S "Paris"
//  }
//
//  let gien = {
//    Participant = Participant "Gien"
//    Timestamp = DateTime(2010,1,2)
//    Answer = Answer.S "Brussels"  
//  }
//
//  let don = {
//    Participant = Participant "Don"
//    Timestamp = DateTime(2010,1,1)
//    Answer = Answer.S "Brussels"    
//  }
//
//  [<Fact>]
//  let ``Given participant with correct answer Then returns participant`` () =
//    let expected = Some(don)
//    let actual = determineWinner question [| mathias; gien; don |]
//    test <@ expected = actual @>
//
//  [<Fact>]
//  let ``Given no participant with correct answer Then returns None`` () =
//    let expected = None
//    let actual = determineWinner question [| mathias |]
//    test <@ Option.isNone actual @>