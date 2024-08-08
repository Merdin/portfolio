using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Backend.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetById(int id);

        Task<bool> UserExists(User user);

        Task<User> GetByToken(string token);
    }
}
