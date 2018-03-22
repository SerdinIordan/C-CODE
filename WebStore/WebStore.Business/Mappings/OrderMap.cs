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
    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            this.Table("[Order]");
            Id(a => a.Id, a =>
            {
                a.Column("OrderID");
                a.Generator(Generators.Identity);
            });

            Property(a => a.OrderDate, a =>
            {
                a.Column("OrderDate");

            });

            ManyToOne(p => p.Customer, m =>
            {
                m.Column("CustomerID");
                m.Lazy(LazyRelation.NoLazy);
            });

            Property(a => a.DiscountPercent, a =>
            {
                a.Column("DiscountPercent");
                //a.Precision(5);
                //a.Scale(2);
            });

            Bag(p => p.OrderLines, cm =>
            {
                cm.Fetch(CollectionFetchMode.Subselect);
                cm.Cascade(Cascade.All);
                cm.Inverse(true);
                cm.Table("OrderDetail");
                cm.Key(k => k.Column("OrderID"));
                cm.Lazy(CollectionLazy.NoLazy);
            },
            action => action.OneToMany());
        }
    
}
}
