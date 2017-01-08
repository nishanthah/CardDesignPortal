using System;
using System.Collections.Generic;

namespace Card.Models
{
    public class UserDetailRepository : IDbRepository<UserDetail>
    {
        public void Add(UserDetail item)
        {
            throw new NotImplementedException();
        }

        public UserDetail Find(int id, string accuntNo = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDetail Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserDetail item)
        {
            throw new NotImplementedException();
        }
    }
}