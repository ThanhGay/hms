namespace HMS.Auth.Dtos
{
    public class ResultLogin
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
        public string Role { get; set; }
    }
}
