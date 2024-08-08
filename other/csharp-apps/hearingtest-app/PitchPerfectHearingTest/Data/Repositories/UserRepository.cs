using Microsoft.EntityFrameworkCore;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PitchPerfectHearingTest.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(User entity)
        {
            _context.Set<User>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Set<User>().Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Set<User>().ToList();
        }

        public User GetSingle(int id)
        {
            return _context.Set<User>().Find(id);
        }

        public void Update(User entity)
        {
            _context.Set<User>().Update(entity);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            try {
                return _context.Set<User>().Find(id);
            } catch (InvalidOperationException e) {
                throw e;
            }
        }

        public User GetUser(string username)
        {
            try {
                return _context.Set<User>().First(u => u.Username == username);
            } catch (InvalidOperationException e) {
                throw e;
            }
        }

        public void UpdatePassword(string username, string password)
        {
            User user = GetUser(username);
            user.Password = new EncryptionProvider().Encrypt(password);
            _context.SaveChanges();
        }
    }
}