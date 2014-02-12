using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;

namespace RichardsTestSuite.SetUp
{
    public class TestConfiguration
    {
        public static bool Remote { get; set; }
        public static string Browser { get; set; }
        public static PlatformType Platform { get; set; }
        public static string BrowserVersion { get; set; }
        public static string SeleniumHubUrl { get; set; }
        public static int SeleniumHubPort { get; set; }

        public static string ApplicationUrl { get; set; }

        static TestConfiguration()
        {
            var reader = new AppSettingsReader();

            Remote = (bool)reader.GetValue("Remote", typeof(bool));
            
            Browser = (string)reader.GetValue("Browser", typeof(string));

            if (Remote)
            {
                BrowserVersion = (string)reader.GetValue("BrowserVersion", typeof(string));
                Platform = GetPlatformType();
                SeleniumHubUrl = (string)reader.GetValue("SeleniumHubUrl", typeof(string));
                SeleniumHubPort = (int)reader.GetValue("SeleniumHubPort", typeof(int));
            }

            ApplicationUrl = (string)reader.GetValue("ApplicationUrl", typeof(string));
        }

        private static PlatformType GetPlatformType()
        {
            var reader = new AppSettingsReader();
            var platformValue = (string)reader.GetValue("Platform", typeof(string));

            PlatformType platform;
            return Enum.TryParse(FirstCharToUpper(platformValue), out platform) ? platform : PlatformType.Windows;
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("The string was null or empty.");
            return input.First().ToString(CultureInfo.InvariantCulture).ToUpper() + String.Join("", input.Skip(1)).ToLower();
        }
    }
}