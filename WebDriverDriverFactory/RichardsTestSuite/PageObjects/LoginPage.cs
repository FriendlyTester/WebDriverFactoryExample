using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace RichardsTestSuite.PageObjects
{
    /// <summary>
    /// A PageObject for the Hotmail LoginPage, using the PageObjects suport class.
    /// </summary>
    public class LoginPage
    {
        /// <summary>
        /// Note that I have used IWebDriver.
        /// As mentioned in the DriverFactory this is because all drivers are contracted to this interface.
        /// So if you only use methods available on the Interface, then they will also be available on all drivers.
        /// Therefore allowing your page object to be used by any Driver.
        /// </summary>
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "i0116")]
        private IWebElement TxtUsername { get; set; }

        [FindsBy(How = How.Id, Using = "i0118")]
        private IWebElement TxtPassword { get; set; }

        [FindsBy(How = How.Id, Using = "idSIButton9")]
        private IWebElement BtnLogin { get; set; }

        [FindsBy(How = How.Id, Using = "idTd_Tile_ErrorMsg_Login")]
        private IWebElement LblLoginErrorMessage { get; set; }

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("i0116")).Displayed);
        }

        public void PopulateUsername(string username)
        {
            TxtUsername.SendKeys(username);
        }

        public void PopulatePassword(string password)
        {
            TxtPassword.SendKeys(password);
        }

        public LoginPage ClickLoginExpectingError()
        {
            BtnLogin.Click();
            return new LoginPage(_driver);
        }

        public string ReadLoingErrorMessage()
        {
            return LblLoginErrorMessage.Text;
        }
    }
}
