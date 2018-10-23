using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketCourse.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Cost { get; set; }
        public List<string> Information { get; set; }

        public ICollection<Group> Groups { get; set; }

        public Course()
        {
            Groups = new List<Group>();
        }
    }
}