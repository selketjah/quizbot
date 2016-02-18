#load "Core.fs"
open QuizBot
open QuizBot.Core
#load "Twitter.fs"
//open QuizBot.Twitter

#I "../../packages"
#r @"FSharp.Data/lib/net40/FSharp.Data.dll"
#load "WorldBankQuestions.fs"
open QuizBot.WorldBankQuestions

let country, capital = getRandomCountryCapital ()
let q = { Question = country; ExpectedAnswer = capital }



open System

let sleepTime = TimeSpan(0,0,1)

let rec loop (sleepTime:TimeSpan) = async {
  printfn "started..."
  // create question
  let question = guessCapitalOfCountryQuestion()  
  // post question
  let questionID = Twitter.postTweet question.Question

  do! Async.Sleep (sleepTime.TotalMilliseconds |> int)
  // grab answers
  let answers = Twitter.grabReplies questionID

  answers |> List.iter (printfn "%A")
  // check winner
  
  return! loop(sleepTime)
}

loop sleepTime |> Async.Start