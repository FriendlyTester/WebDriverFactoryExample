using OpenQA.Selenium;

namespace WebDriverDriverFactory.Config
{
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
