using Customer.Domain;
using Customer.Persistence.Database;
using Customer.Service.EventHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.EventHandlers
{
    public class ClientEventHandler : INotificationHandler<ClientCreateCommand>
    {
        private readonly CustomerDbContext _customerDbContext;

        public ClientEventHandler(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task Handle(ClientCreateCommand notification, CancellationToken cancellationToken)
        {
            await _customerDbContext.AddAsync(new Client 
            {
                Name = notification.Name
            });

            await _customerDbContext.SaveChangesAsync();
        }
    }
}
