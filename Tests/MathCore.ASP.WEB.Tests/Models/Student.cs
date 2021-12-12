using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathCore.ASP.WEB.Tests.Models
{
    public class Student
    {
        public int Id { get; init; }
        public string LastName { get; init; }
        public string FirstName { get; init; }
        public string Patronymic { get; init; }
        public Course Course { get; init; }

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
        public int Id { get; init; }

        public string Name { get; init; }

        public ICollection<Student> Students { get; init; } = new List<Student>();

        public Course() { }

        public Course(int Id, string Name, ICollection<Student> Students)
        {
            this.Id = Id;
            this.Name = Name;
            this.Students = Students;
        }
    }
}
