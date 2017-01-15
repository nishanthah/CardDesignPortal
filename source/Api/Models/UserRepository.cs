using System;
using System.Collections.Generic;
using Db.Mysql;
using MySql.Data.MySqlClient;

namespace Card.Models
{
    public class UserRepository : IDbRepository<User>
    {
        public void Add(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "INSERT INTO user (username,password,is_active,no_of_attempt,reset_code)VALUES ('"
                    + user.UserName + "','"
                    + user.Password + "',"
                    + user.IsActive + ","
                    + user.NoOfAttempt + ",'"
                    + user.ResetCode + "')";
                    connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
        }

        public User Find(int id)
        {
            User user = null;

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM user where id=" + user.Id);
                    user = new User()
                    {
                        Id = Convert.ToInt32(dataReader["id"].ToString()),
                        UserName = dataReader["username"].ToString(),
                        Password = dataReader["password"].ToString(),
                        IsActive = dataReader["is_active"].ToString().Equals("1") ? true : false,
                        NoOfAttempt = Convert.ToInt32(dataReader["no_of_attempt"].ToString()),
                        ResetCode = dataReader["reset_code"].ToString()
                    };
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return user;
        }

        public User Find(int id, string cardNo = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            User user = null;
            List<User> userDetailList = new List<User>();
            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM user");
                    while (dataReader.Read())
                    {

                        user = new User()
                        {
                            Id = Convert.ToInt32(dataReader["id"].ToString()),
                            UserName = dataReader["username"].ToString(),
                            Password = dataReader["password"].ToString(),
                            IsActive = dataReader["is_active"].ToString().Equals("1") ? true : false,
                            NoOfAttempt = Convert.ToInt32(dataReader["no_of_attempt"].ToString()),
                            ResetCode = dataReader["reset_code"].ToString()
                        };
                        userDetailList.Add(user);
                    }
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return userDetailList;
        }

        public User Remove(int id)
        {
            User user = null;
            try
            {
                user = Find(id);
                using (DbConnect connect = new DbConnect())
                {
                    string query = "update user set"
                    + "is_active = 0 where id = " + user.Id;
                    connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return user;
        }

        public void Update(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "update  user set "
                    + "username='" + user.UserName
                    + "',password='" + user.Password
                    + "',is_active=" + user.IsActive
                    + ",no_of_attempt=" + user.NoOfAttempt
                    + ",reset_code='" + user.ResetCode
                    + "' where id = " + user.Id;
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