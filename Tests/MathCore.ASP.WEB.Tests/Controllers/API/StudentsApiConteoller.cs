using AutoMapper;

using MathCore.ASP.Filters.Results;
using MathCore.ASP.WEB.Tests.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace MathCore.ASP.WEB.Tests.Controllers.API
{
    [Route("api/students")]
    [ApiController]
    [ProcessingTimeHeader]
    public class StudentsApiConteoller : ControllerBase
    {
        private readonly IMapper _Mapper;

        public StudentsApiConteoller(IMapper Mapper) => _Mapper = Mapper;

        public record Student(int id, string Name, string LastName, string Patronymic, Course Course)
        {
            public Student() : this(0, "", "", "", null) { }
        }

        [HttpGet, EnableQuery]
        public IActionResult GetStudents() => Ok(_Mapper.Map<IEnumerable<Student>>(TestData.Students));

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id) => 
            TestData.Students.FirstOrDefault(s => s.Id == id) is { } student 
                ? Ok(student) 
                : NotFound();

        public record Course(int id, string Name)
        {
            public Course() : this(0, "") { }
        }
        
        [HttpGet("courses"), EnableQuery]
        public IActionResult GetCourses() => Ok(_Mapper.Map<IEnumerable<Course>>(TestData.Cources));

        [HttpGet("courses/{id}")]
        public IActionResult GetCourse(int id) =>
            TestData.Cources.FirstOrDefault(c => c.Id == id) is { } course
                ? Ok(course)
                : NotFound();

    }
}
