using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace TestareOLX
{
    [TestClass]
    public class SearchTest
    {
        private string localitate = "Drobeta-Turnu Severin";
        private string judet = "Mehedinti";
        private int minPrice = 69;
        private int maxPrice = 6969;

        [TestMethod]
        public void TestPriceSearch()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.olx.ro/");

            var searchButton = driver.FindElement(By.Id("submit-searchmain"));
            var cookieButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));

            cookieButton.Click();
            searchButton.Click();


            Thread.Sleep(2000);

            var minPriceButton = driver.FindElement(By.XPath("//input[@data-testid='range-from-input']"));
            var maxPriceButton = driver.FindElement(By.XPath("//input[@data-testid='range-to-input']"));

            minPriceButton.SendKeys(minPrice.ToString());
            maxPriceButton.SendKeys(maxPrice.ToString());

            Thread.Sleep(2000);


            var anunturiList = driver.FindElements(By.CssSelector("div.css-19ucd76"));

            foreach(var anunt in anunturiList)
            {
                IWebElement pretElement;
                try
                {
                    pretElement = anunt.FindElement(By.CssSelector("a > div > div > div.css-9nzgu8 > div.css-u2ayx9 > p"));
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

                    var pret = int.Parse(pretString);

                    Assert.IsTrue(pret >= minPrice && pret <= maxPrice);
                }


            }

            driver.Quit();
        }

        [TestMethod]
        public void TestLocationSearch()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.olx.ro/");

            var searchButton = driver.FindElement(By.Id("submit-searchmain"));
            var cookieButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));

            cookieButton.Click();

            var locationInput = driver.FindElement(By.Id("cityField"));
            locationInput.SendKeys($"{localitate}, {judet}");

            Thread.Sleep(1000);

            searchButton.Click();

            Thread.Sleep(2000);

            var anunturiList = driver.FindElements(By.CssSelector("div.css-19ucd76"));

            foreach (var anunt in anunturiList)
            {
                IWebElement locationElement;
                try
                {
                    locationElement = anunt.FindElement(By.XPath("//p[@data-testid='location - date']"));
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

            driver.Quit();
        }
    }
}
