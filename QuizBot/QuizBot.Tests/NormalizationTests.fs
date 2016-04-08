module NormalizationTests

  open System
  open Xunit
  open Swensen.Unquote
  open QuizBot.Normalization

  [<Fact>]
  let ``Given response with trailing whitespace Then returns trimmed and decap string`` () =
    let expected = "änswer"
    let actual = cleanText "ÄnsWer "
    test <@ expected = actual @>

  [<Fact>]
  let ``Given response with double whitespace Then returns trimmed and decap string without double whitespace`` () =
    let expected = "whîtespaced answer"
    let actual = cleanText "WhîteSpaced  anSwer "
    test <@ expected = actual @>
