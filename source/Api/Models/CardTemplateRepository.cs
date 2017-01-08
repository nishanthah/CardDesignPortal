using System;
using System.Collections.Generic;
using Db.Mysql;
using MySql.Data.MySqlClient;

namespace Card.Models
{
    public class CardTemplateRepository : IDbRepository<CardTemplate>
    {
        public void Add(CardTemplate cardTemplate)
        {
            if (cardTemplate == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "INSERT INTO card_template (image,is_active)VALUES ('"
                    + cardTemplate.Image + "',"
                    + cardTemplate.IsActive;
                    connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
        }

        public CardTemplate Find(int id)
        {
            CardTemplate cardTemplate = null;

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM card_template where id=" + cardTemplate.Id);
                    cardTemplate = new CardTemplate()
                    {
                        Id = Convert.ToInt32(dataReader["id"].ToString()),
                        Image = dataReader["image"].ToString(),
                        IsActive = Convert.ToBoolean(dataReader["is_active"].ToString()),
                    };
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return cardTemplate;
        }

        public CardTemplate Find(int id, string cardNo = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardTemplate> GetAll()
        {
            CardTemplate cardTemplate = null;
            List<CardTemplate> cardTemplateList = new List<CardTemplate>();
            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM card_template ");
                    while (dataReader.Read())
                    {
                        cardTemplate = new CardTemplate()
                        {
                            Id = Convert.ToInt32(dataReader["id"].ToString()),
                            Image = dataReader["image"].ToString(),
                            IsActive = Convert.ToBoolean(dataReader["is_active"].ToString()),
                        };
                        cardTemplateList.Add(cardTemplate);
                    }
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return cardTemplateList;
        }

        public CardTemplate Remove(int id)
        {
            CardTemplate cardTemplate = null;
            try
            {
                cardTemplate = Find(id);
                using (DbConnect connect = new DbConnect())
                {
                    string query = "update card_template set"
                    + "is_active = 0 where id = " + cardTemplate.Id;
                    connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return cardTemplate;
        }

        public void Update(CardTemplate cardTemplate)
        {
            if (cardTemplate == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "update  card_template set "
                    + "image='" + cardTemplate.Image
                    + "',is_active=" + cardTemplate.IsActive
                    + " where id = " + cardTemplate.Id;
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