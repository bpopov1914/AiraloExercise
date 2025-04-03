using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AiraloExercise
{
    public class JapanPackageUiTest
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized"); //Opens the browser in full screen mode
            options.AddArgument("--disable-notifications"); //Deactivate pending notifications TODO: Check if this is working
            driver = new ChromeDriver(options);
            
        }

        [Test]
        public void JapanPackage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            if (driver.WindowHandles.Any())
            {
                driver.Navigate().GoToUrl("https://www.airalo.com/");
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