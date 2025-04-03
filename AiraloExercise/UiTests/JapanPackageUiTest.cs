using AiraloExercise.UiTests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AiraloExercise.UiTests
{
    public class JapanPackageUiTest
    {
        IWebDriver driver;
        BrowserActions browserActions = new BrowserActions();
        ScreenshotHelper screenshotHelper = new ScreenshotHelper();

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized"); 
            options.AddArgument("--disable-notifications"); 
            try
            {
                driver = new ChromeDriver(options);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting the driver: " + ex.Message);
            }

        }

        [Test]
        public void JapanPackage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string url = "https://www.airalo.com/";

            if (driver.WindowHandles.Any())
            {
                browserActions.NavigateToUrl(driver, url);
                browserActions.SearchForCountry(driver, "Japan");
                browserActions.SelectFirstPackage(driver);
                screenshotHelper.TakeScreenshot(driver, "screenshot");
            }
            else
            {
                Assert.Fail("Browser wasn't opened.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error during Quit: " + ex.Message);
                }
            }
        }
    }
}