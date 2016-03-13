using System;
using System.Collections.Generic;
using MvcSolution.Data;

namespace MvcSolution.Data
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string ImageKey { get; set; }
        public int Age { get; set; }
        public string Signature { get; set; }
        public Gender Gender { get; set; }
        public string RegisterIp { get; set; }
        public string RegisterAddress { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string InternalNotes { get; set; }
        
        public virtual ICollection<UserRoleRL> UserRoleRLs { get; set; }
        public virtual ICollection<Image> Images { get; set; } 
        public virtual ICollection<UserTagRL> UserTagRLs { get; set; } 
        
        public User()
        {
            this.Id = Guid.NewGuid();
            this.Gender = Gender.Unknown;
        }

        public User(string username, string password) : this()
        {
            this.Username = username;
            this.Password = password;
            this.NickName = username.Substring(0, username.IndexOf("@"));
        }

        public void UpdateByCompleteRegistration(User user)
        {
            this.NickName = user.NickName;
            this.Signature = user.Signature;
            this.Age = user.Age;
            this.Gender = user.Gender;
            this.ImageKey = user.ImageKey;
        }
    }
}
