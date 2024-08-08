using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TestCases.Pages;

namespace TestCases
{
    public class BaseTestClass
    {
        protected IWebDriver WebDriver;
        protected HomePage HomePage;
        protected LoginPage LoginPage;
        private WebDriverWait _waitDriver;

        private void InitializeFirefoxDriver()
        {
            WebDriver = new FirefoxDriver();
        }

        protected void DefaultSetup()
        {
            InitializeFirefoxDriver();
            InitializeWaitDriver();
            NavigateToDomain();
        }

        protected void DefaultTearDown()
        {
            DestroySession();
        }

        protected void DestroySession()
        {
            WebDriver.Quit();
        }

        protected void InitializeWaitDriver()
        {
            _waitDriver = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(2));
        }

        protected void NavigateToDomain()
        {
            WebDriver.Navigate().GoToUrl("https://test.studym8.nl");
        }

        protected void InitializeHomePage()
        {
            HomePage = new HomePage(WebDriver);
        }

        protected void InitializeLoginPage()
        {
            LoginPage = new LoginPage(WebDriver);
        }

        private void LoginAs(string username, string password)
        {
            HomePage.LoginClick();
            LoginPage.Login(username, password);
            WaitForPresenceOfElement(HomePage.LogoutElementId());
        }

        protected void LoginAsStudent()
        {
            LoginAs("student@studym8.nl", "9&y-~h#N");
        }

        protected void LoginAsStudyCareerCounselor()
        {
            LoginAs("studycareercounselor@studym8.nl", "N!M>g//9");
        }

        protected void LoginAsModulePrincipal()
        {
            LoginAs("moduleprincipal@studym8.nl", "U%tq7JPd");
        }

        protected void LoginAsModuleManager()
        {
            LoginAs("modulemanager@studym8.nl", "^KMMg49s");
        }

        protected void LoginAsExamCommitteeMember()
        {
            LoginAs("examcommittemember@studym8.nl", "%Uh>YM^4");
        }

        protected void Logout()
        {
            HomePage.LogoutClick();
            WaitForPresenceOfElement(HomePage.LoginElementId());
        }

        private void WaitForPresenceOfElement(By locator)
        {
            _waitDriver.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }
    }
}