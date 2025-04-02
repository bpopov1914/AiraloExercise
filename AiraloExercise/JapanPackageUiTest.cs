using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}