using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TicketVueling.E2ETest.Utils;

namespace TicketVueling.E2E.Test
{
    [TestClass]
    public class UnitTest1
    {
        public static IWebDriver _firefoxDriver;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _firefoxDriver = new FirefoxDriver();
            _firefoxDriver.Url = "https://tickets.vueling.com/";
        }

        [TestMethod]
        public void Test1()
        {
            WebDriverWait wait = new WebDriverWait(_firefoxDriver, TimeSpan.FromSeconds(5));
            var url = (_firefoxDriver.Url == "https://tickets.vueling.com/ScheduleSelectNew.aspx");

            this.AcceptCookies(_firefoxDriver, 3);
            this.SelectRadioButton(_firefoxDriver, TripType.OneWay, 3);
            this.SelectOriginCity(_firefoxDriver, "Barcelona");
            this.SelectDestinyCity(_firefoxDriver, "Bruselas");
            this.SelectDate(_firefoxDriver);
            this.SelectAdults(_firefoxDriver, 1);
            //this.SelectExtraSeat(_chromeDriver, true, ExtraSeat.TwoExtraSeat);
            this.SearchFly(_firefoxDriver);
            this.SelectPrice(_firefoxDriver, 0);
            this.SelectTarifas(_firefoxDriver, 7, Tarifas.Optimus);
        }

        [TestCleanup]
        public void TearDown()
        {
            _firefoxDriver.Close();
            _firefoxDriver.Quit();
        }

        public void AcceptCookies(IWebDriver webdriver, double waitTime)
        {
            //todo el tema del waittime es para dar tiempo entre paso y paso ya que a veces va rápido y no se pueden apreciar 
            WebDriverWait wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(waitTime));
            wait.Until(driver => driver.FindElement(By.Id("ensBannerDescription")));

            var button = webdriver.FindElement(By.Id("ensCloseBanner"));

            if (button.Displayed)
            {
                button.Click(); //Click para simular que da click al botón (como es lógico)
            }
        }

        private void SelectOriginCity(IWebDriver webdriver, string city)
        {
            this.SelectTypeCity(webdriver, "origin");
            this.WriteBox(webdriver, "AvailabilitySearchInputSearchView_TextBoxMarketOrigin1", city);
        }

        private void SelectDestinyCity(IWebDriver webdriver, string city)
        {
            this.SelectTypeCity(webdriver, "destination");
            this.WriteBox(webdriver, "AvailabilitySearchInputSearchView_TextBoxMarketDestination1", city);
        }

        private void SelectDate(IWebDriver webdriver)
        {
            webdriver.FindElement(By.Id("datePickerTitleCloseButton")).Click();
            //*[@id="datePickerContainer"]/div[1]/table/tbody/tr[5]/td[1]/a
        }

        private void SelectTypeCity(IWebDriver webdriver, string typeCity)
        {
            webdriver.FindElement(By.Id(typeCity)).Click();
        }

        private void WriteBox(IWebDriver webdriver, string id, string place)
        {
            //sendkeys nos hace una simulación de lo que escribimos en el elemento, y tab para pasar al siguiente elemento
            //en este caso pasar de caja de origen a destino.
            webdriver.FindElement(By.Id(id)).SendKeys(place);
            webdriver.FindElement(By.Id(id)).SendKeys(Keys.Tab);
        }
        
        private void SelectRadioButton(IWebDriver webdriver,TripType num, double waitTime)
        {
            WebDriverWait wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(waitTime));

            wait.Until(driver => driver.FindElement(By.Id("radiosBuscador")));
            
            //el findElements nos devuelve un array de elementos. Como en nuestro caso teníamos los tres radiobuttons con la misma clase
            //y nos devuelve un array con los tres. Usamos un enum para coger el que queramos (no un int como hacía yo antes mindundi)
            var radio = webdriver.FindElements(By.ClassName("elForm_radio--label"));
            radio[(int)num].Click();
        }

        private void SelectAdults(IWebDriver webdriver, int adults)
        {
            var adultsBtn = webdriver.FindElements(By.ClassName("adt_select_button"));
            adultsBtn[adults].Click();
        }

        private void SelectExtraSeat(IWebDriver webdriver, bool check, ExtraSeat numSeat)
        {
            if (check == true)
            {
                webdriver.FindElement(By.Id("isExtraSeat"));
                var extraSeats = webdriver.FindElements(By.Id("extraSeatOptionsList"));
                extraSeats[(int)numSeat].Click();
            }
        }
        private void SearchFly(IWebDriver webdriver)
        {
            webdriver.FindElement(By.Id("divButtonBuscadorNormal")).Click();
        }

        private void SelectPrice(IWebDriver webdriver, int indexPrice)
        {
            var flights = webdriver.FindElements(By.Id("flightCardsContainer"));
            var flight = flights[indexPrice];
            flight.FindElement(By.Id("justPrice")).Click();
        }

        private void SelectTarifas(IWebDriver webdriver, double waitTime, Tarifas numTarifa)
        {
            //var tarifas = webdriver.FindElements(By.ClassName("fares-box"));
            //var tarifa = tarifas[(int)numTarifa];

            webdriver.FindElement(By.Id("optimaFareBox")).FindElement(By.ClassName("fares-box_radio")).Click();
            webdriver.FindElement(By.Id("stvContinueButton")).Click();
        }
    }
}