using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiraloExercise.UiTests.Utils
{
    class BrowserActions
    {
        public void NavigateToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void SearchForCountry(IWebDriver driver, string searchText)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement searchIcon = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath("//span[@class='airalo-icon-search']")));
            IWebElement searchBox = driver.FindElement(By.XPath("//input[@data-testid='search-input']"));
            searchBox.SendKeys(searchText);
            IWebElement japanLocalOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath("//span[@data-testid='Japan-name']")));
            japanLocalOption.Click();
            
        }

        public void SelectFirstPackage(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement packagesContainer = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath(".//div[@class='package-list-wrapper']")));

            IWebElement firstPackageButton = driver.FindElement(By.XPath("//a[@href='/japan-esim/moshi-moshi-7days-1gb']//button[@type='button']"));
            firstPackageButton.Click();

        }
    }
}
