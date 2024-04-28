using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreManagementSystem.Models
{
    public class GetBookResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPage { get; set; }
        public List<Book> Data { get; set; }
    }
}
