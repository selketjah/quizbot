namespace QuizBot

module Facilitator = 

  open System
  open System.Text.RegularExpressions
  open QuizBot.Participant
  open QuizBot.Core
  open QuizBot.Questions
  open QuizBot.Twitter

  let pause = TimeSpan(0,10,0)

  let announceWinner (participant:Participant) =
    participant
    |> Participant.value
    |> sprintf "@%s has won!"
    |> Twitter.postTweet |> ignore

  let rec loop (sleepTime:TimeSpan) = async {  
    
    let question = Questions.next ()  
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
    | None -> Twitter.postTweet "Nobody won. :-(" |> ignore
    | Some(winner) -> 
      announceWinner winner.Participant |> ignore
  
    //do! Async.Sleep (pause.TotalMilliseconds |> int)

    return! loop(sleepTime)
  }

type BotService () =

  member this.Start () = 

    let time = System.TimeSpan(0,2,0)
    Facilitator.loop time 
    |> Async.Start

    printfn "Started"

  member this.Stop () =
    printfn "Stopped"