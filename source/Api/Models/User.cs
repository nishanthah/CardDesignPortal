namespace Card.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int NoOfAttempt { get; set; }
        public string ResetCode { get; set; }
    }
}