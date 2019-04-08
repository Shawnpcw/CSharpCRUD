using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace crud.Models
{
    public class Dish
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int DishId { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.Text)]
        public string Chef { get; set; }
        [Required]
        public int Tastiness { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
        public  string CreatedAt { get; set; }
        public  string UpdatedAt { get; set; }

    }
    public class User
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(2)]
        public string Fname { get; set; }
        [Required]
        [MinLength(2)]
        public string Lname { get; set; }
        [EmailAddress]
        [Required]
        [MinLength(2)]
        public string Email { get; set; }
        [Required]
        [MinLength(2, ErrorMessage="Password must be 8 characters or longer!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        public DateTime Created_at {get;set;} = DateTime.Now;
        public DateTime Updated_at {get;set;} = DateTime.Now;
    }
    public class LoginUser
    {
        [Required]
        public string Email {get; set;}
        [Required]
        public string Password { get; set; }
    }
}
