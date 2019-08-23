using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCapstoneWeb
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        public string ReturnURL { get; set; }
    }
}