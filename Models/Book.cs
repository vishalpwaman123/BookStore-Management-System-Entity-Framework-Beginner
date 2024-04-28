using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManagementSystem.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BookID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime InsertionDate { get; set; }
        public DateTime UpdateDate { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string BookType { get; set; }
        [Required]
        public string BookPrice { get; set; }
        public string BookDetails { get; set; }
        public string BookAuthor { get; set; }
        public string BookImageUrl { get; set; }
        public string PublicId { get; set; }
        public int Quantity { get; set; }
        public bool IsArchive { get; set; }
        public bool IsActive { get; set; }
    }
}
