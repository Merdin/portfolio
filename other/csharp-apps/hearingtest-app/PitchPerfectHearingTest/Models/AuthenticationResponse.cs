namespace PitchPerfectHearingTest.Models
{
    public class AuthenticationResponse
    {
        public bool IsValidated { get; set; }

        public AuthenticatedUser AuthenticatedUser { get; set; }

        public AuthenticationResponse() { }
    }
}