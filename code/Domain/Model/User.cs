using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Domain.Model
{
    [Table("User")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [NotMapped]
        public UserType UserType { get; protected set; } = UserType.Owner;

        public User() { }

        public User(string username, string password, string firstName, string lastName, string email)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public bool EqualsUsername(string username)
        {
            return Username == username;
        }

        public bool EqualsPassword(string password)
        {
            return Password == password;
        }

        public bool EqualsFirstName(string firstName)
        {
            return FirstName == firstName;
        }

        public bool EqualsLastName(string lastName)
        {
            return LastName == lastName;
        }

        public bool EqualsEmail(string email)
        {
            return Email == email;
        }
    }
}
