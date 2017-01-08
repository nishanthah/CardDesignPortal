using System;
using System.Collections.Generic;

namespace Card.Models
{
    public class CardTemplateRepository : IDbRepository<CardTemplate>
    {
        public void Add(CardTemplate item)
        {
            throw new NotImplementedException();
        }

        public CardTemplate Find(int id, string accuntNo = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardTemplate> GetAll()
        {
            throw new NotImplementedException();
        }

        public CardTemplate Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CardTemplate item)
        {
            throw new NotImplementedException();
        }
    }
}