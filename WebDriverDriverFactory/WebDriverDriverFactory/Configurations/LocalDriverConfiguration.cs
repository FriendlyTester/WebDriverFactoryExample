using System;

namespace WebDriverDriverFactory.Config
{
    public class LocalDriverConfiguration
    {
        public string Browser { get; set; }

        public LocalDriverConfiguration(string browser)
        {
            Browser = browser;
        }
    }
}
