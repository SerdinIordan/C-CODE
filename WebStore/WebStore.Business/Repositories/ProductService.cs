using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using WebStore.Common.Services;

namespace WebStore.Business.Repositories
{
    public class ProductService:IProductService
    {
        private ISession session;
        public ProductService(ISession session)
        {
            this.session = session;
        }
        public void Delete(Product entity)
        {
            session.Delete(entity);
            session.Flush();
        }

        public List<Product> GetAll()
        {
            return session.Query<Product>().ToList();
        }

        public Product GetById(int id)
        {
            return session.Get<Product>(id);
        }

        public Product GetByName(string name)
        {

            throw new NotImplementedException();
        }

        public void Save(Product entity)
        {
            session.Save(entity);
            session.Flush();
        }
        public void Update(Product entity)
        {
            session.Update(entity);
            session.Flush();
        }
    }
}
