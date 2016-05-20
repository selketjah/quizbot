namespace QuizBot

module Announcement =

  open System
  open QuizBot.Core
  
  let announceWinner (winner:Answer) (question:Question) =
    match question.ExpectedAnswer with
    | Exact(x) -> sprintf "@%s has won! The correct answer was %s." (winner.Participant |> Participant.value) x
    | XOf(x, y) -> sprintf "@%s has won with the combination [%s]." (winner.Participant |> Participant.value) winner.Answer
    | Closest(x) -> sprintf "@%s has won! The correct answer was %.2f." (winner.Participant |> Participant.value) x

  let announceNoWinner (question:Question) =
    match question.ExpectedAnswer with
    | Exact(x) -> sprintf "Nobody won :-( The correct answer was %s." x
    | XOf(x, y) -> sprintf "Nobody had %i correct answers. :-/" x
    | Closest(x) -> sprintf "Nobody was close enough :'( The exact answer was %.0f." x