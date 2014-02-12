using OpenQA.Selenium;
using WebDriverDriverFactory.Common;
using WebDriverDriverFactory.Config;

namespace RichardsTestSuite.SetUp
{
    public class TestDriverFactory
    {
        public IWebDriver CreateDriver()
        {
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

            return new WebDriverFactory().Create(
                new LocalDriverConfiguration(
                    TestConfiguration.Browser));
        }

    }
}
