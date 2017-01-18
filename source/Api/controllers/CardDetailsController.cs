using System;
using Db.Mysql;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Card.Entity;
using Newtonsoft.Json;
using Card.Models;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Card.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    [Route("api/[action]")]
    public class CardController : Controller
    {
        private static IDbRepository<CardRequest> CardRequestDbRepository { get; set; }

        public CardController(IDbRepository<CardRequest> iDbRepository = null)
        {
            CardRequestDbRepository = iDbRepository;
        }

        [HttpPost]
        public IActionResult CardRequestWithID([FromBody] CardRequest cardRequest)
        {
            try
            {
                IEnumerable<CardRequest> cardDetailsList = CardRequestDbRepository.GetAll();
                cardRequest = cardDetailsList.Where(x=>x.Id == cardRequest.Id).FirstOrDefault();

                return Ok(cardRequest);
            }
            catch (MySqlException mySqlException)
            {
                return BadRequest(mySqlException.Message);
            }
        }

        [HttpPost]
        public IActionResult CardRequest([FromBody]CardRequest cardRequest)
        {
            try
            {
                CardRequestDbRepository.Add(cardRequest);
                return Ok(true);
            }
            catch (MySqlException mySqlException)
            {
                return BadRequest(mySqlException.Message);
            }
        }
    }
}