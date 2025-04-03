using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiraloExercise.UiTests.Utils
{
    public class ScreenshotHelper
    {
        public void TakeScreenshot(IWebDriver driver, string screenshotName)
        {           
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{screenshotName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            screenshot.SaveAsFile(filePath);
            Console.WriteLine($"Screenshot saved to: {filePath}");            
        }
    }
}
