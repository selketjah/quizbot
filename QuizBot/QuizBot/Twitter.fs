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

  let unixEpoch = DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)

  let fromUnix (unixTime:int) =
    unixTime
    |> float
    |> TimeSpan.FromSeconds
    |> unixEpoch.Add

  let prettyTime (unixTime:int) = 
    (unixTime |> fromUnix).ToShortTimeString()
  
  let timeframe = 15. 
  let safetyBuffer = 5. |> TimeSpan.FromSeconds

  let delayUntilNextCall (context:TwitterContext) =
    let delay =
        if context.RateLimitRemaining > 0
        then
            timeframe / (float context.RateLimitCurrent)
            |> TimeSpan.FromMinutes
        else
            let nextReset = 
                context.RateLimitReset
                |> float
                |> TimeSpan.FromSeconds
                |> unixEpoch.Add
            nextReset - DateTime.UtcNow
    delay.Add safetyBuffer

  let postTweet text =
    let message = 
      text
      |> trimToTweet

    let status = 
      Task.Run<Status>(fun _ -> 
        text
        |> context.TweetAsync)

    let tweet = status.Result
    
    printfn "Rate:  total:%i remaining:%i reset:%s" context.RateLimitCurrent context.RateLimitRemaining (context.RateLimitReset |> prettyTime)

    tweet.StatusID

  let grabReplies (id:uint64) =
    let tweets =
      query {
        for tweet in context.Status do
        where (tweet.Type = StatusType.Mentions && tweet.InReplyToStatusID = id)
        select tweet }
      |> Seq.toArray

    tweets
    |> Array.map(fun t -> 
      { MessageId = t.StatusID
        ScreenName = t.User.ScreenNameResponse 
        Message = removeBotHandle t.Text
        Timestamp = t.CreatedAt })