using Identity.Persistence.Database;
using Identity.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Queries
{
    public interface IUserQueryService
    {
        Task<DataCollection<UserDto>> GetAllAsync(int page, int take, IEnumerable<string> users = null);
        Task<UserDto> GetByIdAsync(string id);
    }

    public class UserQueryService : IUserQueryService
    {
        private readonly IdentityAppDbContext _identityAppDbContext;

        public UserQueryService(IdentityAppDbContext identityAppDbContext)
        {
            _identityAppDbContext = identityAppDbContext;
        }

        public async Task<DataCollection<UserDto>> GetAllAsync(int page, int take, IEnumerable<string> users = null)
        {
            var collection = await _identityAppDbContext.Users
                .Where(x => users == null || users.Contains(x.Id))
                .OrderBy(x => x.FistName)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<UserDto>>();
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            return (await _identityAppDbContext.Users.SingleAsync(x => x.Id == id)).MapTo<UserDto>();
        }
    }
}
