using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;

namespace WebStore.Business.Mappings
{
    public class OrderDetailMap : ClassMapping<OrderDetail>
    {
        public OrderDetailMap()
        {
            Id(a => a.Id, a =>
            {
                a.Column("OrderDetailID");
                a.Generator(Generators.Identity);
            });

            ManyToOne(p => p.Product, m =>
            {
                m.Column("ProductID");
                m.Lazy(LazyRelation.NoLazy);
            });

            Property(a => a.Price, a =>
            {
                a.Column("Price");
                a.Precision(8);
                a.Scale(2);
            });


            ManyToOne(p => p.Order, m =>
            {
                m.Column("OrderID");
                m.Lazy(LazyRelation.NoLazy);
            });
            Property(a => a.Quantity, a =>
            {
                a.Column("Quantity");
                a.Precision(8);
                a.Scale(2);
            });

            Property(a => a.TotalPriceWithoutDiscount, a =>
            {
                a.Column("TotalPriceWithoutDiscount");
                a.Precision(8);
                a.Scale(2);
            });

            Property(a => a.TotalPrice, a =>
            {
                a.Column("TotalPrice");
                a.Precision(8);
                a.Scale(2);
            });
        }



    
    }
}
