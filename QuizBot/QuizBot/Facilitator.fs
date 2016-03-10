namespace QuizBot

module Facilitator = 

  open System
  open System.Text.RegularExpressions
  open QuizBot.Participant
  open QuizBot.Core
  open QuizBot.WorldBankQuestions
  open QuizBot.Twitter

  let sleepTime = TimeSpan(0,0,1)

  let (|IsInt|_|) input =
    let m = Regex.Match(input, @"^\d+$")
    if (m.Success) then Some (Int64.Parse input) else None

  let parseResponse r =
    { Participant = Participant r.ScreenName
      Timestamp = r.Timestamp
      Answer = match r.Message with
               | IsInt i -> Answer.I i
               | _ -> Answer.S r.Message }

  let announceWinner (participant:Participant) =
    participant
    |> Participant.value
    |> sprintf "%s has won!"
    |> Twitter.postTweet |> ignore

  let rec loop (sleepTime:TimeSpan) = async {  
    
    let question = guessCapitalOfCountryQuestion()  
    let questionID = Twitter.postTweet question.Question

    do! Async.Sleep (sleepTime.TotalMilliseconds |> int)

    let replies = Twitter.grabReplies questionID

    let winner = 
      replies
      |> Array.map (fun r -> parseResponse r)
      |> Core.determineWinner question

    match winner with
    | None -> Twitter.postTweet "Nobody won." |> ignore
    | Some(winner) -> 
      announceWinner winner.Participant |> ignore
  
    return! loop(sleepTime)
  }

  loop sleepTime |> Async.Start

type BotService () =

  member this.Start () = 
    printfn "Started"

  member this.Stop () =
    printfn "Stopped"