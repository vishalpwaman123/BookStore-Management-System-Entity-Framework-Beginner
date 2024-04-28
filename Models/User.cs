using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManagementSystem.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username{ get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime InsertionDate { get; set; }
        public bool IsActive { get; set; }
    }
}
