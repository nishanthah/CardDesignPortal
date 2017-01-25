using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Card.Models;
using System.Collections.Generic;
using System.Linq;

namespace Card.Controllers
{
    [Route("api/[action]")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    public class UserController : Controller
    {

        private IDbRepository<UserDetail> UserDetailDbRepository { get; set; }

        public UserController(IDbRepository<UserDetail> userDetailDbRepository)
        {
            this.UserDetailDbRepository = userDetailDbRepository;
        }

        [HttpPost]
        [Authorize(Policy = "PortalUser")]
        public IActionResult UserById([FromBody] UserDetail userDetail)
        {
            try
            {
                return Ok(this.UserDetailDbRepository.Find(userDetail.Id));
            }
            catch (MySqlException mySqlException)
            {
                return BadRequest(mySqlException.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "PortalUser")]
        public IActionResult User([FromBody] UserDetail userDetail)
        {
            try
            {
                int userCount = this.UserDetailDbRepository.GetAll().Where(x => x.Id == userDetail.Id).Count();
                if (userCount == 0)
                {
                    this.UserDetailDbRepository.Add(userDetail);
                }
                else
                {
                    this.UserDetailDbRepository.Update(userDetail);
                }

                return Ok(true);
            }
            catch (MySqlException mySqlException)
            {
                return BadRequest(mySqlException.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "PortalUser")]
        public IActionResult IsUserRegister([FromBody] UserDetail userDetail)
        {
            try
            {
                int userCount = this.UserDetailDbRepository.GetAll().Where(x => x.Id == userDetail.Id).Count();
                if (userCount == 1)
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (MySqlException mySqlException)
            {
                return BadRequest(mySqlException.Message);
            }
        }
    }
}