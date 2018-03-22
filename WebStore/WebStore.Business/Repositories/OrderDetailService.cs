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
    public class OrderDetailService : IEntityService<OrderDetail>
    {
        private ISession session;
        public OrderDetailService(ISession session)
        {
            this.session = session;
        }
        public void Delete(OrderDetail entity)
        {
            session.Delete(entity);
            session.Flush();
        }

        public List<OrderDetail> GetAll()
        {
            return session.Query<OrderDetail>().ToList();
        }

        public OrderDetail GetById(int id)
        {
            return session.Get<OrderDetail>(id);
        }

        public Product GetByName(string name)
        {

            throw new NotImplementedException();
        }

        public void Save(OrderDetail entity)
        {
            session.Save(entity);
            session.Flush();
        }
        public void Update(OrderDetail entity)
        {
            session.Update(entity);
            session.Flush();
        }
    }
}
