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
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Id(a => a.Id, a =>
            {
                a.Column("ProductId");
                a.Generator(Generators.Identity);
            });
            Property(a => a.Name, a =>
            {
                a.Column("Name");
            });
            Property(a => a.ListPrice, a =>
            {
                a.Column("Price");
            });
            Property(a => a.ProductImage, a =>
            {
                // a.Column("Image");
                a.Column("ProductImage");
              a.Type(NHibernateUtil.BinaryBlob);
              a.Length(Int32.MaxValue);
            });
            

        }
    }
}
