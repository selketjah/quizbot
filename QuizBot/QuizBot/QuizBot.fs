module QuizBot

// TODO: rename Answer? Ambiguous, used in Question and Answer
// TODO: extract core domain in module
// TODO / mayber: simplify determineWinner

open System

type Question = { Question: string; Answer: string }

type Participant = string

type Answer = {
  Participant:Participant
  Timestamp:DateTime
  Answer:string
}

let validateAnswer (question:Question) answer =
  (question.Answer = answer)

let determineCandidates question answers = 
    answers
    |> List.filter (fun answer -> validateAnswer question answer.Answer)

let determineWinner question answers =
  match (determineCandidates question answers) with
  | [] -> None
  | candidates -> 
    candidates
    |> List.minBy (fun answer -> answer.Timestamp)
    |> Some

