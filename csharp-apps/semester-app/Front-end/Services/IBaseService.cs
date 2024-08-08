using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public interface IBaseService<T>
    {
        Task SetJwtToken();
        
        StringContent Content(object item);

        Task<T> GetById(int id);

        Task<List<T>> Index();

        Task<T> Create(T item);
        
        Task<bool> Update(int id, T item);
        
        Task<bool> Delete(int id);
    }
}