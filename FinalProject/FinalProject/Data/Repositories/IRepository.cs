using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        int Count { get; }
        IEnumerable<T> List(QueryOptions<T> options);
        T Get(int id);
        T Get(QueryOptions<T> options);
        bool Exists(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        //int GetCount();
    }
}
