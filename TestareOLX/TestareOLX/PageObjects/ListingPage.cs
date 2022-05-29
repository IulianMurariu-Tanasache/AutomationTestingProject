using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestareOLX.PageObjects
{
    internal class ListingPage
    {
        private IWebDriver driver;

        public ListingPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement PhoneLabelHidden => driver.FindElement(PhoneLabelHiddenBy);
        public IWebElement ShowPhoneNumberButton => driver.FindElement(By.XPath("//button[@data-testid='show-phone']"));
        public IWebElement PhoneNumberLabel => driver.FindElement(PhoneLabelBy);
        public IWebElement CityLabel => driver.FindElement(By.XPath("//div[@class='css-1nrl4q4']/div[1]/p[@class='css-7xdcwc-Text eu5v0x0']"));
        public IWebElement CountyLabel => driver.FindElement(By.XPath("//div[@class='css-1nrl4q4']/div[1]/p[@class='css-xl6fe0-Text eu5v0x0']"));
        public IWebElement MapButton => driver.FindElement(By.XPath("//div[@class='qa-static-ad-map-container']"));

        public By PhoneLabelHiddenBy => By.XPath("//h3[@class='css-11hr49z-Text eu5v0x0']");
        public By PhoneLabelBy => By.XPath("//a[@data-testid='contact-phone']");
    }
}
