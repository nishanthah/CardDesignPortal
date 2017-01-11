using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Db.Mysql;

namespace Card.Models
{
    public class UserDetailRepository : IDbRepository<UserDetail>
    {
        public void Add(UserDetail userDetail)
        {
            if (userDetail == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "INSERT INTO user_detail (id,first_name,last_name,phone_number,email_address,mailling_address,town,cif,date_of_birth,account_branch,image)VALUES ("
                    + "1" + ",'"
                    + userDetail.FirstName + "','"
                    + userDetail.LastName + "','"
                    + userDetail.PhoneNumber + "','"
                    + userDetail.EmailAddress + "','"
                    + userDetail.MaillingAddress + "','"
                    + userDetail.Town + "','"
                    + userDetail.Cif + "','"
                    + userDetail.DateOfBirth + "','"
                    + userDetail.AccountBranch + "','"
                    + userDetail.Image + "')";
                    connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
        }

        public UserDetail Find(int id)
        {
            UserDetail userDetail = null;

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM user_detail where id=" + userDetail.Id);
                    userDetail = new UserDetail()
                    {
                        Id = Convert.ToInt32(dataReader["id"].ToString()),
                        FirstName = dataReader["first_name"].ToString(),
                        LastName = dataReader["last_name"].ToString(),
                        PhoneNumber = dataReader["phone_number"].ToString(),
                        EmailAddress = dataReader["email_address"].ToString(),
                        MaillingAddress = dataReader["mailling_address"].ToString(),
                        Town = dataReader["town"].ToString(),
                        Cif = dataReader["cif"].ToString(),
                        DateOfBirth = dataReader["date_of_birth"].ToString(),
                        AccountBranch = dataReader["account_branch"].ToString(),
                        Image = dataReader["image"].ToString()
                    };
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return userDetail;
        }

        public UserDetail Find(int id, string cardNo = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDetail> GetAll()
        {
            UserDetail userDetail = null;
            List<UserDetail> userDetailList = new List<UserDetail>();
            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM user_detail");
                    while (dataReader.Read())
                    {
                        userDetail = new UserDetail()
                        {
                            Id = Convert.ToInt32(dataReader["id"].ToString()),
                            FirstName = dataReader["first_name"].ToString(),
                            LastName = dataReader["last_name"].ToString(),
                            PhoneNumber = dataReader["phone_number"].ToString(),
                            EmailAddress = dataReader["email_address"].ToString(),
                            MaillingAddress = dataReader["mailling_address"].ToString(),
                            Town = dataReader["town"].ToString(),
                            Cif = dataReader["cif"].ToString(),
                            DateOfBirth = dataReader["date_of_birth"].ToString(),
                            AccountBranch = dataReader["account_branch"].ToString(),
                            Image = dataReader["image"].ToString()
                        };
                        userDetailList.Add(userDetail);
                    }
                }
            }
            catch (MySqlException mysqlException)
            {
                throw mysqlException;
            }
            return userDetailList;
        }

        public UserDetail Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserDetail userDetail)
        {
            if (userDetail == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "update user_detail set " +
                    "id=" + userDetail.Id +
                    ",first_name='" + userDetail.FirstName +
                    "',last_name='" + userDetail.LastName +
                    "',phone_number='" + userDetail.PhoneNumber +
                    "',email_address='" + userDetail.EmailAddress +
                    "',mailling_address='" + userDetail.MaillingAddress +
                    "',town='" + userDetail.Town +
                    "',cif='" + userDetail.Cif +
                    "',date_of_birth='" + userDetail.DateOfBirth +
                    "',account_branch='" + userDetail.AccountBranch +
                    "',image='" + userDetail.Image +
                    "' where id=" +userDetail.Id;
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