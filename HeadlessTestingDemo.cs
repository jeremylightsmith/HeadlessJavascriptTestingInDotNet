using NUnit.Framework;
using Noesis.Javascript.Headless;
using Noesis.Javascript.Headless.Reporters;

namespace HeadlessTestingDemo
{
    [TestFixture]
    public class HeadlessTestingDemo
    {
        [Test]
        public void Then_all_of_our_specs_will_pass()
        {
            var testRunner = new JavaScriptTestRunner();
            testRunner.Include(JavaScriptLibrary.jQuery_1_6_4_min);
            testRunner.Include(JavaScriptLibrary.Jasmine_1_1_0);

            // load the "Roman.js" file from the resource specified in <T>, which happens to be this assembly but could be in another project.
            testRunner.LoadFromResource<HeadlessTestingDemo>("Roman.js");
            testRunner.LoadFromResource<HeadlessTestingDemo>("RomanSpec.js");
            testRunner.LoadFromResource<HeadlessTestingDemo>("SpecHelper.js");

            var stringReporter = new StringReporter();

            testRunner.RunJasmineSpecs(stringReporter);

            Assert.That(stringReporter.TotalFailures, Is.EqualTo(0), stringReporter.Result);
        }
    }
}