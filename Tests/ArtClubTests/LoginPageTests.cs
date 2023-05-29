
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ArtClubTests
{
    [TestClass]
    public class LoginPageTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // driver.Navigate().GoToUrl("URL_OF_LOGIN_PAGE");
            driver.Navigate().GoToUrl("https://localhost:7036/Identity/Account/Login");
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestLoginPage()
        {
            // Fill in the login form fields
            var emailInput = driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys("john_Admin1@gmail.com");

            var passwordInput = driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys("john_Admin1@gmail.com");

            var loginButton = driver.FindElement(By.Id("login-submit"));
            loginButton.Click();

            // Wait for the page to navigate or for an element on the next page to appear
           
           var nextPageElement = wait.Until(ExpectedConditions.ElementExists(By.Id("https://localhost:7036/")));
            // Assert any necessary conditions on the next page
            Assert.IsNotNull(nextPageElement);
           
        }
    }
}
