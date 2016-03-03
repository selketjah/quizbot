namespace QuizBot

module Core =

  open System

  type Question = { 
    Question: string
    ExpectedAnswer: string 
  }

  type Participant = Participant of string

  type Answer = {
    Participant:Participant
    Timestamp:DateTime
    Answer:string
  }

  let validateAnswer (question:Question) answer =
    (question.ExpectedAnswer = answer)

  let determineCandidates question answers = 
      answers
      |> List.filter (fun answer -> 
        validateAnswer question answer.Answer)

  let determineWinner question answers =
    match (determineCandidates question answers) with
    | [] -> None
    | candidates -> 
      candidates
      |> List.minBy (fun answer -> answer.Timestamp)
      |> Some

