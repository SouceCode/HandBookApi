
using System;
using System.Linq;

namespace HandBookApi.Models
{
    public static class DbInitializer
    {
        public static void Initialize(HandBookContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Base_Books.
            if (context.Base_Books.Any())
            {
                return;   // DB has been seeded
            }

            var base_Books = new Base_Book[]
            {
                new Base_Book{Name="网站1",ReMark="测试",CreateDate=DateTime.Parse("2019-09-01")},
                  new Base_Book{Name="网站2",ReMark="测试",CreateDate=DateTime.Parse("2019-09-01")},
                 new Base_Book{Name="网站3",ReMark="测试",CreateDate=DateTime.Parse("2019-09-01")},
                   new Base_Book{Name="网站4",ReMark="测试",CreateDate=DateTime.Parse("2019-09-01")}
            };
            foreach (Base_Book s in base_Books)
            {
                context.Base_Books.Add(s);
            }
            context.SaveChanges();

            // var courses = new Course[]
            // {
            //     new Course{CourseID=1050,Title="Chemistry",Credits=3},
            //     new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            //     new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            //     new Course{CourseID=1045,Title="Calculus",Credits=4},
            //     new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            //     new Course{CourseID=2021,Title="Composition",Credits=3},
            //     new Course{CourseID=2042,Title="Literature",Credits=4}
            // };
            // foreach (Course c in courses)
            // {
            //     context.Courses.Add(c);
            // }
            // context.SaveChanges();

            // var enrollments = new Enrollment[]
            // {
            //     new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            //     new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            //     new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            //     new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            //     new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            //     new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            //     new Enrollment{StudentID=3,CourseID=1050},
            //     new Enrollment{StudentID=4,CourseID=1050},
            //     new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            //     new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            //     new Enrollment{StudentID=6,CourseID=1045},
            //     new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            // };
            // foreach (Enrollment e in enrollments)
            // {
            //     context.Enrollments.Add(e);
            // }
            // context.SaveChanges();
        }
    }
}