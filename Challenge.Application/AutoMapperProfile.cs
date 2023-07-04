using AutoMapper;
using Challenge.Application.Permissions.Services.Dto;
using Challenge.Core.Domain;

namespace Challenge.Application
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Permission, EmployeePermissionTypeDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>();
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<PermissionType, PermissionTypeDto>();
            CreateMap<PermissionType, PermissionTypeDto>().ReverseMap();
        }
    }
}
