using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_laba2
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Dictionary<string, int> AccessRights { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            AccessRights = new Dictionary<string, int>();
        }
    }
}
