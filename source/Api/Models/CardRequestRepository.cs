using System;
using System.Collections.Generic;
using Db.Mysql;
using MySql.Data.MySqlClient;

namespace Card.Models
{
    public class CardRequestRepository : IDbRepository<CardRequest>
    {
        public void Add(CardRequest cardRequest)
        {
            if (cardRequest == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "INSERT INTO card_request (id,card_no,template_id,card_holder_name,expire_date)VALUES ("
                    + cardRequest.Id + ",'"
                    + cardRequest.CardNo + "',"
                    + cardRequest.TemplateId + ","
                    + cardRequest.CardHolderName + ",'"
                    + cardRequest.ExpireDate + "')";
                    connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
        }

        public CardRequest Find(int id)
        {
            throw new NotImplementedException();
        }

        public CardRequest Find(int id, string cardNo = null)
        {
            CardRequest cardRequest = null;

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM card_request where id=" + id + "and card_no = '" + cardNo + "'");
                    cardRequest = new CardRequest()
                    {
                        Id = Convert.ToInt32(dataReader["id"].ToString()),
                        CardNo = dataReader["card_no"].ToString(),
                        TemplateId = Convert.ToInt32(dataReader["template_id"].ToString()),
                        CardHolderName = dataReader["card_holder_name"].ToString(),
                        ExpireDate = Convert.ToDateTime(dataReader["expire_date"].ToString())
                    };
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return cardRequest;
        }

        public IEnumerable<CardRequest> GetAll()
        {
            CardRequest cardRequest = null;
            List<CardRequest> cardRequestList = new List<CardRequest>();

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM card_request where id=" + cardRequest.Id);
                    while (dataReader.Read())
                    {
                        cardRequest = new CardRequest()
                        {
                            Id = Convert.ToInt32(dataReader["id"].ToString()),
                            CardNo = dataReader["card_no"].ToString(),
                            TemplateId = Convert.ToInt32(dataReader["template_id"].ToString()),
                            CardHolderName = dataReader["card_holder_name"].ToString(),
                            ExpireDate = Convert.ToDateTime(dataReader["expire_date"].ToString())
                        };
                    }
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return cardRequestList;
        }

        public CardRequest Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CardRequest cardRequest)
        {
            if (cardRequest == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "update  user set "
                    + "card_no='" + cardRequest.CardNo
                    + "',template_id='" + cardRequest.TemplateId
                    + "',card_holder_name=" + cardRequest.CardHolderName
                    + ",expire_date=" + cardRequest.ExpireDate
                    + "' where id = " + cardRequest.Id + " and card_no='" + cardRequest.CardNo + "'";
                    connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
        }
    }
}