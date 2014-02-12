using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using RichardsTestSuite.SetUp;
using RichardsTestSuite.PageObjects;

namespace RichardsTestSuite.Tests
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = DriverSetup.Driver;
            _driver.Navigate().GoToUrl(TestConfiguration.ApplicationUrl);
        }

        [Test]
        public void BadEmailAddressTest()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.PopulateUsername("richardbradshaw123@hotmail.com");
            loginPage.PopulatePassword("password");
            loginPage = loginPage.ClickLoginExpectingError();

            Assert.That(loginPage.ReadLoingErrorMessage(), Is.EqualTo("That Microsoft account doesn't exist. Enter a different email address or get a new account."));
        }
    }
}
