using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TestCases.Pages;

namespace TestCases.Tests
{
    public class LogoutTests : BaseTestClass
    {
        [SetUp]
        public void Setup()
        {
            DefaultSetup();
            InitializeHomePage();
            InitializeLoginPage();
        }

        [Test]
        public void LogoutStudent()
        {
            LoginAsStudent();
            Logout();
            //todo:create actual method to check if logout by creating a message
            Assert.That(HomePage.LoginElementExist, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            DefaultTearDown();
        }
    }
}