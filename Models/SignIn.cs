namespace BookStoreManagementSystem.Models
{
    public class SignInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SignInResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public SignIn Data { get; set; }
    }

    public class SignIn
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string InsertionDate { get; set; }
    }
}
