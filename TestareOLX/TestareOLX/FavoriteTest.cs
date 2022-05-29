using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using TestareOLX.PageObjects;
using TestareOLX.Shared;

namespace TestareOLX
{
    [TestClass]
    public class FavoriteTest
    {

        private IWebDriver _driver;
        private SharedObjects _shared;
        private FavoritePage _favoritePage;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();

            _shared = new SharedObjects(_driver);
            _favoritePage = new FavoritePage(_driver);

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.olx.ro/");
        }

        [TestMethod]
        public void TestareAdaugareFavorite()
        {
            //Accept cookies
            _shared.CookieButton.Click();

            //Wait for the page to load
            Thread.Sleep(1000);

            //Aici apasam pe inima si dam nu multumesc cand ne cere sa facem cont
            var offerName = _favoritePage.AdaugaLaFavorite();

            //Prima oara cand dam click, butonul e ascuns, dar incercarea il forteaza sa drea scoll 
            //pana sus si afiseaza bunonul dupa il putem apasa
            try
            { 
                _shared.HeartButton.Click();
                Thread.Sleep(1000);
            }
            catch (Exception e){}

            _shared.HeartButton.Click();

            //Incarca pagina 
            Thread.Sleep(1000);
			//Garanteaza ca e o lista

			try {
                _favoritePage.ListButton.Click();
            }catch(Exception e) { }

            Thread.Sleep(1000);

            string numeOfertaFavorita = _favoritePage.FirstFavoriteItem.Text;

            Console.WriteLine("Numele ofertei adaugate din lista de favorite:");
            Console.WriteLine(numeOfertaFavorita);

            Assert.IsTrue((numeOfertaFavorita == offerName));
        }

        [TestMethod]
        public void TestareStergereFavorite()
		{
            _shared.CookieButton.Click();

            //Wait for the page to load
            Thread.Sleep(1000);

            //Aici apasam pe inima si dam nu multumesc cand ne cere sa facem cont
            var offerName = _favoritePage.AdaugaLaFavorite();

            //Prima oara cand dam click, butonul e ascuns, dar incercarea il forteaza sa drea scoll 
            //pana sus si afiseaza bunonul dupa il putem apasa
            //Prima oara cand dam click, butonul e ascuns, dar incercarea il forteaza sa drea scoll 
            //pana sus si afiseaza bunonul dupa il putem apasa
            try
            {
                _shared.HeartButton.Click();
                Thread.Sleep(1000);
            }
            catch (Exception e) { }

            _shared.HeartButton.Click();

            //Incarca pagina 
            Thread.Sleep(1000);
            //Garanteaza ca e o lista
            try
            {
                _favoritePage.ListButton.Click();
            }
            catch (Exception e) { }
            Thread.Sleep(1000);

            _favoritePage.HeartRemoveButton.Click();

            Thread.Sleep(1000);

            IWebElement ofertaFavorita = null;
            try 
            { 
                ofertaFavorita = _favoritePage.FirstFavoriteItem;
            }
			catch(Exception e){}

            Assert.IsNull(ofertaFavorita);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }

    }
}
