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
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));

            CreateMap<CreateEmployeeDto, Employee>()
                 .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
