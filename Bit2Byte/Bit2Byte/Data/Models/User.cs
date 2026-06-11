using System;

namespace Bit2Byte.Data.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PendingEmail { get; set; }
        public string PasswordHash { get; set; }
        public string AvatarPath { get; set; }
        public string Bio { get; set; }
        public string Interests { get; set; }
        public string EmailChangeToken { get; set; }
        public DateTime? EmailChangeTokenExpires { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
