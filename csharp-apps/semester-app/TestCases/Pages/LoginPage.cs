using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestCases.Pages
{
    public class LoginPage : BaseTestClass
    {
        public IWebElement txtUsername => WebDriver.FindElement(By.Id("username"));
        public IWebElement txtPassword => WebDriver.FindElement(By.Id("password"));
        public IWebElement btnLogin => WebDriver.FindElement(By.Id("submitlogin"));

        public LoginPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public void Login(string userName, string password)
        {
            txtUsername.SendKeys(userName);
            txtPassword.SendKeys(password);
            btnLogin.Submit();
        }
    }
}