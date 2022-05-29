using OpenQA.Selenium;
using System;
using TestareOLX.Helpers;

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
            var offerNameElement = FirstOffer.FindElement(By.CssSelector(".gallerywide > :first-child .inner a"));
            string offerName = offerNameElement.FindElement(By.CssSelector(".gallerywide > :first-child  > .inner strong")).Text;

            Console.WriteLine("Numele ofertei adaugate la favorite:");

            Console.WriteLine(offerName);

            WaitHelpers.WaitForSeconds(1);

            var favoriteButton = FirstOffer.FindElement(By.CssSelector(".favtab"));
            favoriteButton.Click();


            //Nu vrem sa ne facem cont
            var nuMultumescBy = By.CssSelector("[data-cy=\"search_results_button_close_observed_search_info_message\"]");
            var nuMultumesc = driver.FindElement(nuMultumescBy);
            WaitHelpers.WaitForElementToBeVisibleCustom(driver, nuMultumescBy);
            nuMultumesc.Click();

            return offerName;
        }

        public IWebElement FirstFavoriteItem => driver.FindElement(FirstFavoriteItemBy);
        public IWebElement HeartRemoveButton => driver.FindElement(HeartRemoveButtonBy);
        public IWebElement ListButton => driver.FindElement(By.Id("observedViewList"));
        public IWebElement FirstOffer => driver.FindElement(FirstOfferBy);

        public By FirstOfferBy => By.CssSelector(".gallerywide > :first-child");
        public By HeartRemoveButtonBy => By.CssSelector(".removeObservedAd");
        public By FirstFavoriteItemBy => By.CssSelector(".offers td:first-child .title-cell span");
        public By HiddenFavoriteBy => By.CssSelector("body.homepage hasHiddenHeader");
    }
}
