using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PitchPerfectHearingTest.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetUser(string username);
        public User GetUser(int id);
        public void UpdatePassword(string username, string password);
    }
}
