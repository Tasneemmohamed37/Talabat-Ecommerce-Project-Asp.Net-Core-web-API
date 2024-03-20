using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Reposatory.Data.Config.Orders
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OI => OI.ProductItemOrdered, P => P.WithOwner()); // 1:1 [total] , will mapp ProductItemOrdered props to columns in orderItem table 

            builder.Property(OI => OI.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
