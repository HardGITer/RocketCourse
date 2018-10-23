using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketCourse.Models
{
    public class OrderedCourse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OrderedCourseName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}