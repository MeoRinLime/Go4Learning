using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStructure
{
    public class User
    {
        public int UserId { get; set; }
        public char UserName { get; set; }
        public int UserType { get; set; } // 0代表学生，1代表老师
        public char userPassword {  get; set; }

        public User(int Id, char Name, int Type,char Passwprd)
        {
            UserId = Id;
            UserName = Name;
            UserType = Type;
            userPassword = Passwprd;

        }
    }
}
