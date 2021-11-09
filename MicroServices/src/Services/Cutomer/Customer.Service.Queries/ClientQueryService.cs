using Customer.Persistence.Database;
using Customer.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Service.Queries
{
    public interface IClientQueryService
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ClientDto> GetByIdAsync(int id);
    }

    public class ClientQueryService : IClientQueryService
    {
        private readonly CustomerDbContext _customerDbContext;

        public ClientQueryService(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var collection = await _customerDbContext.Clients
                .Where(x => clients == null || clients.Contains(x.ClientId))
                .OrderBy(x => x.Name)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ClientDto>>();
        }

        public async Task<ClientDto> GetByIdAsync(int id)
        {
            return (await _customerDbContext.Clients.SingleAsync(x => x.ClientId == id)).MapTo<ClientDto>();
        }
    }
}
