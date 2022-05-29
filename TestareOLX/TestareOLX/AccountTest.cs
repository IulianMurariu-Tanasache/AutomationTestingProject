using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestareOLX.Helpers;
using TestareOLX.PageObjects;
using TestareOLX.Shared;
using Xunit.Sdk;

namespace TestareOLX
{
    [TestClass]
    public class AccountTest
    {
        private IWebDriver _driver;
        private string _username;
        private string _badPass;
        private string _goodPass;
        private SharedObjects _shared;
        private LoginPage _loginPage;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();

            _shared = new SharedObjects(_driver);
            _loginPage = new LoginPage(_driver);

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.olx.ro/");

            _username = "yamxi101@gmail.com";
            _badPass = "nu e bun";
            _goodPass = "Ceamaitareparola123";
        }

        [TestMethod]
        public void MyAccountClickAfterLogin()
        {
            _shared.CookieButton.Click();
            _shared.AccountButton.Click();

            _loginPage.UserInput.SendKeys(_username);
            _loginPage.PassInput.SendKeys(_goodPass);

            _loginPage.LoginButton.Click();

            //returning to the home page to see where the website gets us 

            _driver.Navigate().GoToUrl("https://www.olx.ro/");

            _shared.AccountButton.Click();

            string url = _driver.Url.ToString();

            Assert.IsTrue(url.Contains("myaccount"));
        }

        [TestMethod]
        public void MyAccountClick()
        {
            _shared.CookieButton.Click();
            _shared.AccountButton.Click();

            Assert.IsNotNull(_loginPage.UserInput);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }
    }
}