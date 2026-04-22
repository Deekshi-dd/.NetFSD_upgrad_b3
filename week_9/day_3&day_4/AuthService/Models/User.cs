namespace AuthService.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // plain for now (we improve later)
        public string Role { get; set; } // Admin / User
    }
}
