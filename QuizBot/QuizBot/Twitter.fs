namespace QuizBot

[<RequireQualifiedAccess>]
module Twitter =

  let postTweet question =

    printfn "%s" question
    
    let id = 1234UL

    id

  let grabReplies (id:uint64) =
      // ID of the response, username, answer
      [
        1UL, "@joe", "Brussels"
        2UL, "@jack", "Paris"
      ]