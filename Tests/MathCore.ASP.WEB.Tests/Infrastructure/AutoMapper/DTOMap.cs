using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using MathCore.ASP.WEB.Tests.Controllers.API;
using MathCore.ASP.WEB.Tests.Models;

namespace MathCore.ASP.WEB.Tests.Infrastructure.AutoMapper
{
    public class DTOMap : Profile
    {
        public DTOMap()
        {
            CreateMap<Student, StudentsApiConteoller.Student>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(student => student.FirstName))
                .ForMember(dto => dto.Course, opt => opt.MapFrom(student => student.Course.Name))
                .ForMember(dto => dto.Course, opt => opt.MapFrom(student => new StudentsApiConteoller.Course(student.Course.Id, student.Course.Name)));

            CreateMap<Course, StudentsApiConteoller.Course>();
        }
    }
}
