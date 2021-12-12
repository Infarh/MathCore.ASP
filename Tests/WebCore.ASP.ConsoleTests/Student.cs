using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.ASP.ConsoleTests
{
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Course Course { get; set; }

        public Student() { }

        public Student(int Id, string LastName, string FirstName, string Patronymic, Course Course)
        {
            this.Id = Id;
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.Patronymic = Patronymic;
            this.Course = Course;
        }
    }

    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        public Course() { }

        public Course(int Id, string Name, ICollection<Student> Students)
        {
            this.Id = Id;
            this.Name = Name;
            this.Students = Students;
        }
    }
}
