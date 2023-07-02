using AutoMapper;
using Challenge.Application.Permissions.Queries.Dto;
using Challenge.Core.Domain;

namespace Challenge.Application
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Permission, EmployeePermissionTypeDto>();
        }
    }
}
