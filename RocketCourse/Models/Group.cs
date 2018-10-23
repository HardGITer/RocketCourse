using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RocketCourse.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public int PersonCount { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public Course Course { get; set; }

        //public ICollection<Message> Messages { get; set; }

        //public Teacher Teacher { get; set; }
        //public int CourseId { get; set; }

        public Group()
        {
            Users = new List<User>();
            //Messages = new List<Message>();
        }
    }
}