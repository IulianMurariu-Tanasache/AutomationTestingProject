using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TestareOLX.Helpers;
using TestareOLX.PageObjects;
using TestareOLX.Shared;

namespace TestareOLX
{

    // daca nu au numar de telefon, testele pica!!!
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

            _shared.ElectronicsListingsButton.Click();
            //WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _shared.ElectronicsListingButtonDivBy);
            WaitHelpers.WaitForSeconds(0.5);

            _shared.AllListingsButton.Click();
            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _listPage.FirstItemBy);

            _listPage.FirstItem.Click();
            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _listingPage.PhoneLabelHiddenBy);

            Assert.AreEqual("xxx xxx xxx", _listingPage.PhoneLabelHidden.Text);

            _listingPage.ShowPhoneNumberButton.Click();
            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _listingPage.PhoneLabelBy);

            Assert.IsNotNull(_listingPage.PhoneNumberLabel);
            Assert.IsTrue(_listingPage.PhoneNumberLabel.Text.Contains("07"));
        }

        [TestMethod]
        public void TestCheckLocationIsSame()
        {
            _shared.CookieButton.Click();

            _shared.ElectronicsListingsButton.Click();
            //WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _shared.ElectronicsListingButtonDivBy);
            WaitHelpers.WaitForSeconds(0.5);

            _shared.AllListingsButton.Click();
            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _listPage.FirstItemBy);

            _listPage.FirstItem.Click();
            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _listingPage.PhoneLabelHiddenBy);

            string[] oras = _listingPage.CityLabel.Text.Split(new string[] { "," }, StringSplitOptions.None);
            string judet = _listingPage.CountyLabel.Text;

            _listingPage.MapButton.Click();

            var locationNameBy = By.XPath("//span[@class='css-1k9djcd']");
            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, locationNameBy);

            string locationName = _driver.FindElement(locationNameBy).Text;
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
