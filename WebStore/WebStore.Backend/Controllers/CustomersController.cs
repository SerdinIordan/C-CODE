using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebStore.Backend.Models;
using WebStore.Business;
using WebStore.Business.Repositories;
using WebStore.Common.Entities;
using WebStore.Common.Models;

namespace WebStore.Backend.Controllers
{
    public class CustomersController : ApiController
    {
        private static string connectionString =
            @"Data Source=INTERN2017-25;Initial Catalog=WebstoreP;User ID=sa;Password=1234%asd;";

        private static ISessionFactory sessionFactory;

        static CustomersController()
        {
            var config = new NhConfig(connectionString);
            sessionFactory = config.Create();
        }

        [HttpPost]
        public PageData<Customer> GetPage(GridDataRequest gridRequest)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Customer>();
                gridRequest.TotalRecords = query.Count();

                query = ApplyFilter(query, gridRequest);
                var total = query.Count();
                //var totalPages = CountTotalPage(query, gridRequest);




                // query = ApplyPaging(query, gridRequest);



                var result = new PageData<Customer>
                {
                    Data = query.ToList(),
                    //  Total = totalPages,
                    TotalRecords = total
                };

                return result;
            }
        }


        private int CountTotalPage(IQueryable<Customer> query, GridDataRequest gridRequest)
        {
            var total = query.Count();
            //gridRequest.TotalRecords = total;
            var shouldAddOne = !(total % gridRequest.PageSize == 0);
            int totalPages = 0;

            if (shouldAddOne)
                totalPages = total / gridRequest.PageSize + 1;
            else
                totalPages = total / gridRequest.PageSize;

            return totalPages;
        }

        private IQueryable<Customer> ApplyPaging(IQueryable<Customer> query, GridDataRequest gridRequest)
        {
            return query.Skip(gridRequest.PageNumber * gridRequest.PageSize)
                                .Take(gridRequest.PageSize);
        }



        private IQueryable<Customer> ApplyFilter(IQueryable<Customer> query, GridDataRequest gridRequest)
        {

            if (gridRequest.FilterValue != null)
            {
                query = query.Where(a => a.Name.StartsWith(gridRequest.FilterValue));
            }


            if (gridRequest.FilterBetween1 == 0 && gridRequest.FilterBetween2 == 0)
            {
                query = query.Where(a => a.DiscountPercent >= 0);
            }
            else if (gridRequest.FilterBetween1 == 0 && gridRequest.FilterBetween2 != 0)
            {

                query = query.Where((a => (a.DiscountPercent <= gridRequest.FilterBetween2)));
            }
            else
               if (gridRequest.FilterBetween1 != 0 && gridRequest.FilterBetween2 != 0)
            {
                query = query.Where((a => (a.DiscountPercent >= gridRequest.FilterBetween1) && a.DiscountPercent <= gridRequest.FilterBetween2));
            }
            else if (gridRequest.FilterBetween1 != 0 && gridRequest.FilterBetween2 == 0)
            {
                query = query.Where((a => (a.DiscountPercent >= gridRequest.FilterBetween1)));
            }



            return query;
        }

        [HttpPost]
        public void save(Customer customer)
        {

            using (var session = sessionFactory.OpenSession())
            {
                var repository = new CustomerService(session);
                repository.Save(customer);

            }

        }

        public Customer getProductById(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var repository = new CustomerService(session);
                return repository.GetById(id);

            }
        }
        public Customer getCustomerById(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var repository = new CustomerService(session);
                return repository.GetById(id);

            }
        }

        [HttpGet]
        public int GetOrderByCustomerID(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Order>();

                // query = query.Where(p => p.Id == id);

                foreach (var order in query)
                {
                    if (order.Customer.Id == id)
                    {
                        return 1;
                    }


                }

                return 0;
            }
        }

        [HttpPost]
        public CustomerSummary getDetailCustomer(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<OrderDetail>();
                // var product = session.Get<Product>(id); //produsul de unde putem adauga informatii

                query = query.Where(a => a.Order.Customer.Id == id);
                if (query.Count() != 0)
                {
                    CustomerSummary customerSummary = new CustomerSummary();
                    customerSummary.Id = id;
                    customerSummary.OrdersCount = query.Count();
                    customerSummary.TotalAmmountCharged = query.Sum(a => a.TotalPrice);
                    customerSummary.LastOrder = query.Max(a => a.Order.OrderDate);
                    return customerSummary;
                }
                else
                {
                    CustomerSummary customerSummary = new CustomerSummary();
                    customerSummary.OrdersCount = query.Count();
                    customerSummary.TotalAmmountCharged = 0;
                    return customerSummary;
                }

            }
        }
        [HttpGet]
        public List<string> getNameCustomers()
        {
            using (var session = sessionFactory.OpenSession())
            {
                //Dictionary<string, int> map = new Dictionary<string, int>();
                var query = session.Query<Customer>();
                return query.Select(a => a.Name).Distinct().ToList();

            }
        }
        [HttpGet]
        public List<Customer> getCustomers()
        {
            using (var session = sessionFactory.OpenSession())
            {
                //Dictionary<string, int> map = new Dictionary<string, int>();
                var query = session.Query<Customer>();
                return query.Where(a => a.Name != null).Distinct().ToList();

            }
        }




        [HttpPost]
        public void edit(Customer customer)
        {

            using (var session = sessionFactory.OpenSession())
            {
                session.Update(customer);
                session.Flush();
            }

        }
        [HttpDelete]
        public void delete([FromUri] int id)
        {

            using (var session = sessionFactory.OpenSession())
            {
                var product = session.Get<Customer>(id);
                session.Delete(product);
                session.Flush();

            }

        }


    }
}