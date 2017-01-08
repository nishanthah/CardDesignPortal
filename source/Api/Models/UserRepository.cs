using System;
using System.Collections.Generic;

namespace Card.Models
{
    public class UserRepository : IDbRepository<User>
    {
        public void Add(User item)
        {
            throw new NotImplementedException();
        }

        public User Find(int id, string accuntNo = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}