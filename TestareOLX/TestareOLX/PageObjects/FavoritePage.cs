using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestareOLX.PageObjects
{
    public class FavoritePage
    {
        private IWebDriver driver;

        public FavoritePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string AdaugaLaFavorite()
        {
            var firstOffer = driver.FindElement(By.CssSelector(".gallerywide > :first-child"));

            var offerNameElement = firstOffer.FindElement(By.CssSelector(".gallerywide > :first-child .inner a"));
            string offerName = offerNameElement.FindElement(By.CssSelector(".gallerywide > :first-child  > .inner strong")).Text;

            Console.WriteLine("Numele ofertei adaugate la favorite:");

            Console.WriteLine(offerName);

            var favoriteButton = firstOffer.FindElement(By.CssSelector(".favtab"));
            favoriteButton.Click();

            Thread.Sleep(1000);

            //Nu vrem sa ne facem cont
            var nuMultumesc = driver.FindElement(By.CssSelector("[data-cy=\"search_results_button_close_observed_search_info_message\"]"));
            nuMultumesc.Click();

            return offerName;
        }

        public IWebElement FirstFavoriteItem => driver.FindElement(By.CssSelector(".offers td:first-child .title-cell span"));
        public IWebElement HeartRemoveButton => driver.FindElement(By.CssSelector(".removeObservedAd"));
        public IWebElement ListButton => driver.FindElement(By.Id("observedViewList"));
    }
}
