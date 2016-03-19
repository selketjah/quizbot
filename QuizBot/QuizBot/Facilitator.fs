namespace QuizBot

module Facilitator = 

  open System
  open System.Text.RegularExpressions
  open QuizBot.Participant
  open QuizBot.Core
  open QuizBot.WorldBankQuestions
  open QuizBot.Twitter

  let sleepTime = TimeSpan(0,0,1)

  let announceWinner (participant:Participant) =
    participant
    |> Participant.value
    |> sprintf "%s has won!"
    |> Twitter.postTweet |> ignore

  let rec loop (sleepTime:TimeSpan) = async {  
    
    printfn "New question"

    let question = guessCapitalOfCountryQuestion()  
    let questionID = Twitter.postTweet question.Question

    do! Async.Sleep (sleepTime.TotalMilliseconds |> int)

    let replies = Twitter.grabReplies questionID

    let winner = 
      replies
      |> Array.map (fun r -> 
        { Participant = Participant r.ScreenName
          Timestamp = r.Timestamp
          Answer = r.Message })
      |> Core.determineWinner question

    printfn "%A" winner

    match winner with
    | None -> Twitter.postTweet "Nobody won." |> ignore
    | Some(winner) -> 
      announceWinner winner.Participant |> ignore
  
    return! loop(sleepTime)
  }

  loop sleepTime |> Async.Start

type BotService () =

  member this.Start () = 

    let time = System.TimeSpan(0,0,10)
    Facilitator.loop time 
    |> Async.Start

    printfn "Started"

  member this.Stop () =
    printfn "Stopped"