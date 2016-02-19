namespace QuizBot

[<RequireQualifiedAccess>]
module Twitter =

  open System
  open System.Configuration
  open System.Threading
  open System.Threading.Tasks
  open LinqToTwitter
    
  let appSettings = ConfigurationManager.AppSettings
  let apiKey = appSettings.["apiKey"]
  let apiSecret = appSettings.["apiSecret"]
  let accessToken = appSettings.["accessToken"]
  let accessTokenSecret = appSettings.["accessTokenSecret"]

  let context = 
    let credentials = SingleUserInMemoryCredentialStore()
    credentials.ConsumerKey <- apiKey
    credentials.ConsumerSecret <- apiSecret
    credentials.AccessToken <- accessToken
    credentials.AccessTokenSecret <- accessTokenSecret
    let authorizer = SingleUserAuthorizer()
    authorizer.CredentialStore <- credentials
    new TwitterContext(authorizer)

  let trimToTweet (msg:string) =
    if msg.Length > 140 
    then msg.Substring(0,134) + " [...]"
    else msg

  let postTweet question =
    let message = 
      question
      |> trimToTweet

    let status = 
      Task.Run<Status>(fun _ -> 
        question
        |> context.TweetAsync)

    status.Id

  let grabReplies (id:uint64) =
      // ID of the response, username, answer
      [
        1UL, "@joe", "Brussels"
        2UL, "@jack", "Paris"
      ]