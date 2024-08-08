using PitchPerfectHearingTest.Models;
using System.Collections.Generic;

namespace PitchPerfectHearingTest.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T GetSingle(int id);
    }
}