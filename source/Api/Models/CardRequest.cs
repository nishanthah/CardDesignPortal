using System;

namespace Card.Models
{
    public class CardRequest
    {
        public int Id { get; set; }
        public string CardNo { get; set; }
        public int TemplateId { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}