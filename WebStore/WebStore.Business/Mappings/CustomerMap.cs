using NHibernate;
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
    public class CustomerMap : ClassMapping<Customer>
    {
        public CustomerMap()
        {
            Id(a => a.Id, a =>
            {
                a.Column("CustomerId");
                a.Generator(Generators.Identity);
            });
            Property(a => a.Name, a =>
            {
                a.Column("Name");
            });
            Property(a => a.DiscountPercent, a =>
            {
                a.Column("DiscountPercent");
            });
            Property(a => a.CustomerImage, a =>
            {
                // a.Column("Image");
                a.Column("CustomerImage");
                a.Type(NHibernateUtil.BinaryBlob);
                a.Length(Int32.MaxValue);
            });


        }
    }
}
