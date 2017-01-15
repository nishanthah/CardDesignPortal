using System;
using Db.Mysql;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Card.Entity;
using Newtonsoft.Json;
namespace Card.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    public class CardController : Controller
    {
        [HttpPost]
        [Route("api/CardRequest/id")]
        public string CardRequestWithID([FromBody] CardRequest cardRequest)
        {

            CardRequest cardRequestAsigned = null;
            
            try
            {
                using (DbConnect connect = new DbConnect())
                {
                    MySqlDataReader dataReader = connect.MysqlExecuteQuery("SELECT * FROM card_request where id=" + cardRequest.Id + ";");

                    while (dataReader.Read())
                    {
                        cardRequestAsigned = new CardRequest(Convert.ToInt32(dataReader["id"].ToString()), Convert.ToInt32(dataReader["card_no"].ToString()), Convert.ToInt32(dataReader["template_id"].ToString()), dataReader["card_holder_name"].ToString(), dataReader["expire_date"].ToString());
                        // requestCardDetails.Add(new CardRequest(Convert.ToInt32(dataReader["id"].ToString()), Convert.ToInt32(dataReader["card_no"].ToString()), Convert.ToInt32(dataReader["template_id"].ToString()), dataReader["card_holder_name"].ToString(), dataReader["expire_date"].ToString()));
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
                    Id = cardRequestAsigned.Id,
                    CardNo = cardRequestAsigned.CardNo,
                    TemplateId = cardRequestAsigned.TemplateId,
                    CardHoldeName = cardRequestAsigned.CardHoldeName,
                    ExpireDate = cardRequestAsigned.ExpireDate
                }
            });


        }

        [HttpPost]
        [Route("api/CardRequest")]
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

    public class CardRequest
    {
        public CardRequest(int id, int cardNo, int templateId, string cardHolderName, string expireDate)
        {
            this.Id = id;
            this.CardNo = cardNo;
            this.TemplateId = templateId;
            this.CardHoldeName = cardHolderName;
            this.ExpireDate = expireDate;
        }
        public int Id { get; set; }
        public int CardNo { get; set; }
        public int TemplateId { get; set; }
        public string CardHoldeName { get; set; }
        public string ExpireDate { get; set; }
    }

}