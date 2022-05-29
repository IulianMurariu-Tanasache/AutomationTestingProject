using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestareOLX.PageObjects
{
    internal class ListingsPage
    {
        private IWebDriver driver;

        public ListingsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement FirstItem => driver.FindElement(FirstItemBy);
        public IWebElement SearchButton2 => driver.FindElement(SearchButton2By);
        public IWebElement MinPriceInput => driver.FindElement(MinPriceBy);
        public IWebElement MaxPriceInput => driver.FindElement(By.XPath("//input[@data-testid='range-to-input']"));
        public IReadOnlyCollection<IWebElement> AdsList => driver.FindElements(By.CssSelector("div.css-19ucd76"));
        public IWebElement PriceTag(IWebElement ad) => ad.FindElement(By.CssSelector("a > div > div > div.css-9nzgu8 > div.css-u2ayx9 > p"));
        public IWebElement LocationText(IWebElement ad) => ad.FindElement(By.XPath("//p[@data-testid='location - date']"));

        public By FirstItemBy => By.XPath("//div[@data-testid='listing-grid']/div[2]/a[@class='css-1bbgabe']");
        public By MinPriceBy => By.XPath("//input[@data-testid='range-from-input']");

        public By SearchButton2By => By.Name("searchBtn");
    }
}
