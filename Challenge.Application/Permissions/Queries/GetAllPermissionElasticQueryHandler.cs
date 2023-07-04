using AutoMapper;
using Challenge.Application.Permissions.Services;
using Challenge.Application.Permissions.Services.Dto;
using Challenge.Core.Domain;
using MediatR;

namespace Challenge.Application.Permissions.Queries
{
    public class GetAllPermissionElasticQueryHandler : IRequestHandler<GetAllPermissionElasticQuery, IEnumerable<EmployeePermissionTypeDto>>
    {        
        private readonly IMapper _mapper;
        private readonly IEslasticsearchService _eslasticsearchService;

        public GetAllPermissionElasticQueryHandler(IMapper mapper, IEslasticsearchService eslasticsearchService)
        {            
            _mapper = mapper;
            _eslasticsearchService = eslasticsearchService;
        }

        public async Task<IEnumerable<EmployeePermissionTypeDto>> Handle(GetAllPermissionElasticQuery request, CancellationToken cancellationToken)
        {
            // -- Check if exist search
            var existSearch = !string.IsNullOrEmpty(request.Search);

            // -- Return elements
            var list = await _eslasticsearchService.Search<Permission>(request.Search.Trim());


            if (list == null)
            {
                return new List<EmployeePermissionTypeDto>();
            }

            // -- Mapper to object to result
            var result = list.Select(x => _mapper.Map<EmployeePermissionTypeDto>(x)).AsEnumerable();
            return result;
        }
    }
}
