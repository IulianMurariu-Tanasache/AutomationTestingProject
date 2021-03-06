using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestareOLX.Helpers;
using TestareOLX.PageObjects;
using TestareOLX.Shared;

namespace TestareOLX
{
    [TestClass]
    public class SearchTest
    {
        private string localitate = "Drobeta-Turnu Severin";
        private string judet = "Mehedinti";
        private int minPrice = 69;
        private int maxPrice = 6969;
        private IWebDriver _driver;
        private SharedObjects _shared;
        private ListingsPage _listPage;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();

            _shared = new SharedObjects(_driver);
            _listPage = new ListingsPage(_driver);

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.olx.ro/");
        }

        [TestMethod]
        public void TestPriceSearch()
        {
            _shared.CookieButton.Click();
            _shared.SearchButton.Click();

            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _listPage.MinPriceBy);

            _listPage.MinPriceInput.SendKeys(minPrice.ToString());
            WaitHelpers.WaitForSeconds(0.5);
            _listPage.MaxPriceInput.SendKeys(maxPrice.ToString());
            
            _listPage.SearchButton2.Click();
            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _listPage.SearchButton2By);

            foreach(var anunt in _listPage.AdsList)
            {
                IWebElement pretElement;
                try
                {
                    pretElement = _listPage.PriceTag(anunt);
                }
                catch
                {
                    pretElement = null;
                }


                if(pretElement != null)
                {
                    var pretString = pretElement.Text;
                    pretString = pretString.Replace(" ", "");
                    pretString = pretString.Replace("lei", "");
                    pretString = pretString.Replace("\r\nPrețulenegociabil", "");
                    pretString = pretString.Replace("Gratuit", "0");
                    pretString = pretString.Replace("Schimb", "");
                    pretString = pretString.Replace(",", ".");

                    var pret = double.Parse(pretString);

                    Assert.IsTrue(pret >= minPrice && pret <= maxPrice);
                }
            }
        }

        [TestMethod]
        public void TestLocationSearch()
        {
            _shared.CookieButton.Click();
            _shared.LocationInput.SendKeys($"{localitate}, {judet}");
            WaitHelpers.WaitForSeconds(0.5);

            _shared.SearchButton.Click();
            WaitHelpers.WaitForElementToBeVisibleCustom(_driver, _listPage.SearchButton2By);

            foreach (var anunt in _listPage.AdsList)
            {
                IWebElement locationElement;
                try
                {
                    locationElement = _listPage.LocationText(anunt);
                }
                catch
                {
                    locationElement = null;
                }


                if (locationElement != null)
                {
                    var locationString = locationElement.Text;

                    var ok = locationString.Contains(localitate);

                    Assert.IsTrue(ok);

                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }
    }
}
