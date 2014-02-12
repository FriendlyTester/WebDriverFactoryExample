using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;

namespace RichardsTestSuite.SetUp
{
    /// <summary>
    /// Configuration object
    /// This would usually also contain other test specific config values required to be read from app.config
    /// </summary>
    public class TestConfiguration
    {
        public static bool Remote { get; set; }
        public static string Browser { get; set; }
        public static PlatformType Platform { get; set; }
        public static string BrowserVersion { get; set; }
        public static string SeleniumHubUrl { get; set; }
        public static int SeleniumHubPort { get; set; }

        public static string ApplicationUrl { get; set; }

        //Because this is static it will be executed at the start of any test run.
        //Reads the keys from the app.config and assigns them to the properties declared above.
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

        /// <summary>
        /// This method converts the OS string in the app.config to the matching value in the PlatformType Enum.
        /// </summary>
        /// <returns>A PlatformType instance</returns>
        private static PlatformType GetPlatformType()
        {
            var reader = new AppSettingsReader();
            var platformValue = (string)reader.GetValue("Platform", typeof(string));

            PlatformType platform;
            return Enum.TryParse(FirstCharToUpper(platformValue), out platform) ? platform : PlatformType.Windows;
        }

        /// <summary>
        /// In order to match the enum will have to Title case the OS string value.
        /// This is more protection for when someone enters the OS all lowercase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("The string was null or empty.");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1)).ToLower();
        }
    }
}