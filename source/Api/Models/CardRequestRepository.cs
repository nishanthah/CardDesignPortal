using System;
using System.Collections.Generic;

namespace Card.Models
{
    public class CardRequestRepository : IDbRepository<CardRequest>
    {
        public void Add(CardRequest item)
        {
            throw new NotImplementedException();
        }

        public CardRequest Find(int id, string accuntNo = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public CardRequest Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CardRequest item)
        {
            throw new NotImplementedException();
        }
    }
}