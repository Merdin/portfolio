using NUnit.Framework;

namespace TestCases.Tests
{
    public class LoginTests : BaseTestClass
    {
        [SetUp]
        public void Setup()
        {
            DefaultSetup();
            InitializeHomePage();
            InitializeLoginPage();
        }

        [Test]
        public void LoginStudent()
        {
            LoginAsStudent();
            Assert.That(HomePage.LogoutElementExist, Is.True);
        }

        [Test]
        public void LoginTestStudyCareerCounselor()
        {
            LoginAsStudyCareerCounselor();
            Assert.That(HomePage.LogoutElementExist, Is.True);
        }

        [Test]
        public void LoginModulePrincipal()
        {
            LoginAsModulePrincipal();
            Assert.That(HomePage.LogoutElementExist, Is.True);
        }

        [Test]
        public void LoginModuleManager()
        {
            LoginAsModuleManager();
            Assert.That(HomePage.LogoutElementExist, Is.True);
        }

        [Test]
        public void LoginTestExamCommitteeMember()
        {
            LoginAsExamCommitteeMember();
            Assert.That(HomePage.LogoutElementExist, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            DefaultTearDown();
        }
    }
}