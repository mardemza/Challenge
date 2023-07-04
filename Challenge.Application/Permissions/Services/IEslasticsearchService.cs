using Challenge.Core;
using Challenge.Core.Domain;

namespace Challenge.Application.Permissions.Services
{
    public interface IEslasticsearchService
    {
        Task Add<T>(T obj) where T: class;        
        Task Update<T>(T obj) where T: EntityBase;
        
        Task<IEnumerable<T>> Search<T>(string query, int page = 1, int pageSize = 5) where T: class;

        Task Reset();
    }
}