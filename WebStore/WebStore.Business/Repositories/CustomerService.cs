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


    public class CustomerService : ICustomerService
    {
        private ISession session;
        public CustomerService(ISession session)
        {
            this.session = session;
        }
        public void Delete(Customer entity)
        {
            session.Delete(entity);
            session.Flush();
        }

        public List<Customer> GetAll()
        {
            return session.Query<Customer>().ToList();
        }

        public Customer GetById(int id)
        {
            return session.Get<Customer>(id);
        }

        public Customer GetByName(string name)
        {

            throw new NotImplementedException();
        }

        public void Save(Customer entity)
        {
            session.Save(entity);
            session.Flush();
        }
        public void Update(Customer entity)
        {
            session.Update(entity);
            session.Flush();
        }
    }
}
