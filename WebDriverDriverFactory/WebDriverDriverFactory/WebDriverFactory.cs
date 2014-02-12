using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using WebDriverDriverFactory.Config;

namespace WebDriverDriverFactory.Common
{
    public class WebDriverFactory
    {
        private DesiredCapabilities _capabilities;
        private IWebDriver _driver;

        public IWebDriver Create(LocalDriverConfiguration configuration)
        {
            switch (configuration.Browser)
            {
                case "chrome":
                    _driver = new ChromeDriver(@"DriverServices");
                    break;

                case "internet explorer":
                    var options = new InternetExplorerOptions { EnableNativeEvents = false };
                    _driver = new InternetExplorerDriver(@"DriverServices", options);
                    break;

                case "firefox":
                    _driver = new FirefoxDriver();
                    break;

                case "phantomjs":
                    _driver = new PhantomJSDriver(@"DriverServices");
                    break;

                default:
                    _driver = new FirefoxDriver();
                    break;
            }

            return _driver;
        }

        public IWebDriver Create(RemoteDriverConfiguration configuration)
        {
            var remoteServer = BuildRemoteServer(configuration.SeleniumHubUrl, configuration.SeleniumHubPort);

            switch (configuration.Browser)
            {
                case "firefox":
                    _capabilities = DesiredCapabilities.Firefox();
                    break;

                case "chrome":
                    _capabilities = DesiredCapabilities.Chrome();
                    break;

                case "internet explorer":
                    _capabilities = DesiredCapabilities.InternetExplorer();
                    break;
            }

            SetCapabilities(configuration.Platform, configuration.BrowserVersion);

            _driver = new RemoteWebDriver(new Uri(remoteServer), _capabilities);

            return _driver;
        }

        private void SetCapabilities(PlatformType platform, string browserVersion)
        {
            _capabilities.SetCapability(CapabilityType.Platform, new Platform(platform));
            _capabilities.SetCapability(CapabilityType.Version, browserVersion);
        }

        private static string BuildRemoteServer(string remoteServer, int remoteServerPort)
        {
            return string.Format("{0}:{1}/wd/hub", remoteServer, remoteServerPort);
        }
    }
}