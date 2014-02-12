using NUnit.Framework;
using OpenQA.Selenium;
using RichardsTestSuite.SetUp;

namespace RichardsTestSuite
{
    [SetUpFixture]
    public class DriverSetup
    {
        internal static IWebDriver Driver;

        [SetUp]
        public void StartTestServer()
        {
            Driver = new TestDriverFactory().CreateDriver();
        }

        [TearDown]
        public void StopTestServer()
        {
            Driver.Quit();
        }
    }
}
