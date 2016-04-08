using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using QuizBot;

namespace QuizBot.Service {
  class Program {
    static void Main(string[] args) {
      HostFactory.Run(x =>                  
      {
        x.Service<QuizBot.BotService>(s => 
        {
          s.ConstructUsing(name => new BotService());
          s.WhenStarted(tc => tc.Start());
          s.WhenStopped(tc => tc.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("Quiz bot bowl");
        x.SetDisplayName("Quizbot");
        x.SetServiceName("QuizBot");
      });
    }
  }
}
