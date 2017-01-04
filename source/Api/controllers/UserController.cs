using System;
using System.Collections.Generic;
using Db.Mysql;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Card.Entity;

namespace Card.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("api/UserDetails/id")]
        public string User([FromBody] User  user)
        {
           
        
            User userAsigned=null;
            
            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM user_detail where id=" + user.Id + ";");

                    while (dataReader.Read())
                    {
                       userAsigned=new User(Convert.ToInt32(dataReader["id"].ToString()),dataReader["name"].ToString(),dataReader["kids_cif"].ToString(),dataReader["mailling_address"].ToString(),dataReader["date_of_birth"].ToString(),dataReader["phone"].ToString(),dataReader["account_branch"].ToString(),dataReader["email"].ToString(),dataReader["Image"].ToString());
                    }
                        
                }
            }
            catch (Exception e)
            {

            }
          
            return JsonConvert.SerializeObject(new RequestResult 
                { 
                    State = RequestState.Success, 
                    Data = new 
                    { 
                        Id=userAsigned.Id,
                        CardHoldeName=userAsigned.CardHoldeName,
                        KidsCif=userAsigned.KidsCif,
                        MailingAddress =userAsigned.MailingAddress,
                        DateOfBirth=userAsigned.DateOfBirth,
                        Phone=userAsigned.Phone,
                        AccountBranch=userAsigned.AccountBranch,
                        Email=userAsigned.Email,
                        Image =userAsigned.Image
                       
                    } 
                }); 
            

        }
        
        

    }

    public class User
    {
        public User(int Id,string CardHoldeName,string KidsCif,string MailingAddress,string DateOfBirth,string Phone,string AccountBranch,string Email,string Image)
        {
            this.Id = Id;
            this.CardHoldeName = CardHoldeName;
            this.KidsCif = KidsCif;
            this.MailingAddress = MailingAddress;
            this.DateOfBirth = DateOfBirth;
            this.Phone = Phone;
            this.AccountBranch = AccountBranch;
            this.Email = Email;
            this.Image = Image;
        }
        public int Id { get; set; }
        public string CardHoldeName { get; set; }
        public string KidsCif { get; set; }
        public string MailingAddress { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string AccountBranch { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }




}