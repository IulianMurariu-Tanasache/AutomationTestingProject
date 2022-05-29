using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using TestareOLX.PageObjects;
using TestareOLX.Shared;

namespace TestareOLX
{
    [TestClass]
    public class ListingTest
    {
        private IWebDriver _driver;
        private SharedObjects _shared;
        private ListingsPage _listPage;
        private ListingPage _listingPage;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();

            _shared = new SharedObjects(_driver);
            _listPage = new ListingsPage(_driver);
            _listingPage = new ListingPage(_driver);

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.olx.ro/");
        }

        [TestMethod]
        public void TestSeePhoneNumber()
        {
            _shared.CookieButton.Click();
            Thread.Sleep(2000);

            _shared.ElectronicsListingsButton.Click();  
            Thread.Sleep(500);

            _shared.AllListingsButton.Click();
            Thread.Sleep(2000);

            _listPage.FirstItem.Click();
            Thread.Sleep(2000);

            Assert.AreEqual("xxx xxx xxx", _listingPage.PhoneLabelHidden.Text);

            _listingPage.ShowPhoneNumberButton.Click();
            Thread.Sleep(2000);

            Assert.IsNotNull(_listingPage.PhoneNumberLabel);
            Assert.IsTrue(_listingPage.PhoneNumberLabel.Text.Contains("07"));
        }

        [TestMethod]
        public void TestCheckLocationIsSame()
        {
            _shared.CookieButton.Click();
            Thread.Sleep(2000);

            _shared.ElectronicsListingsButton.Click();
            Thread.Sleep(500);

            _shared.AllListingsButton.Click();
            Thread.Sleep(2000);

            _listPage.FirstItem.Click();
            Thread.Sleep(2000);

            string[] oras = _listingPage.CityLabel.Text.Split(new string[] { "," }, StringSplitOptions.None);
            string judet = _listingPage.CountyLabel.Text;

            _listingPage.MapButton.Click();  
            Thread.Sleep(2000);

            string locationName = _driver.FindElement(By.XPath("//span[@class='css-1k9djcd']")).Text;
            Assert.IsTrue(locationName.Contains(judet));
            foreach(string s in oras)
                if(s != "" || s != " ")
                    Assert.IsTrue(locationName.Contains(s));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }
    }
}
