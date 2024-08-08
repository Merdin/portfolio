using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace TestCases.Pages
{
    public class HomePage : BaseTestClass
    {
        public IWebElement LogoutElement => WebDriver.FindElement(LogoutElementId());
        public IWebElement LoginElement => WebDriver.FindElement(LoginElementId());
        public IWebElement ModuleElement => WebDriver.FindElement(ModuleElementId());


    public HomePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public bool LoginElementExist() => LoginElement.Displayed;
        public bool LogoutElementExist() => LogoutElement.Displayed;
        public bool ModuleElementExist() => ModuleElement.Displayed;

        public By LogoutElementId() => By.Id("logout");
        public By LoginElementId() => By.Id("login");
        public By ModuleElementId() => By.Id("modules");

        public void LoginClick()
        {
            LoginElement.Click();
        }

        public void LogoutClick()
        {
            LogoutElement.Click();
        }
    }
}