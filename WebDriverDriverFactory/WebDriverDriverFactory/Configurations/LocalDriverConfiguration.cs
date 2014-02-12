using System;

namespace WebDriverDriverFactory.Config
{
    /// <summary>
    /// A LocalDriverConfiguration class.
    /// For a local instance we only require the browser's name
    /// </summary>
    public class LocalDriverConfiguration
    {
        public string Browser { get; set; }

        public LocalDriverConfiguration(string browser)
        {
            Browser = browser;
        }
    }
}
