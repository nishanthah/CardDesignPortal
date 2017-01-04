using System;
using System.Collections.Generic;
using Db.Mysql;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
//using MailKit.Net.Smtp;
//using MimeKit;
//using MailKit.Security;
using Card.Entity;
using Newtonsoft.Json;
namespace Reset.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    public class ResetController : Controller
    {
        [HttpPost]
        [Route("api/Reset/id")]
        public string Reset([FromBody] UserEntity user)
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            int resetCode = _rdm.Next(_min, _max);
            ///sending email code is need implement
            /*
             var message = new MimeMessage();
             message.From.Add(new MailboxAddress("Angular 2", "angular2ntbdemo@gmail.com"));
             message.To.Add(new MailboxAddress("Nishna", "smithnishan4n@gmail.com"));
             message.Subject = "Hello World - A mail from ASPNET Core";
             message.Body = new TextPart("plain")
             {
                 Text = "Hello World - A mail from ASPNET Core"
             };

             using (var client = new SmtpClient())
             {
                 client.Connect("smtp.example.com", 587, false);
                 client.AuthenticationMechanisms.Remove("XOAUTH2");
                 // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.     client.Authenticate("anuraj.p@example.com", "password");
                 client.Send(message);
                 client.Disconnect(true);
             }*/
           int result=0;
           bool userStatus = false;
            if (!isUserExist(user.emailAddress))
            {
                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestState.Success,
                    Data = new
                    {
                        result = result,
                        userExist = userStatus
                    }
                });
            }
            else
                userStatus = true;

            string query = "update user u JOIN user_detail ud ON u.id = ud.id SET u.reset_code="+resetCode+" where u.id=(select id from user_detail where email='"+user.emailAddress+"');";
            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    result=connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }    

            return JsonConvert.SerializeObject(new RequestResult
            {
                State = RequestState.Success,
                Data = new
                {
                   result = true,
                   userExist = userStatus
                }
            });


        }

        [HttpPost]
        [Route("api/Password/id")]
        public string Password([FromBody] UserEntity user)
        {

            int result=0;
            bool userStatus=false;
            if(!isUserExist(user.emailAddress))
            {
                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestState.Success,
                    Data = new
                    {
                        result = result,
                        userExist = userStatus
                    }
                });
            }
            else
                userStatus=true;
            string query = "update user u JOIN user_detail ud ON u.id = ud.id SET u.password ='"+user.password+"' where u.reset_code ="+user.resetCode+" and u.id = (select id from user_detail where email ='"+user.emailAddress+"');";
            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    result=connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }    

            return JsonConvert.SerializeObject(new RequestResult
            {
                State = RequestState.Success,
                Data = new
                {
                    result=result,
                    userExist=userStatus
                }
            });


        }
        public bool isUserExist(string email)
        {
             try
            {
                int count=0;
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM user_detail where email='" +email+ "';");

                    while (dataReader.Read())
                    {
                      count++;
                    }

                }
                if (count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                   
            }
            return false;
        }
        
    }


}