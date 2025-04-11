using Data;
using Data.DatabaseConnections;
using Data.DTOs;
using Domain.Interfaces;

namespace Domain
{
    public class User
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Username { get; }
        public string Email { get; set; }
        public string HashedPassword { get; }
        public string Notes { get; set; }
        public string Role { get; }
        public List<string> Categories { get; }
        public int NumberOfCategories { get { return Categories.Count; } }
        public List<Review> Reviews { get; }
        public int NumberOfReviews { get { return Reviews.Count; } }


        // For new Users
        public User(string name, string username, string email, string hashedPassword, string notes, string role, List<string>? categories = null)
        {
            Id = 0;
            Name = name;
            Username = username;
            Email = email;
            HashedPassword = hashedPassword;
            Notes = notes;
            Role = role;
            if (categories == null) Categories = new();
            else Categories = categories;
            Reviews = new();
        }
        // Loading from DB
        public User(int id, string name, string username, string email, string hashedPassword, string notes, string role, List<string> categories, List<Review> reviews)
        {
            Id = id;
            Name = name;
            Username = username;
            Email = email;
            HashedPassword = hashedPassword;
            Notes = notes;
            Role = role;
            Categories = categories;
            Reviews = reviews;
        }

        public bool VerifyPassword(string text)
        {
            return PasswordHashing.VerifyPassword(text, HashedPassword);
        }

        public override string ToString() { return Name; }
    }
}
