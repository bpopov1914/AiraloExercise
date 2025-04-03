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
            Dictionary<string, string> expectedPackageDetails = new Dictionary<string, string>
        {
            { "Title", "Moshi Moshi" },
            { "Coverage", "Japan" },
            { "Data", "1 GB" },
            { "Validity", "7 Days" },
            { "Price", "$4.50 USD" }
        };

            if (driver.WindowHandles.Any())
            {
                browserActions.NavigateToUrl(driver, url);
                browserActions.SearchForCountry(driver, "Japan");
                browserActions.ChangeCurrentcy(driver);
                browserActions.SelectFirstPackage(driver);
                Dictionary<string, string> japanPackageDetails = browserActions.GetPackageInfo(driver);

                Assert.That(japanPackageDetails, Is.Not.Null, "No package details returned.");
                screenshotHelper.TakeScreenshot(driver, "screenshot");

                foreach (var key in japanPackageDetails.Keys)
                {
                    Assert.That(japanPackageDetails[key], Is.EqualTo(expectedPackageDetails[key]),
                        $"Mismatch for key: {key}. Expected: {expectedPackageDetails[key]}, but got: {japanPackageDetails[key]}");
                }
                                
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