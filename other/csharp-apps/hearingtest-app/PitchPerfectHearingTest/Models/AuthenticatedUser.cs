namespace PitchPerfectHearingTest.Models
{
    public class AuthenticatedUser
    {
        public User User { get; set; }
        public string TokenKey { get; set; }

        public AuthenticatedUser(User user, string tokenKey)
        {
            User = user;
            TokenKey = tokenKey;
        }
    }
}