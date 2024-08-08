using Microsoft.VisualStudio.TestTools.UnitTesting;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PitchPerfectHearingTest.test
{
    [TestClass]
    public class AuthenticationTests
    {
        [TestInitialize]
        public void Setup()
        {
            new UserRepository(new ApplicationContext()).Create(new User()
            {
                Username = "Luke".ToLower(),
                Password = new EncryptionProvider().Encrypt("Skywalker"),
                EmailAdres = "Luke@pitchperfect.nl".ToLower(),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            });
        }

        [TestMethod]
        public void CreateUserTest()
        {
            User createUser = new User()
            {
                Username = "Merdin".ToLower(),
                Password = new EncryptionProvider().Encrypt("Test123"),
                EmailAdres = "Merdin@pitchperfect.nl".ToLower(),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
            new UserRepository(new ApplicationContext()).Create(createUser);

            var getAllUsers = new UserRepository(new ApplicationContext()).GetAll().ToList();
            var merdinUsername = (from item in getAllUsers where item.Username.Equals(createUser.Username) select item.Username).Single();


            Assert.AreEqual(createUser.Username, merdinUsername);
        }

        [TestMethod]
        public void AuthenticateUserTest()
        {
            var authProvider = new AuthenticationProvider();
            var getAll = new UserRepository(new ApplicationContext()).GetAll().ToList();
            var luke = (from item in getAll where item.Username.Equals("Luke".ToLower()) select item).Single();

            PasswordBox password = null;
            var t = new Thread(() => { password = new PasswordBox() { Password = new EncryptionProvider().Decrypt(luke.Password) }; });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            var authenticatedUser = authProvider.AuthenticateUser(luke.Username, password);

            Assert.IsTrue(authenticatedUser.IsValidated);
            Assert.AreEqual(luke, authenticatedUser.AuthenticatedUser.User);
            Assert.IsTrue(Application.Current.Properties.Contains("SessionToken"));
            Assert.IsTrue(Application.Current.Properties.Contains("AuthenticatedUser"));
        }

        [TestMethod]
        public void ResetPasswordTest()
        {
            var getAll = new UserRepository(new ApplicationContext()).GetAll().ToList();
            var luke = (from item in getAll where item.Username.Equals("Luke".ToLower()) select item).Single();
            var lukeOldPassword = luke.Password;

            var authProvider = new AuthenticationProvider();
            authProvider.ResetPassword(luke.Username.ToLower(), luke.EmailAdres.ToLower());

            var getAllUsers = new UserRepository(new ApplicationContext()).GetAll().ToList();
            var lukeNewPassword = (from item in getAllUsers where item.Username.Equals("Luke".ToLower()) select item.Password).Single();

            string decryptedPassword = new EncryptionProvider().Decrypt(lukeNewPassword);

            Assert.AreEqual(AuthenticationProvider.DefaultPassword, decryptedPassword);
            Assert.AreNotEqual(lukeOldPassword, decryptedPassword);
        }

        [TestMethod]
        public void UserDoesntExistTest()
        {
            var username = "A user that doesn't exist";
            PasswordBox password = null;
            var t = new Thread(() => { password = new PasswordBox() { Password = "test123" }; });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            var authProvider = new AuthenticationProvider();
            var exception = Assert.ThrowsException<NullReferenceException>(() => authProvider.AuthenticateUser(username, password));
            Assert.AreEqual(exception.Message, $"Gebruiker '{username}' bestaat niet!");
        }

        [TestMethod]
        public void UserPasswordIsIncorrectTest()
        {
            PasswordBox password = null;
            var t = new Thread(() => { password = new PasswordBox() { Password = "An incorrect password" }; });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            var authProvider = new AuthenticationProvider();
            var exception = Assert.ThrowsException<UnauthorizedAccessException>(() => authProvider.AuthenticateUser("Luke".ToLower(), password));
            Assert.AreEqual(exception.Message, "Wachtwoord is onjuist!");
        }

        [TestCleanup]
        public void Cleanup()
        {
            /* Get all the users */
            var getAllUsers = new UserRepository(new ApplicationContext()).GetAll().ToList();
            var luke = (from item in getAllUsers where item.Username.Equals("Luke".ToLower()) select item);
            var merdin = (from item in getAllUsers where item.Username.Equals("Merdin".ToLower()) select item);

            /* Using a foreach to make sure all the users in the list will be deleted */
            /* Delete user Luke */
            foreach (var item in luke)
            {
                new UserRepository(new ApplicationContext()).Delete(item);
            }

            /* Delete user Merdin */
            foreach (var item in merdin)
            {
                new UserRepository(new ApplicationContext()).Delete(item);
            }
        }
    }
}
