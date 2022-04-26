using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SelenmiumFirstProject
{
    public class SimpleApplicationRunner
    {
        public static void Main(string[] args)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://github.com");

            string searchPhrase = "selenium";

            IWebElement searchInput = driver.FindElement(By.Name("q"));
            searchInput.SendKeys(searchPhrase);
            searchInput.SendKeys(Keys.Enter);

            IList<String> actualItems = driver
                .FindElements(By.ClassName("v-align-middle"))
                .Where(xx => xx.TagName.Equals("a"))
                .Select(xx => xx.Text.ToLower())
                .ToList();

            foreach(String item in actualItems)
            {
                Console.WriteLine(item);
            }

            Assert.True(actualItems.All(yy => yy.ToLower().Contains(searchPhrase)));

            driver.Quit();

        }
    }
}