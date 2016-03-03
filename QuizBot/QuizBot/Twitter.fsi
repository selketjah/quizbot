namespace QuizBot

module Twitter =

 open System

 type Response = { 
   MessageId:uint64
   ScreenName:string
   Message:string
   Timestamp:DateTime 
 }

 val postTweet: string -> uint64

 val grabReplies: uint64 -> Response[]