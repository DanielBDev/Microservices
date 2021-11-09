using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.Persistence.Database.Configuration
{
    public class OrderDetailConfiguration
    {
        public OrderDetailConfiguration(EntityTypeBuilder<OrderDetail> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.OrderDetailId);
        }
    }
}
