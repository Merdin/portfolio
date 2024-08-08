using PitchPerfectHearingTest.Models;

namespace PitchPerfectHearingTest.Interfaces
{
    public interface IAuthenticationProvider
    {
        AuthenticationResponse AuthenticateUser(string username, object password);
        bool ValidateToken();
    }
}