using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TicketVueling.E2ETest.Utils;
[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace TicketVueling.E2E.Test
{
    [TestClass]
    public class UnitTest1
    {
        public static IWebDriver _firefoxDriver;
        public static IWebDriver _chromeDriver;
        Model test = new Model();

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            Drivers driver = new Drivers();
            _chromeDriver = driver.GetDriver(WebDrivers.Chrome);
            _chromeDriver.Url = "https://tickets.vueling.com/";

            _firefoxDriver = driver.GetDriver(WebDrivers.Firefox);
            _firefoxDriver.Url = "https://tickets.vueling.com/";
        }

        [TestMethod]
        public async Task TestChrome()
        {
            WebDriverWait wait = new WebDriverWait(_chromeDriver, TimeSpan.FromSeconds(5));
            var url = (_chromeDriver.Url == "https://tickets.vueling.com/ScheduleSelectNew.aspx");

            test.AcceptCookies(_chromeDriver, 10); 
            test.SelectRadioButton(_chromeDriver, TripType.OneWay, 10);
            await test.SelectOriginCity(_chromeDriver, "Barcelona", 10);
            await test .SelectDestinyCity(_chromeDriver, "Bruselas", 10);
            test.SelectDate(_chromeDriver);
            test.SelectAdults(_chromeDriver, 1, 10);
            //this.SelectExtraSeat(_firefoxDriver, true, ExtraSeat.TwoExtraSeat);
            test.SearchFly(_chromeDriver, 10);
            test.SelectPrice(_chromeDriver, 0, 10);
            test.SelectTarifas(_chromeDriver, 7, Tarifas.Optimus);
        }

        [TestMethod]
        public async Task TestFireFox()
        {
            WebDriverWait wait = new WebDriverWait(_firefoxDriver, TimeSpan.FromSeconds(5));
            var url = (_firefoxDriver.Url == "https://tickets.vueling.com/ScheduleSelectNew.aspx");

            test.AcceptCookies(_firefoxDriver, 10);
            test.SelectRadioButton(_firefoxDriver, TripType.OneWay, 10);
            await test.SelectOriginCity(_firefoxDriver, "Barcelona", 10);
            await test.SelectDestinyCity(_firefoxDriver, "Bruselas", 10);
            test.SelectDate(_firefoxDriver);
            test.SelectAdults(_firefoxDriver, 1, 10);
            //this.SelectExtraSeat(_firefoxDriver, true, ExtraSeat.TwoExtraSeat);
            test.SearchFly(_firefoxDriver, 10);
            test.SelectPrice(_firefoxDriver, 0, 10);
            test.SelectTarifas(_firefoxDriver, 7, Tarifas.Optimus);
        }

        [TestCleanup]
        public void TearDown()
        {
            _chromeDriver.Close();
            _chromeDriver.Quit();
        }

        
    }
}