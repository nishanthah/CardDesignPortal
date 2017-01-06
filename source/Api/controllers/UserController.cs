using System;
using System.Collections.Generic;
using Db.Mysql;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Card.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Card.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    public class UserController : Controller
    {
        [HttpPost]
        [Authorize(Policy = "PortalUser")]
        [Route("api/UserDetails/id")]
        public string UserById([FromBody] UserEntity user)
        {
            UserEntity userAsigned = null;

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM user_detail where id=" + user.Id + ";");

                    while (dataReader.Read())
                    {
                        userAsigned = new UserEntity(
                            Convert.ToInt32(dataReader["id"].ToString()),
                            dataReader["fName"].ToString(),
                            dataReader["lName"].ToString(),
                            dataReader["code"].ToString(),
                            dataReader["phoneNum"].ToString(),
                            dataReader["emailAddress"].ToString(),
                            dataReader["address"].ToString(),
                            dataReader["town"].ToString(),
                            dataReader["cif"].ToString(),
                            dataReader["dob"].ToString(),
                            dataReader["accBranch"].ToString(),
                            dataReader["file"].ToString()
                            );
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
                    Id = userAsigned.Id,
                    // CardHoldeName = userAsigned.CardHoldeName,
                    // KidsCif = userAsigned.KidsCif,
                    // MailingAddress = userAsigned.MailingAddress,
                    // DateOfBirth = userAsigned.DateOfBirth,
                    // Phone = userAsigned.Phone,
                    // AccountBranch = userAsigned.AccountBranch,
                    // Email = userAsigned.Email,
                    // Image = userAsigned.Image

                }
            });

        }

        [HttpPost]
        [Route("api/UserDetails")]
        [Authorize(Policy = "PortalUser")]
        public IActionResult User([FromBody] UserEntity user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    string query = "INSERT INTO user_details (UserID,FirstName,LastName,PhonCode,Phone,Email,Address,Town,Cif,Dob,AccountBranch,ImagePath)VALUES (1,'"
                    + user.fName + "','"
                    + user.lName + "','"
                    + user.code + "','"
                    + user.phoneNum + "','"
                    + user.emailAddress + "','"
                    + user.address + "','"
                    + user.town + "','"
                    + user.cif + "','"
                    + user.dob + "','"
                    + user.accBranch + "','"
                    + user.file + "')";
                    connect.MysqlExecuteNonQuery(query);
                    return Ok(true);
                }
            }
            catch (Exception e)
            {
                return BadRequest(false);
            }
        }
    }
}