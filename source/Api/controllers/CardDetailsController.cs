using System;
using Db.Mysql;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Card.Entity;
using Newtonsoft.Json;
using Card.Models;
namespace Card.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    [Route("api/CardRequest/id")]
    public class CardController : Controller
    {
        private static IDbRepository<CardRequest> CardRequestDbRepository { get; set; }
        [HttpPost]
        public IActionResult CardRequestWithID([FromBody] CardRequest cardRequest, IDbRepository<CardRequest> iDbRepository = null)
        {

            CardRequest cardRequestAsigned = null;
            
            try
            {
                IEnumerable<CardRequest> cardDetailsList = CardRequestDbRepository.GetAll();
                cardRequest cardDetails = cardDetailsList.Where(x=>x.Id == cardRequest.Id).FirstOrDefault();

                Ok(cardDetails);
            }
            catch (Exception e)
            {
                return BadRequest(mySqlException.Message);
            }

            return JsonConvert.SerializeObject(new RequestResult
            {
                State = RequestState.Success,
                Data = new
                {
                    Id = cardRequestAsigned.Id,
                    CardNo = cardRequestAsigned.CardNo,
                    TemplateId = cardRequestAsigned.TemplateId,
                    CardHoldeName = cardRequestAsigned.CardHoldeName,
                    ExpireDate = cardRequestAsigned.ExpireDate
                }
            });


        }

        [HttpPost]
        public bool CardRequest([FromBody]CardRequest cardRequest)
        {

            string query = "insert into card_request (id,card_no,template_id,card_holder_name,expire_date) values(" + cardRequest.Id + "," + cardRequest.CardNo + "," + cardRequest.TemplateId + ",'" + cardRequest.CardHoldeName + "','" + cardRequest.ExpireDate + "');";

            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    connect.MysqlExecuteNonQuery(query);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}