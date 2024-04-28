using System.ComponentModel.DataAnnotations;

namespace BookStoreManagementSystem.Models
{
    public class DeleteBookRequest
    {
        [Required]
        public int BookID { get; set; }
        [Required]
        public string PublicID { get; set; }
    }

    public class DeleteBookResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
