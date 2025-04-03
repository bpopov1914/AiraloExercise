using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AiraloExercise.UiTests.Utils
{
    class BrowserActions
    {
        public void NavigateToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void ChangeCurrentcy(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement currency = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath(".//span[@data-testid='€ EUR-header-language']")));
            currency.Click();
            IWebElement usdCurrency = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath(".//a[@data-testid='USD-currency-select']")));
            usdCurrency.Click();
            IWebElement updateButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath(".//button[@data-testid='UPDATE-button']")));
            updateButton.Click();
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

        public Dictionary<string, string> GetPackageInfo(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Dictionary<string, string> packageInfo = new Dictionary<string, string>();

            IWebElement title = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath(".//div[@data-testid='sim-detail-operator-title']/p")));
            IWebElement coverage = driver.FindElement(By.XPath(".//div[@class='sim-detail-top']//p[@data-testid='COVERAGE-value']"));
            IWebElement data = driver.FindElement(By.XPath(".//div[@class='sim-detail-top']//p[@data-testid='DATA-value']"));
            IWebElement validity = driver.FindElement(By.XPath(".//div[@class='sim-detail-top']//p[@data-testid='VALIDITY-value']"));
            IWebElement price = driver.FindElement(By.XPath(".//div[@class='sim-detail-top']//p[@data-testid='PRICE-value']"));

            packageInfo.Add("Title", title.GetAttribute("innerText"));
            packageInfo.Add("Coverage", coverage.GetAttribute("innerText"));
            packageInfo.Add("Data", data.GetAttribute("innerText"));
            packageInfo.Add("Validity", validity.GetAttribute("innerText"));
            packageInfo.Add("Price", price.GetAttribute("innerText"));

            return packageInfo;
        }
    }
}
