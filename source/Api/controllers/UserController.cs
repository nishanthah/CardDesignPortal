using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Card.Models;

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
                userDetail.Id = 1;
                this.UserDetailDbRepository.Update(userDetail);
                return Ok(true);
            }
            catch (MySqlException mySqlException)
            {
                return BadRequest(mySqlException.Message);
            }
        }
    }
}