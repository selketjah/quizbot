namespace QuizBot

[<RequireQualifiedAccess>]
module Twitter =

  open System
  open System.Configuration
  open System.Threading
  open System.Threading.Tasks
  open System.Text.RegularExpressions
  open LinqToTwitter
    
  type Response = { 
    MessageId:uint64
    ScreenName:string
    Message:string
    Timestamp:DateTime
  }

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

  let removeBotHandle text = 
    Regex.Replace(text, "@QuizBowlBot", "", RegexOptions.IgnoreCase)

  let postTweet text =
    let message = 
      text
      |> trimToTweet

    let status = 
      Task.Run<Status>(fun _ -> 
        text
        |> context.TweetAsync)

    (uint64) status.Id

  let grabReplies (id:uint64) =
    let tweets =
      query {
        for tweet in context.Status do
        where (tweet.Type = StatusType.Mentions && tweet.InReplyToStatusID = id)
        select tweet }
      |> Seq.toArray

    tweets
    |> Array.map(fun t -> 
      {   MessageId = t.StatusID
          ScreenName = t.User.ScreenNameResponse 
          Message = t.Text
          Timestamp = t.CreatedAt })