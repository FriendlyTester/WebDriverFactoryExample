using OpenQA.Selenium;
using WebDriverDriverFactory.Common;
using WebDriverDriverFactory.Config;

namespace RichardsTestSuite.SetUp
{
    /// <summary>
    /// Calls the WebDriver factory by creating required configuration based on the remote value.
    /// </summary>
    public class TestDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            //If remote is true, then create a RemoteDriverConfig and pass it to the factory
            if (TestConfiguration.Remote)
            {
                return new WebDriverFactory().Create(
                    new RemoteDriverConfiguration(
                            TestConfiguration.Browser,
                            TestConfiguration.Platform,
                            TestConfiguration.BrowserVersion,
                            TestConfiguration.SeleniumHubUrl,
                            TestConfiguration.SeleniumHubPort));
            }

            //Else (false) create a LocalDriverConfig and pass this to the factory
            return new WebDriverFactory().Create(
                new LocalDriverConfiguration(
                    TestConfiguration.Browser));
        }

    }
}
