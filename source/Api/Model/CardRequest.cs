using System;

namespace Card.Model
{
    public class CardRequest
    {
        public int Id { get; set; }
        public int CardNo { get; set; }
        public int TemplateId { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}