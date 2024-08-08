using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PitchPerfectHearingTest.Models
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public static string DefaultPassword { get; private set; }

        public AuthenticationResponse AuthenticateUser(string username, object password)
        {
            try 
            {
                if (string.IsNullOrEmpty(username) || password == null || (password as PasswordBox) == null)
                {
                    throw new NullReferenceException("Velden mogen niet leeg zijn!");
                }
                return ValidateUser(username, password);
            } 
            catch 
            {
                throw;
            }
        }
        public bool ResetPassword(string username, string email)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                throw new NullReferenceException("Velden mogen niet leeg zijn!");
            }

            try
            {
                var user = new UserRepository(new ApplicationContext()).GetUser(username);

                if (user.EmailAdres.Equals(email))
                {
                    GenerateRandomPassword();
                    new UserRepository(new ApplicationContext()).UpdatePassword(user.Username, DefaultPassword);
                    return true;
                }
                else
                {
                    throw new NullReferenceException($"Combinatie is onjuist!");
                }
            }
            catch (InvalidOperationException)
            {
                throw new NullReferenceException($"Gebruiker '{username}' bestaat niet!");
            }
        }

        public bool ValidateToken()
        {
            var sessionToken = Application.Current.Properties["SessionToken"] as Token;
            var authenticatedUser = Application.Current.Properties["AuthenticatedUser"] as AuthenticatedUser;

            if ((sessionToken == null || authenticatedUser == null) ||
                (!authenticatedUser.TokenKey.Equals(sessionToken.TokenKey)) ||
                (sessionToken.TokenExpiration < DateTime.Now))
            {
                Application.Current.Properties.Remove("SessionToken");
                Application.Current.Properties.Remove("AuthenticatedUser");
                return false;
            }

            sessionToken.TokenExpiration = DateTime.Now.AddMinutes(10);

            Application.Current.Properties["SessionToken"] = sessionToken;
            return true;
        }

        private AuthenticationResponse ValidateUser(string username, object password)
        {
            try 
            {
                var response = new AuthenticationResponse();

                if ((password as PasswordBox).Password.Equals(Decrypt(new UserRepository(new ApplicationContext()).GetUser(username)?.Password)))
                {
                    var token = new Token() { TokenExpiration = DateTime.Now.AddMinutes(10), TokenKey = Guid.NewGuid().ToString() };
                    var authenticatedUser = new AuthenticatedUser(new UserRepository(new ApplicationContext()).GetUser(username), token.TokenKey);
                    Application.Current.Properties.Add("SessionToken", token);

                    response.IsValidated = true;
                    response.AuthenticatedUser = authenticatedUser;
                    return response;
                } 
                else 
                {
                    throw new UnauthorizedAccessException("Wachtwoord is onjuist!");
                }
            } 
            catch (InvalidOperationException) 
            {
                throw new NullReferenceException($"Gebruiker '{username}' bestaat niet!");
            }
        }

        private string Decrypt(string password)
        {
            var encryptionProvider = new EncryptionProvider();
            var decryptedPassword = encryptionProvider.Decrypt(password);

            return decryptedPassword;
        }

        private void GenerateRandomPassword()
        {
            DefaultPassword = new RandomGenerator().RandomPassword();
        }
    }
}
