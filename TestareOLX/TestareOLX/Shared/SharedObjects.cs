using OpenQA.Selenium;

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
        public IWebElement AllListingsButton => _driver.FindElement(AllListingsButtonBy);
        public IWebElement SearchButton => _driver.FindElement(SearchButtonBy);
        public IWebElement LocationInput => _driver.FindElement(By.Id("cityField"));

        public By SearchButtonBy => By.Id("submit-searchmain");
        public By AllListingsButtonBy => By.XPath("//div[@class='subcategories-title']/a[@data-id='99']");
        public By ElectronicsListingButtonDivBy => By.XPath("//div[@class='subcategories-list clr' and @style='display: block;']");
    }
}
