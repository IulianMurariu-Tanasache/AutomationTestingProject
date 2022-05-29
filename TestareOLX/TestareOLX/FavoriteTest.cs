using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace TestareOLX
{
    [TestClass]
    public class FavoriteTest
    {
        [TestMethod]
        public void TestareAdaugareFavorite()
        {
            var driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://www.olx.ro/");


            //Accept cookies
            var cookieButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            cookieButton.Click();

            //Wait for the page to load
            Thread.Sleep(1000);

            //Aici apasam pe inima si dam nu multumesc cand ne cere sa facem cont
           var offerName = AdaugaLaFavorite(driver);


            //Prima oara cand dam click, butonul e ascuns, dar incercarea il forteaza sa drea scoll 
            //pana sus si afiseaza bunonul dupa il putem apasa
            var acceseazaListaFavorite = driver.FindElement(By.Id("observed-ads-link"));

            try
            { 
                acceseazaListaFavorite.Click();
                Thread.Sleep(1000);
            }
            catch (Exception e){}

            acceseazaListaFavorite.Click();


            //Incarca pagina 
            Thread.Sleep(1000);
			//Garanteaza ca e o lista

			try { 
                driver.FindElement(By.Id("observedViewList")).Click();
            }catch(Exception e) { }
            Thread.Sleep(1000);


            var ofertaFavorita = driver.FindElement(By.CssSelector(".offers td:first-child .title-cell span"));
            string numeOfertaFavorita = ofertaFavorita.Text;

            Console.WriteLine("Numele ofertei adaugate din lista de favorite:");
            Console.WriteLine(numeOfertaFavorita);

            Assert.IsTrue((numeOfertaFavorita == offerName));

            driver.Close();
        }

        [TestMethod]
        public void TestareStergereFavorite()
		{
            var driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://www.olx.ro/");


            //Accept cookies
            var cookieButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            cookieButton.Click();

            //Wait for the page to load
            Thread.Sleep(1000);

            //Aici apasam pe inima si dam nu multumesc cand ne cere sa facem cont
            var offerName = AdaugaLaFavorite(driver);

            //Prima oara cand dam click, butonul e ascuns, dar incercarea il forteaza sa drea scoll 
            //pana sus si afiseaza bunonul dupa il putem apasa
            var acceseazaListaFavorite = driver.FindElement(By.Id("observed-ads-link"));

            try
            {
                acceseazaListaFavorite.Click();
                Thread.Sleep(1000);
            }
            catch (Exception e) { }

            acceseazaListaFavorite.Click();

            //Incarca pagina 
            Thread.Sleep(1000);
            //Garanteaza ca e o lista

            try
            {
                driver.FindElement(By.Id("observedViewList")).Click();
            }
            catch (Exception e) { }
            Thread.Sleep(1000);
            //

            var stergeDinFavorite = driver.FindElement(By.CssSelector(".removeObservedAd"));
            stergeDinFavorite.Click();

            Thread.Sleep(1000);

            IWebElement ofertaFavorita = null;
            try 
            { 
                ofertaFavorita = driver.FindElement(By.CssSelector(".offers td:first-child .title-cell span"));
            }
			catch(Exception e){}

            Assert.IsNull(ofertaFavorita);

            driver.Close();
        }


        public string AdaugaLaFavorite(ChromeDriver driver)
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
    }
}
