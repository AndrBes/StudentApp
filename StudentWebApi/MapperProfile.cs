using AutoMapper;
using StudentData;
using StudentWebApi.Controllers.Models;

namespace StudentWebApi
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentAddDto, Student>();
            CreateMap<Student, StudentGetDto>()
                // Если надо поменять параметр, находимый маппером
                .ForMember(dest => dest.StudentId,
                opt => opt.MapFrom(src => src.Id))
                // Объединение ФИО - FullName
                .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Midname}"))
                // Не отправлять Email
                .ForMember(dest => dest.Email,
                opt => opt.Ignore());
        }
    }
}
