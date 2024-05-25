using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Modules
{
    public class User
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public int? UserType { get; set; } // 0代表学生，1代表老师
        public string? UserPassword {  get; set; }
        public User() { }

        public User(int Id, string Name, int Type,string Passwprd)
        {
            UserId = Id;
            UserName = Name;
            UserType = Type;
            UserPassword = Passwprd;

        }
    }
}
