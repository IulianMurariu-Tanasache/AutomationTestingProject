using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestareOLX.PageObjects
{
    public class LoginPage
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public IWebElement UserInput => _driver.FindElement(By.Id("userEmail"));
        public IWebElement PassInput => _driver.FindElement(By.Id("userPass"));
        public IWebElement LoginButton => _driver.FindElement(By.Id("se_userLogin"));
    }
}
