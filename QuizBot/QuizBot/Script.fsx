#load "Core.fs"
open QuizBot.Core

#I "../packages"
#r @"FSharp.Data.2.2.5\lib\net40\FSharp.Data.dll"
#load "WorldBankQuestions.fs"
open QuizBot.WorldBankQuestions

let country, capital = getRandomCountryCapital ()
let q = { Question = country; ExpectedAnswer = capital }

