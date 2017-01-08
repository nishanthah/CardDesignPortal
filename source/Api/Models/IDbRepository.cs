using System.Collections.Generic;

namespace Card.Models
{
    public interface IDbRepository<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll();
        T Find(int id, string cardNo = null);
        T Find(int id);
        T Remove(int id);
        void Update(T item);
    }
}