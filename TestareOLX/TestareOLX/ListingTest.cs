using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace TestareOLX
{
    [TestClass]
    public class ListingTest
    {
        [TestMethod]
        public void TestSeePhoneNumber()
        {
            var driver = new ChromeDriver(); // open chrome browser
            driver.Manage().Window.Maximize(); // maximize the window
            driver.Navigate().GoToUrl("http://www.olx.ro"); // access the SUT(System Under Test) url. In our case http://a.testaddressbook.com/

            var cookieBtn = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            cookieBtn.Click();

            Thread.Sleep(2000);

            var telefoaneBtn = driver.FindElement(By.XPath("//a[@data-id='99']"));
            telefoaneBtn.Click();

            Thread.Sleep(500);

            var toateAnunturileBtn = driver.FindElement(By.XPath("//div[@class='subcategories-title']//a[@data-id='99']"));
            toateAnunturileBtn.Click();

            Thread.Sleep(2000);

            var firstAd = driver.FindElement(By.XPath("//div[@data-testid='listing-grid']/div[2]/a[@class='css-1bbgabe']"));
            Assert.IsNotNull(firstAd);
            firstAd.Click();

            Thread.Sleep(2000);

            var phoneLabelHidden = driver.FindElement(By.XPath("//h3[@class='css-11hr49z-Text eu5v0x0']"));
            Assert.AreEqual("xxx xxx xxx", phoneLabelHidden.Text);

            var showPhoneNumberBtn = driver.FindElement(By.XPath("//button[@data-testid='show-phone']"));
            showPhoneNumberBtn.Click();

            Thread.Sleep(2000);

            //Assert.ThrowsException<OpenQA.Selenium.NoSuchElementException>(() => driver.FindElement(By.XPath("//button[@data-testid='show-phone']")));

            var phoneNumberLabel = driver.FindElement(By.XPath("//a[@data-testid='contact-phone']"));
            Assert.IsNotNull(phoneNumberLabel);
            Assert.IsTrue(phoneNumberLabel.Text.Contains("07"));

            driver.Quit();
        }

        [TestMethod]
        public void TestCheckLocationIsSame()
        {
            var driver = new ChromeDriver(); // open chrome browser
            driver.Manage().Window.Maximize(); // maximize the window
            driver.Navigate().GoToUrl("http://www.olx.ro"); // access the SUT(System Under Test) url. In our case http://a.testaddressbook.com/

            var cookieBtn = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            cookieBtn.Click();

            Thread.Sleep(2000);

            var telefoaneBtn = driver.FindElement(By.XPath("//a[@data-id='99']"));
            telefoaneBtn.Click();

            Thread.Sleep(500);

            var toateAnunturileBtn = driver.FindElement(By.XPath("//div[@class='subcategories-title']//a[@data-id='99']"));
            toateAnunturileBtn.Click();

            Thread.Sleep(2000);

            var firstAd = driver.FindElement(By.XPath("//div[@data-testid='listing-grid']/div[2]/a[@class='css-1bbgabe']"));
            Assert.IsNotNull(firstAd);
            firstAd.Click();

            Thread.Sleep(2000);

            string[] oras = driver.FindElement(By.XPath("//div[@class='css-1nrl4q4']/div[1]/p[@class='css-7xdcwc-Text eu5v0x0']")).Text.Split(new string[] { ", " }, StringSplitOptions.None);
            string judet = driver.FindElement(By.XPath("//div[@class='css-1nrl4q4']/div[1]/p[@class='css-xl6fe0-Text eu5v0x0']")).Text;

            var mapBtn = driver.FindElement(By.XPath("//div[@class='qa-static-ad-map-container']"));
            mapBtn.Click();

            Thread.Sleep(2000);

            string locationName = driver.FindElement(By.XPath("//span[@class='css-1k9djcd']")).Text;
            Assert.IsTrue(locationName.Contains(judet));
            foreach(string s in oras)
                if(s != "")
                    Assert.IsTrue(locationName.Contains(s));

            driver.Quit();
        }
    }
}
