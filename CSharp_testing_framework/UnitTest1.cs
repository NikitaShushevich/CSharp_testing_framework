using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SimpleTestFramework
{
    public class GoogleHomePage
    {
        private readonly IWebDriver driver;

        public GoogleHomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Open()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
        }

        public void Search(string query)
        {
            IWebElement searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys(query);
            searchBox.Submit();
        }

        public bool IsResultPageLoaded()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(d => d.Title.StartsWith("Google Search", StringComparison.OrdinalIgnoreCase));
        }
    }

    [TestFixture]
    public class GoogleSearchTest
    {
        private IWebDriver driver;
        private GoogleHomePage googleHomePage;

        [SetUp]
        public void Setup()
        {
            // Set up the ChromeDriver
            driver = new ChromeDriver();
            googleHomePage = new GoogleHomePage(driver);
        }

        [Test]
        public void GoogleSearch()
        {
            // Open Google homepage
            googleHomePage.Open();

            // Perform a search
            googleHomePage.Search("OpenAI");

            // Wait for the results page to load
            Assert.IsTrue(googleHomePage.IsResultPageLoaded());
        }

        [TearDown]
        public void Teardown()
        {
            // Close the browser
            driver.Quit();
        }
    }
}
