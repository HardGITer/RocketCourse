using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketCourse.Models
{
    public class LoginModel
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int Rolei { get; set; }
    }
}