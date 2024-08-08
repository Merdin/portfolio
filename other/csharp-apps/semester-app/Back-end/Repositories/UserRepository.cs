using System.Threading.Tasks;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Backend.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
        }
        
        public async Task<User> GetById(int id) => await FindByCondition(user => user.Id.Equals(id)).FirstOrDefaultAsync();
        public async Task<bool> UserExists(User claimedUser)
        {
            return (await FindByCondition(user => user.Token.Equals(claimedUser.Token)).FirstOrDefaultAsync() != null);
        }
        
        public async Task<User> GetByToken(string token) => await FindByCondition(user => user.Token.Equals(token)).FirstOrDefaultAsync();
    }
}
