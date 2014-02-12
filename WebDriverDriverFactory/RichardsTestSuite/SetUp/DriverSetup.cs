using NUnit.Framework;
using OpenQA.Selenium;
using RichardsTestSuite.SetUp;

namespace RichardsTestSuite
{
    /// <summary>
    /// This will Setup a driver instance for us at the start of each Fixture.
    /// NUnit will does this because the name space of this class is the highest therefore all tests fall under it.
    /// </summary>
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
