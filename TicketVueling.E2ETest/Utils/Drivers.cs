using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketVueling.E2ETest.Utils
{
    public class Drivers
    {
        public dynamic GetDriver(WebDrivers driver)
        {
            switch(driver)
            {
                case WebDrivers.Chrome:
                    return new ChromeDriver();
                case WebDrivers.Firefox:
                    return new FirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException(nameof(driver), driver, null);
            }
        }
    }
    public enum WebDrivers
    {
        Chrome,
        Firefox
    }
}
