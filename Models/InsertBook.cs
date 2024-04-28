using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookStoreManagementSystem.Models
{
    public class InsertBookRequest
    {
        [Required]
        public string BookName { get; set; }
        [Required]
        public string BookType { get; set; }
        [Required]
        public string BookPrice { get; set; }
        public string BookDetails { get; set; }
        public string BookAuthor { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value Greater than 0")]
        public int Quantity { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }

    public class InsertBookResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
