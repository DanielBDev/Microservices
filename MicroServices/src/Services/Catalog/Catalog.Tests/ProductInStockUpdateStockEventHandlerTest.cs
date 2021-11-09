using Catalog.Domain;
using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using Catalog.Tests.Config;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using static Catalog.Common.Enums;

namespace Catalog.Tests
{
    public class ProductInStockUpdateStockEventHandlerTest
    {
        private ILogger<ProductInStockUpdateStockEventHandler> GetLogger
        {
            get
            {
                return new Mock<ILogger<ProductInStockUpdateStockEventHandler>>()
                    .Object;
            }
        }

        /// <summary>
        /// Intenta substraer stock a un producto con stock
        /// </summary>
        [Fact]
        public void TryToSubstractStockWhenProductHasStock()
        {
            //Arrange
            var context = ApplicationDbContextInMemory.Get();

            var productInStockId = 1;
            var productId = 1;

            context.Stocks.Add(new ProductInStock
            {
                ProductInStockId = productInStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();
            //Act
            var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock = 1,
                        Action = ProductInStockAction.Substract
                    }
                }
            }, new CancellationToken()).Wait();
        }

        /// <summary>
        /// Intenta substraer stock a un producto que no tiene stock
        /// </summary>
        [Fact]
        public void TryToSubstractStockWhenProductHasntStock()
        {
            //Arrange
            var context = ApplicationDbContextInMemory.Get();

            var productInStockId = 2;
            var productId = 2;

            context.Stocks.Add(new ProductInStock
            {
                ProductInStockId = productInStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();

            //Assert
            Assert.Throws<ProductInStockUpdateStockCommandException>(() =>
            {
                try
                {
                    //Act
                    var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

                    handler.Handle(new ProductInStockUpdateStockCommand
                    {
                        Items = new List<ProductInStockUpdateItem>()
                        {
                            new ProductInStockUpdateItem
                            {
                                ProductId = productId,
                                Stock = 2,
                                Action = ProductInStockAction.Substract
                            }
                        }
                    }, new CancellationToken()).Wait();
                }
                catch (AggregateException ae)
                {
                    var exception = ae.GetBaseException();

                    if (exception is ProductInStockUpdateStockCommandException)
                    {
                        throw new ProductInStockUpdateStockCommandException(exception.InnerException?.Message);
                    }
                }
            });
        }

        /// <summary>
        /// Intenta agregar stock a un producto que existe en la base de datos
        /// </summary>
        [Fact]
        public void TryToAddStockWhenProductExist()
        {
            //Arrange
            var context = ApplicationDbContextInMemory.Get();

            var productInStockId = 3;
            var productId = 3;

            context.Stocks.Add(new ProductInStock
            {
                ProductInStockId = productInStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();
            //Act
            var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock = 2,
                        Action = ProductInStockAction.Add
                    }
                }
            }, new CancellationToken()).Wait();

            var stockInDb = context.Stocks.Single(x => x.ProductId == productId).Stock;

            //Assert
            Assert.Equal(3, stockInDb);
        }

        /// <summary>
        /// Intenta agregar stock a un producto que no existe en la base de datos
        /// </summary>
        [Fact]
        public void TryToAddStockWhenProductNotExist()
        {
            //Arrange
            var context = ApplicationDbContextInMemory.Get();

            var productId = 4;

            //Act
            var handler = new ProductInStockUpdateStockEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem
                    {
                        ProductId = productId,
                        Stock = 2,
                        Action = ProductInStockAction.Add
                    }
                }
            }, new CancellationToken()).Wait();

            var stockInDb = context.Stocks.Single(x => x.ProductId == productId).Stock;

            //Assert
            Assert.Equal(2, stockInDb);
        }
    }
}
