﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISDK
{
    public interface IService<T>
    {
        Task<List<T>> GetAsync(string path);
        Task<T> GetAsync(int id, string path);
        Task<bool> AddAsync(T item, string path);
        Task<bool> UpdateAsync(int id, T item, string path);
        Task<bool> DeleteAsync(int id, string path);
    }
}
