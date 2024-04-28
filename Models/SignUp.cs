namespace BookStoreManagementSystem.Models
{
    public class SignUpRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfigPassword { get; set; }
    }

    public class SignUpResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
