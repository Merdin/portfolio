using NUnit.Framework;

namespace TestCases.Tests
{
    public class RoleTests : BaseTestClass
    {
        [SetUp]
        public void Setup()
        {
            DefaultSetup();
            InitializeHomePage();
            InitializeLoginPage();
        }
        
        [Test]
        public void ModulePageAsModulePrincipal()
        {
            LoginAsModulePrincipal();
            Assert.That(HomePage.ModuleElementExist, Is.True);
        }
        
        [Test]
        public void ModulePageAsModuleManager()
        {
            LoginAsModuleManager();
            Assert.That(HomePage.ModuleElementExist, Is.True);
        }
        
        [TearDown]
        public void TearDown()
        {
            DefaultTearDown();
        }
    }
}
