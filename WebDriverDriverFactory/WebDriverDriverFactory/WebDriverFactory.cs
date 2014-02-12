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
        //DesiredCapabilites is used to add specific browser requirements to RemoteWebDriver, such as version, OS.
        private DesiredCapabilities _capabilities;
        //Declare an instance of an IWebDriver to assign a browser driver to.
        //We use IWebDriver as all browsers use this interface, so all compatible.
        private IWebDriver _driver;

        /// <summary>
        /// This method will create a local driver, as it takes the LocalDriver configuation object
        /// </summary>
        /// <param name="configuration">An instance of the LocalDriverConfiguration object.</param>
        /// <returns>Driver that mets the configuration</returns>
        public IWebDriver Create(LocalDriverConfiguration configuration)
        {
            //A simple switch statement to determine which driver/service to create.
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

                //If a string isn't matched, it will default to FireFoxDriver
                default:
                    _driver = new FirefoxDriver();
                    break;
            }

            //Return the driver instance to the calling class.
            return _driver;
        }

        /// <summary>
        /// This method will create a remotedriver, as it takes the RemoteDriver configuation object
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public IWebDriver Create(RemoteDriverConfiguration configuration)
        {
            //Will need to construct the remoteServerUri so it can be passed to the remoteWebDriver.
            var remoteServer = BuildRemoteServer(configuration.SeleniumHubUrl, configuration.SeleniumHubPort);

            switch (configuration.Browser)
            {
                //Switch on browser name
                //Create a DesiredCapabilities for the required driver.
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

            //This method adds additional information to the desired capabilities, in this instance browser version and operating system.
            SetCapabilities(configuration.Platform, configuration.BrowserVersion);

            //We then create a new RemoteWebDriver with the Uri created earlier and the desired capabilities object.
            //This would then call your GRID instance and find a match and start the browser on the matching node.
            _driver = new RemoteWebDriver(new Uri(remoteServer), _capabilities);

            //Return the driver to the calling class.
            return _driver;
        }

        /// <summary>
        /// Adds additional required capabilities to the desiredCapabilities object.
        /// </summary>
        /// <param name="platform">OS enum, conversation is done at Configuration level.</param>
        /// <param name="browserVersion">Version number of browser, note that this is a string.</param>
        private void SetCapabilities(PlatformType platform, string browserVersion)
        {
            _capabilities.SetCapability(CapabilityType.Platform, new Platform(platform));
            _capabilities.SetCapability(CapabilityType.Version, browserVersion);
        }

        /// <summary>
        /// Build a Uri for your GRID Hub instance
        /// </summary>
        /// <param name="remoteServer">The hostname or IP address of your GRID instance, include the http://</param>
        /// <param name="remoteServerPort">Port of your GRID Hub instance</param>
        /// <returns>The correct Uri as a string</returns>
        private static string BuildRemoteServer(string remoteServer, int remoteServerPort)
        {
            return string.Format("{0}:{1}/wd/hub", remoteServer, remoteServerPort);
        }
    }
}