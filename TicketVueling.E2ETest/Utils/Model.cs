using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicketVueling.E2ETest.Utils
{
    public class Model
    {
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

        public async Task SelectOriginCity(IWebDriver webdriver, string city, double waitTime)
        {
            WebDriverWait wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(waitTime));
            wait.Until(driver => driver.FindElement(By.Id("AvailabilitySearchInputSearchView_TextBoxMarketOrigin1")));
            await Task.Delay(1000);
            this.SelectTypeCity(webdriver, "origin");
            await Task.Delay(1000);
            this.WriteBox(webdriver, "AvailabilitySearchInputSearchView_TextBoxMarketOrigin1", city);
        }

        public async Task SelectDestinyCity(IWebDriver webdriver, string city, double waitTime)
        {
            WebDriverWait wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(waitTime));
            wait.Until(driver => driver.FindElement(By.Id("AvailabilitySearchInputSearchView_TextBoxMarketDestination1")));
            await Task.Delay(1000);
            this.SelectTypeCity(webdriver, "destination");
            await Task.Delay(1000);
            this.WriteBox(webdriver, "AvailabilitySearchInputSearchView_TextBoxMarketDestination1", city);
        }

        public void SelectDate(IWebDriver webdriver)
        {
            Thread.Sleep(1000);
            webdriver.FindElement(By.XPath("/html/body/div[11]/div/div/div[1]/table/tbody/tr[5]/td[4]/a")).Click();
        }

        public void SelectTypeCity(IWebDriver webdriver, string typeCity)
        {
            Thread.Sleep(1000);
            webdriver.FindElement(By.Id(typeCity)).Click();
        }

        public void WriteBox(IWebDriver webdriver, string id, string place)
        {
            Thread.Sleep(1000);
            //sendkeys nos hace una simulación de lo que escribimos en el elemento, y tab para pasar al siguiente elemento
            //en este caso pasar de caja de origen a destino.
            webdriver.FindElement(By.Id(id)).SendKeys(place);
            Thread.Sleep(1000);
            webdriver.FindElement(By.Id(id)).SendKeys(Keys.Tab);
        }

        public void SelectRadioButton(IWebDriver webdriver, TripType num, double waitTime)
        {
            WebDriverWait wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(waitTime));

            wait.Until(driver => driver.FindElement(By.Id("radiosBuscador")));
            Thread.Sleep(1000);
            //el findElements nos devuelve un array de elementos. Como en nuestro caso teníamos los tres radiobuttons con la misma clase
            //y nos devuelve un array con los tres. Usamos un enum para coger el que queramos (no un int como hacía yo antes mindundi)
            var radio = webdriver.FindElements(By.ClassName("elForm_radio--label"));
            radio[(int)num].Click();
        }

        public void SelectAdults(IWebDriver webdriver, int adults, double waitTime)
        {
            Thread.Sleep(1000);
            var adultsBtn = webdriver.FindElements(By.ClassName("adt_select_button"));
            adultsBtn[adults].Click();
        }

        public void SelectExtraSeat(IWebDriver webdriver, bool check, ExtraSeat numSeat)
        {

            if (check == true)
            {
                webdriver.FindElement(By.Id("isExtraSeat")).Click();
                var extraSeats = webdriver.FindElements(By.Id("extraSeatOptionsList"));
                extraSeats[(int)numSeat].Click();
            }
        }
        public void SearchFly(IWebDriver webdriver, double waitTime)
        {
            webdriver.FindElement(By.Id("divButtonBuscadorNormal")).Click();
        }

        public void SelectPrice(IWebDriver webdriver, int indexPrice, double waitTime)
        {
            WebDriverWait wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(waitTime));

            wait.Until(driver => driver.FindElement(By.Id("flightCardContent")));


            var flights = webdriver.FindElements(By.Id("flightCardsContainer"));
            var flight = flights[indexPrice];
            flight.FindElement(By.Id("justPrice")).Click();
        }

        public void SelectTarifas(IWebDriver webdriver, double waitTime, Tarifas numTarifa)
        {
            //var tarifas = webdriver.FindElements(By.ClassName("fares-box"));
            //var tarifa = tarifas[(int)numTarifa];

            webdriver.FindElement(By.Id("optimaFareBox")).FindElement(By.ClassName("fares-box_radio")).Click();
            webdriver.FindElement(By.Id("stvContinueButton")).Click();
        }
    }
}
