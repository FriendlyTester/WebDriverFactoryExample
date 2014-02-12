using OpenQA.Selenium;

namespace WebDriverDriverFactory.Config
{
    /// <summary>
    /// A RemoteDriverConfiguration class
    /// Used to construct driver requirements to send to the GRID Hub
    /// </summary>
    public class RemoteDriverConfiguration
    {
        public string Browser { get; set; }

        public PlatformType Platform { get; set; }

        public string BrowserVersion { get; set; }

        public string SeleniumHubUrl { get; set; }

        public int SeleniumHubPort { get; set; }

        public RemoteDriverConfiguration(string browser, PlatformType platform, string browserVersion, string seleniumHubUrl, int seleniumHubPort)
        {
            Browser = browser;
            Platform = platform;
            BrowserVersion = browserVersion;
            SeleniumHubUrl = seleniumHubUrl;
            SeleniumHubPort = seleniumHubPort;
        }
    }
}
