using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestareOLX.Shared
{
    public class SharedObjects
    {
        private IWebDriver _driver;

        public SharedObjects(IWebDriver browser)
        {
            _driver = browser;
        }

        public IWebElement CookieButton => _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
        public IWebElement AccountButton => _driver.FindElement(By.Id("my-account-link"));
        public IWebElement HeartButton => _driver.FindElement(By.Id("observed-ads-link"));
        public IWebElement ElectronicsListingsButton => _driver.FindElement(By.XPath("//a[@data-id='99']"));
        public IWebElement AllListingsButton => _driver.FindElement(By.XPath("//div[@class='subcategories-title']//a[@data-id='99']"));
        public IWebElement SearchButton => _driver.FindElement(By.Id("submit-searchmain"));
        public IWebElement LocationInput => _driver.FindElement(By.Id("cityField"));
    }
}
