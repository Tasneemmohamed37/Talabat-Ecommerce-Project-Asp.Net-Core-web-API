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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShipToAddress, sh => sh.WithOwner()); // 1:1 [total] , will mapp shipping address props to columns in order table 

            builder.Property(O => O.Status)
                .HasConversion(         // HasConversion func take 2 lamda exepression , prop dt store , prop db retrieve
                    S => S.ToString(), 
                    S => (OrderStatus) Enum.Parse(typeof(OrderStatus),S));

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18, 2)");

        }
    }
}
