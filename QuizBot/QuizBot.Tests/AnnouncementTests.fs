module AnnouncementTests

  open System
  open Xunit
  open Swensen.Unquote
  open QuizBot.Core
  open QuizBot.Participant
  open QuizBot.Announcement

  let exactQuestion = { 
    Question = "What is the capital of Belgium?"
    ExpectedAnswer = Exact "Brussels"
  }

  let closestQuestion = {
    Question = "What is the population of Endor?"
    ExpectedAnswer = Closest 10000.
  }

  let xofQuestion = {
    Question = "Name three planets of Star Wars."
    ExpectedAnswer =  XOf (3, set [ "one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine"; "ten"])
  }

  let exactWinner = {
    Participant = Participant "selketjah"
    Timestamp = DateTime(2010,1,2)
    Answer = "Brussels"  
  }

  let closestWinner = {
    Participant = Participant "selketjah"
    Timestamp = DateTime(2016, 4, 9)
    Answer = "9990"
  }

  let xofWinner = {
    Participant = Participant "selketjah"
    Timestamp = DateTime(2016, 4, 8)
    Answer = "Endor, Bespin, Corellia"
  }

  [<Fact>]
  let `` Given question type exact when announcing winner then answer is in announcement `` () =
    let expected = "@selketjah has won! The correct answer was Brussels."
    let actual = announceWinner exactWinner exactQuestion
    test <@ expected = actual @>

  [<Fact>]
  let `` Given question type Closest when announcing winner then answer is in announcement  `` () =
    let expected = "@selketjah has won! The correct answer was 10000.00."
    let actual = announceWinner closestWinner closestQuestion
    test <@ expected = actual @>

  [<Fact>]
  let `` Given question type XOf when announcing winner then winning combination is in announcement `` () =
    let expected = "@selketjah has won with the combination [Endor, Bespin, Corellia]."
    let actual = announceWinner xofWinner xofQuestion
    test <@ expected = actual @>

  [<Fact>]
  let  `` Given question type exact when announcing no winner then answer is in announcement `` () =
    let expected = "Nobody won :-( The correct answer was Brussels."
    let actual = announceNoWinner exactQuestion
    test <@ expected = actual @>


  [<Fact>]
  let `` Given question type Closest when announcing no winner then answer is in announcement `` () =
    let expected = "Nobody was close enough :'( The exact answer was 10000."
    let actual = announceNoWinner closestQuestion
    test <@ expected = actual @>

  [<Fact>]
  let `` Given question type XOf when announcing no winner then answer is in announcement `` () =
    let expected = "Nobody had 3 correct answers. :-/"
    let actual = announceNoWinner xofQuestion
    test <@ expected = actual @>