using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
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

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.olx.ro/");

            _username = "yamxi101@gmail.com";
            _badPass = "nu e bun";
            _goodPass = "Ceamaitareparola123";
        }


        [TestMethod]
        public void MyAccountClickAfterLogin()
        {
            var cookieButton = _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            var accountButton = _driver.FindElement(By.Id("my-account-link"));

            cookieButton.Click();
            accountButton.Click();

            var TxtEmail = _driver.FindElement(By.Id("userEmail"));
            var TxtPassword = _driver.FindElement(By.Id("userPass"));

            TxtEmail.SendKeys(_username);
            TxtPassword.SendKeys(_goodPass);

            var loginButton = _driver.FindElement(By.Id("se_userLogin"));
            loginButton.Click();

            //returning to the home page to see where the website gets us 
            Thread.Sleep(4000);

            _driver.Navigate().GoToUrl("https://www.olx.ro/");

            accountButton = _driver.FindElement(By.Id("my-account-link"));

            accountButton.Click();

            string url = _driver.Url.ToString();

            Assert.AreEqual("https://www.olx.ro/d/myaccount/", url);

            Thread.Sleep(2000);

            _driver.Quit();
        }

        [TestMethod]
        public void MyAccountClick()
        {
            var cookieButton = _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            var accountButton = _driver.FindElement(By.Id("my-account-link"));

            cookieButton.Click();
            accountButton.Click();

            var TxtEmail = _driver.FindElement(By.Id("userEmail"));

            Assert.IsNotNull(TxtEmail);

            _driver.Quit();

        }
    }
}