namespace Card.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public bool IsActive { get; set; }
        public int NoOfAttempt { get; set; }
        public string ResetCode { get; set; }
    }
}