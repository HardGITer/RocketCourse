using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketCourse.Models
{
    public class Message
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public Group Group { get; set; }
    }
}