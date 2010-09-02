using System;
using System.IO;
using Noesis.Javascript;

namespace HeadlessTestingDemo
{
    public class TestRunner
    {
        private readonly JavascriptContext context = new JavascriptContext();
        private readonly string projectPath;

        public TestRunner()
        {
            projectPath = Directory.GetCurrentDirectory().Replace(@"bin\Debug", "");

            LoadFile(@"Jasmine\env.therubyracer.js");
            LoadFile(@"Jasmine\window.js");
            LoadFile(@"Jasmine\jasmine.js");

            context.SetParameter("console", new SystemConsole());

            LoadFile(@"Scripts\jquery-1.4.2.js");
            LoadFile(@"Scripts\Roman.js");
            
            LoadFile(@"Specs\SpecHelper.js");
            LoadFile(@"Specs\RomanSpec.js");
        }

        public void LoadFile(string path)
        {
            var fullPath = Path.GetFullPath(projectPath + path);
            context.Run(File.ReadAllText(fullPath) + ";null;");
        }

        public void Run(ConsoleReporter reporter) 
        {
            context.SetParameter("dotNetReporter", reporter);
            context.Run(@"
              jasmine.JsApiReporter.prototype.reportSpecResults = function(spec) {
                this.totalSpecs += 1;
                if (spec.results().failedCount == 0) {
                  dotNetReporter.Passed(spec.getFullName());
                
                } else {
                  this.totalFailures += 1;
                
                  var errors = [];
                  var results = spec.results().getItems();
                  for (var i = 0; i < results.length; i++) {
                    if (results[i].trace.stack) {
                      errors.push(results[i].trace.stack);
                    }
                  }
                  dotNetReporter.Failed(spec.getFullName(), errors);
                }
              };

              var jasmineEnv = jasmine.getEnv();
              var reporter = new jasmine.JsApiReporter();
              reporter.totalSpecs = 0;
              reporter.totalFailures = 0;
              
              jasmineEnv.addReporter(reporter);
              jasmineEnv.execute();

              dotNetReporter.Finished();
            ");
        }

        public class SystemConsole
        {
            public void log(string message)
            {
                Console.WriteLine(message);
            }

            public void print(string message)
            {
                Console.Write(message);
            }
        }

        public class ConsoleReporter
        {
            public int TotalSpecs;
            public int TotalFailures;

            public void Passed(string name)
            {
                TotalSpecs++;

                Console.Write(".");
            }

            public void Failed(string name, object [] errors)
            {
                TotalSpecs++;
                TotalFailures++;

                Console.WriteLine("X\n{0}", name);
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
            }

            public void Finished()
            {
                Console.WriteLine("\n\n{0} specs run, {1} failures\n\n", TotalSpecs, TotalFailures);
            }
        }

        public static int Main()
        {
            var runner = new TestRunner();
            
            var reporter = new ConsoleReporter();
            runner.Run(reporter);
            
            return reporter.TotalFailures == 0 ? 0 : 1;
        }
    }
}
