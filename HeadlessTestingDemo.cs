using System.Linq;
using NUnit.Framework;
using Noesis.Javascript.Headless;
using Noesis.Javascript.Headless.Reporters;

namespace HeadlessTestingDemo
{
    [TestFixture]
    public class HeadlessTestingDemo
    {
        [Test, TestCaseSource("JasmineResults")]
        public void Expect(string testName, string errors)
        {
            Assert.That(errors, Is.Null);
        }

        public object [] JasmineResults
        {
            get
            {
                var testRunner = new JavaScriptTestRunner();
                testRunner.Include(JavaScriptLibrary.jQuery_1_6_4_min);
                testRunner.Include(JavaScriptLibrary.Jasmine_1_1_0);

                // load the "Roman.js" file from the resource specified in <T>, which happens to be this assembly but could be in another project.
                testRunner.LoadFromResource<HeadlessTestingDemo>("Roman.js");
                testRunner.LoadFromResource<HeadlessTestingDemo>("RomanSpec.js");
                testRunner.LoadFromResource<HeadlessTestingDemo>("SpecHelper.js");

                var testReporter = new ResultsReporter();
                testRunner.RunJasmineSpecs(testReporter);
                return testReporter.Results
                    .Select(x => new[] {x.Key, x.Value})
                    .ToArray();
            }
        }
    }
}