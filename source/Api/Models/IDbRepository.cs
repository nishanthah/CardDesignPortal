using System.Collections.Generic;

namespace Card.Models
{
    public interface IDbRepository<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll();
        T Find(int id, string accuntNo = null);
        T Remove(int id);
        void Update(T item);
    }
}