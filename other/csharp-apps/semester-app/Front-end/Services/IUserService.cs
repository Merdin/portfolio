using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace FrontEnd.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<bool> IsCurrentUserRegistered();

        Task<User> GetCurrentUser();
    }
}