using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RocketCourse.Models
{
    public class CourseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<OrderedCourse> OrderedCourses { get; set; }
    }

    public class CourseDbInitializer : DropCreateDatabaseAlways<CourseContext>
    {
        protected override void Seed(CourseContext context)
        {
            context.Groups.Add(new Group { GroupName = "Test english G1", PersonCount = 10, });

            context.Roles.Add(new Role { Name = "admin" });
            context.Roles.Add(new Role { Name = "teacher" });
            context.Roles.Add(new Role { Name = "user" });

            context.Users.Add(new User { Name = "Матвей", Mail = "admin", Password = "admin", Age = 18, Phone = "+375xxxxxxxxx", Rolei = 1 });

            context.Courses.Add(new Course { Name = "Test english", Duration = 30, Cost = 400, Information = new List<string> { "test inf 1", "test inf 2", "test inf 3" } });


            //context.Courses.Add(new Course { Name = "Test English", Duration=10, Cost= 300,  });
            //context.Roles.Add(new Role { Name = "admin" });
            //context.Roles.Add(new Role { Name = "user" });
            //context.Users.Add(new User { Name = "test", Mail = "test", Age = 18, Phone = "+3752555555555", Password = "test", Rolei = 1 });

            base.Seed(context);
        }
    }
}