using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BLL.DataTransferObjects.EmployeeDto;
using Demo.DAL.Models.EmployeeModels;

namespace Demo.BLL.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.EmployeeType, src => src.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Gender, src => src.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Department, src => src.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeType, src => src.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Gender, src => src.MapFrom(src => src.Gender))
                .ForMember(dest => dest.HiringDate, src => src.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Department, src => src.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.Image, src => src.MapFrom(src => src.ImageName ));


            CreateMap<CreateEmployeeDto, Employee>()
                 .ForMember(dest => dest.HiringDate, src => src.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.Phone, src => src.MapFrom(src => src.PhoneNumber));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, src => src.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.Phone, src => src.MapFrom(src => src.PhoneNumber));
        }
    }
}
