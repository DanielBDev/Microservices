using MediatR;
using Order.Persistence.Database;
using Order.Service.EventHandlers.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Order.Domain;
using static Order.Common.Enums;
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Commands;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly ICatalogProxy _catalogProxy;
        private readonly ILogger<OrderCreateEventHandler> _logger;

        public OrderCreateEventHandler(
            OrderDbContext orderDbContext,
            ICatalogProxy catalogProxy,
            ILogger<OrderCreateEventHandler> logger
            )
        {
            _orderDbContext = orderDbContext;
            _catalogProxy = catalogProxy;
            _logger = logger;
        }

        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- New order creation started");
            var entry = new Domain.Order();

            using (var transaction = await _orderDbContext.Database.BeginTransactionAsync())
            {
                //01.Prepar el detalle
                _logger.LogInformation("--- Preparing detail");
                PrepareDetail(entry, notification);

                //02.Preparar el header
                _logger.LogInformation("--- Preparing header");
                PrepareHeader(entry, notification);

                //03.Crear la orden
                _logger.LogInformation("--- Creating order");
                await _orderDbContext.AddAsync(entry);
                await _orderDbContext.SaveChangesAsync();

                _logger.LogInformation($"--- Order {entry.OrderId} was created");

                //04.Update Stocks
                _logger.LogInformation("--- Updating Stock");
                try
                {
                    await _catalogProxy.UpdateStockAsync(new ProductInStockUpdateStockCommand
                    {
                        Items = notification.Items.Select(x => new ProductInStockUpdateItem
                        {
                            Action = ProductInStockAction.Substract,
                            ProductId = x.ProductId,
                            Stock = x.Quantity
                        })
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"The order could not be created: {ex.Message}");
                }
                

                await transaction.CommitAsync();
            }
        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Items = notification.Items.Select(x => new OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.Price,
                Total = x.Price * x.Quantity

            }).ToList();
        }

        private void PrepareHeader(Domain.Order entry, OrderCreateCommand notification)
        {
            //Header
            entry.Status = OrderStatus.Pending;
            entry.PaymentType = notification.PaymentType;
            entry.ClientId = notification.ClientId;
            entry.CreatedAt = DateTime.UtcNow;

            //Sum
            entry.Total = entry.Items.Sum(x => x.Total);
        }
    }
}
