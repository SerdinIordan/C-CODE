using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using WebStore.Common.Services;
using WebStore.Common.Models;

namespace WebStore.Business.Repositories
{
    public class OrderService : IOrderService
    {
        private ISession session;
        public OrderService(ISession session)
        {
            this.session = session;
        }
        public void Delete(Order entity)
        {
            session.Delete(entity);
            session.Flush();
        }

        public List<Order> GetAll()
        {
            return session.Query<Order>().ToList();
        }

        public Order GetById(int id)
        {
            return session.Get<Order>(id);
        }

        public Product GetByName(string name)
        {

            throw new NotImplementedException();
        }

        public OrderSummary[] ReportByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void Save(Order entity)
        {
            session.Save(entity);
            session.Flush();
        }
        public void Update(Order entity)
        {
            session.Update(entity);
            session.Flush();
        }
    }
}
