using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MathCore.ASP.WEB.Tests.Models;

namespace MathCore.ASP.WEB.Tests.Data
{
    public class TestData
    {
        private record RandomItem<T>(Random Random, T[] Items)
        {
            public static implicit operator T(RandomItem<T> rnd) => rnd.Items[rnd.Random.Next(rnd.Items.Length)];
        }
        
        public static void Test()
        {
            Course[] courses =
            {
                new Course { Id = 1, Name = "Математика" },
                new Course { Id = 2, Name = "Физика" },
                new Course { Id = 3, Name = "Химия" },
            };

            var rnd_course = new RandomItem<Course>(new Random(), courses);

            var students = new Student[1000];
            for(var i = 1; i <= students.Length; i++)
            {
                students[i] = new Student(i, $"Фамилия{i}", $"Имя{i}",$"Отчество{i}", rnd_course);
                students[i].Course.Students.Add(students[i]);
            }
        }
        
        static TestData()
        {
            var course = new RandomItem<Course>(new Random(), Enumerable.Range(1, 10).Select(i => new Course(i, $"Course {i}", new List<Student>())).ToArray());
            Students = Enumerable.Range(1, 1000).Select(i =>
            {
                var student = new Student(i, $"LastName {i}", $"Name {i}", $"Patronymic {i}", course);
                student.Course.Students.Add(student);
                return student;
            }).ToArray();
            Cources = course.Items;
        }
        
        public static IEnumerable<Student> Students { get; }
        
        public static IEnumerable<Course> Cources { get; }
    }
}
