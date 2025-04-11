using Data.Interfaces;
using System;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;

namespace Data.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }
        public string? Notes { get; set; }
        public string? Role { get; set; }
        public List<string> Categories { get; set; } = new();
        public List<ReviewDTO> Reviews { get; set; } = new();
    }
}
