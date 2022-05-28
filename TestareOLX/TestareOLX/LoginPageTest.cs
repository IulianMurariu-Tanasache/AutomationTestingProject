using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace TestareOLX
{
    [TestClass]
    public class LogInPageTest
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

            _username = "wrong@gmail.com";
            _badPass = "nu e bun";
            _goodPass = "este bun";
        }


        [TestMethod]
        public void IncorrectLoginCreditentials()
        {
            var cookieButton = _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            var loginButton = _driver.FindElement(By.Id("my-account-link"));

            cookieButton.Click();
            loginButton.Click();

            var TxtEmail = _driver.FindElement(By.Id("userEmail"));
            var TxtPassword = _driver.FindElement(By.Id("userPass"));

            TxtEmail.SendKeys(_username);
            TxtPassword.SendKeys(_badPass);

            var loginButton2 = _driver.FindElement(By.Id("se_userLogin"));
            loginButton2.Click();
           
            //daca exista erori atunci avem un test bun
            string url = _driver.Url.ToString();

            Console.WriteLine(url);

            Assert.AreNotEqual("https://www.olx.ro/",url);

            Thread.Sleep(2000);
        }

        [TestMethod]
        public void LogInSuccessful()
        {
            var cookieButton = _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            var loginButton = _driver.FindElement(By.Id("my-account-link"));

            cookieButton.Click();
            loginButton.Click();

            var TxtEmail = _driver.FindElement(By.Id("userEmail"));
            var TxtPassword = _driver.FindElement(By.Id("userPass"));

            TxtEmail.SendKeys(_username);
            TxtPassword.SendKeys(_goodPass);

            var loginButton2 = _driver.FindElement(By.Id("se_userLogin"));
            loginButton2.Click();

            //daca exista erori atunci avem un test bun
            string url = _driver.Url.ToString();

            Console.WriteLine(url);

            Assert.AreEqual("https://www.olx.ro/", url);

            Thread.Sleep(2000);
        }

    }
}