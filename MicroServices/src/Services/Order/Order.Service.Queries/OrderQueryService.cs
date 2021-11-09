using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Service.Queries
{
    public interface IOrderQueryService
    {
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take);
        Task<OrderDto> GetByIdAsync(int id);
    }

    public class OrderQueryService : IOrderQueryService
    {
        private readonly OrderDbContext _orderDbContex;

        public OrderQueryService(OrderDbContext orderDbContex)
        {
            _orderDbContex = orderDbContex;
        }

        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
        {
            var collection = await _orderDbContex.Orders
                .Include(x => x.Items)
                .OrderByDescending(x => x.OrderId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<OrderDto>>();
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            return (await _orderDbContex.Orders.Include(x => x.Items).SingleAsync(x => x.OrderId == id)).MapTo<OrderDto>();
        }
    }
}
