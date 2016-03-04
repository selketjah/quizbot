module UnitTests

open System
open Xunit
open Swensen.Unquote
open QuizBot.Core

let question = { 
  Question = "What is the capital of Belgium?"
  ExpectedAnswer = "Brussels" }

//[<Fact>]
//let ``Given wrong answer Then return false`` () =
//  let expected = false
//  let actual = validateAnswer question "Paris"
//  test <@ expected = actual @>
//
//[<Fact>]
//let ``Given correct answer Then return true`` () =
//  let expected = true
//  let actual = validateAnswer question "Brussels"
//  test <@ expected = actual @>

let mathias = {
  Participant = Participant "Mathias"
  Timestamp = DateTime(2010,1,1)
  Answer = "Paris"
}

let gien = {
  Participant = Participant "Gien"
  Timestamp = DateTime(2010,1,2)
  Answer = "Brussels"  
}

let don = {
  Participant = Participant "Don"
  Timestamp = DateTime(2010,1,1)
  Answer = "Brussels"    
}

[<Fact>]
let ``Given participant with correct answer Then returns participant`` () =
  let expected = Some(don)
  let actual = determineWinner question [| mathias; gien; don |]
  <@ expected = actual @>

[<Fact>]
let ``Given no participant with correct answer Then returns None`` () =
  let expected = None
  let actual = determineWinner question [| mathias |]
  <@ Option.isNone actual @>