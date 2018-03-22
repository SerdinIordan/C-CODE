using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Http;
using WebStore.Backend.Models;
using WebStore.Business;
using WebStore.Business.Repositories;
using WebStore.Common.Entities;

namespace WebStore.Backend.Controllers
{
    public class OrdersController : ApiController
    {
        private static string connectionString =
            @"Data Source=INTERN2017-25;Initial Catalog=WebstoreP;User ID=sa;Password=1234%asd;";

        private static ISessionFactory sessionFactory;

        static OrdersController()
        {
            var config = new NhConfig(connectionString);
            sessionFactory = config.Create();
        }

        [HttpPost]
        public PageData<Order> GetPage(GridDataRequest gridRequest)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Order>();

                query = query.OrderByDescending(c => c.OrderDate);
                query = ApplyFilter(query, gridRequest);
                var total = query.Count();
                // var totalPages = CountTotalPage(query, gridRequest);




                //  query = ApplyPaging(query, gridRequest);
                var orders = query.ToList();

                foreach (var order in orders)
                {
                    order.OrderLines = null;
                }

                var result = new PageData<Order>
                {
                    Data = orders,
                    //    Total = totalPages,
                    TotalRecords = total

                };

                return result;
            }
        }


        [HttpPost]
        public PageData<OrderDetail> GetPageOrdersDetailByOrderId(GridDataRequest gridRequest, int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<OrderDetail>();
                query = query.Where(a => a.Order.Id == id);
                var total = query.Count();
                query = ApplyFilterOrdersDetail(query, gridRequest);


                var totalPages = CountTotalPageOrdersDetail(query, gridRequest);

                query = ApplyPagingOrdersDetail(query, gridRequest);
                var orders = query.ToList();

                foreach (var order in orders)
                {
                    order.Order.OrderLines = null;
                }

                var result = new PageData<OrderDetail>
                {
                    Data = query.ToList(),
                    Total = totalPages,
                    TotalRecords = total

                };

                return result;
            }
        }

        [HttpPost]
        public PageData<OrderDetail> GetPageOrdersDetail(GridDataRequest gridRequest)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<OrderDetail>();
                var total = query.Count();
                query = ApplyFilterOrdersDetail(query, gridRequest);

                var totalPages = CountTotalPageOrdersDetail(query, gridRequest);

                query = ApplyPagingOrdersDetail(query, gridRequest);
                var query2 = session.Query<Order>();
                var orders = query2.ToList();
                foreach (var order in orders)
                {
                    order.OrderLines = null;
                }



                var result = new PageData<OrderDetail>
                {
                    Data = query.ToList(),
                    Total = totalPages,
                    TotalRecords = total

                };

                return result;
            }
        }


        private int CountTotalPageOrdersDetail(IQueryable<OrderDetail> query, GridDataRequest gridRequest)
        {
            var total = query.Count();
            gridRequest.TotalRecords = total;
            var shouldAddOne = !(total % gridRequest.PageSize == 0);
            int totalPages = 0;

            if (shouldAddOne)
                totalPages = total / gridRequest.PageSize + 1;
            else
                totalPages = total / gridRequest.PageSize;

            return totalPages;
        }




        private int CountTotalPage(IQueryable<Order> query, GridDataRequest gridRequest)
        {
            var total = query.Count();
            gridRequest.TotalRecords = total;
            var shouldAddOne = !(total % gridRequest.PageSize == 0);
            int totalPages = 0;

            if (shouldAddOne)
                totalPages = total / gridRequest.PageSize + 1;
            else
                totalPages = total / gridRequest.PageSize;

            return totalPages;
        }

        private IQueryable<Order> ApplyPaging(IQueryable<Order> query, GridDataRequest gridRequest)
        {
            return query.Skip(gridRequest.PageNumber * gridRequest.PageSize)
                                .Take(gridRequest.PageSize);
        }

        private IQueryable<OrderDetail> ApplyPagingOrdersDetail(IQueryable<OrderDetail> query, GridDataRequest gridRequest)
        {
            return query.Skip(gridRequest.PageNumber * gridRequest.PageSize)
                                .Take(gridRequest.PageSize);
        }





        private IQueryable<Order> ApplyFilter(IQueryable<Order> query, GridDataRequest gridRequest)
        {

            if (gridRequest.FilterBy != null)
            {
                if (gridRequest.FilterBy.Equals("All"))
                {
                    query = query.Where(a => a.Customer.Name != null);
                }
                else
                {
                    query = query.Where(a => a.Customer.Name.StartsWith(gridRequest.FilterBy));
                }
            }
            //filtrare dupa discount
            if (gridRequest.FilterBetween1 == 0 && gridRequest.FilterBetween2 == 0)
            {
                query = query.Where(a => a.Customer.DiscountPercent >= 0);
            }
            else if (gridRequest.FilterBetween1 == 0 && gridRequest.FilterBetween2 != 0)
            {

                query = query.Where((a => (a.Customer.DiscountPercent <= gridRequest.FilterBetween2)));
            }
            else
               if (gridRequest.FilterBetween1 != 0 && gridRequest.FilterBetween2 != 0)
            {
                query = query.Where((a => (a.Customer.DiscountPercent >= gridRequest.FilterBetween1) && a.Customer.DiscountPercent <= gridRequest.FilterBetween2));
            }
            else if (gridRequest.FilterBetween1 != 0 && gridRequest.FilterBetween2 == 0)
            {
                query = query.Where((a => (a.Customer.DiscountPercent >= gridRequest.FilterBetween1)));
            }

            //filtrare dupa data
            if (gridRequest.FilterDate1 == null && gridRequest.FilterDate2 == null)
            {
                DateTime dateTime = new DateTime(1970, 8, 18);
                query = query.Where(a => a.OrderDate >= dateTime);
            }
            else if (gridRequest.FilterDate1 == null && gridRequest.FilterDate2 != null)
            {

                gridRequest.FilterDate2 = DateTime.Parse(gridRequest.FilterDate2.ToString()).AddHours(17);
                query = query.Where((a => (a.OrderDate <= gridRequest.FilterDate2)));
            }
            else
               if (gridRequest.FilterDate1 != null && gridRequest.FilterDate2 != null)
            {
                gridRequest.FilterDate1 = DateTime.Parse(gridRequest.FilterDate1.ToString()).AddHours(-7);
                gridRequest.FilterDate2 = DateTime.Parse(gridRequest.FilterDate2.ToString()).AddHours(17);
                query = query.Where((a => (a.OrderDate >= gridRequest.FilterDate1) && a.OrderDate <= gridRequest.FilterDate2));
            }
            else if (gridRequest.FilterDate1 != null && gridRequest.FilterDate2 == null)
            {
                gridRequest.FilterDate1 = DateTime.Parse(gridRequest.FilterDate1.ToString()).AddHours(-7);
                query = query.Where((a => (a.OrderDate >= gridRequest.FilterDate1)));
            }

            return query;
        }


        private IQueryable<OrderDetail> ApplyFilterOrdersDetail(IQueryable<OrderDetail> query, GridDataRequest gridRequest)
        {

            if (gridRequest.FilterBy != null)
            {
                if (gridRequest.FilterBy.Equals("All"))
                {
                    query = query.Where(a => a.Order.Customer.Name != null);

                }
                else
                {
                    query = query.Where(a => a.Order.Customer.Equals(gridRequest.FilterBy));
                }
            }


            return query;
        }

        [HttpGet]
        public List<Customer> getNamesCustomers()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Customer>();
                return query.Where(a => a.Name != null).Distinct().ToList();
            }
        }

        [HttpGet]
        public List<Product> getProducts()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Product>();
                return query.Where(a => a.Name != null).Distinct().ToList();

            }
        }
        [HttpPost]
        public void saveOrder(Order order)
        {

            using (var session = sessionFactory.OpenSession())
            {
                if (!string.IsNullOrEmpty(order.Customer.Name))
                {
                    var repository = new OrderDetailService(session);  //repository de OrderDetail
                    var repo = new OrderService(session);
                    var lines = order.OrderLines;
                    order.OrderLines = new List<OrderDetail>();
                    // lines = new List<OrderDetail>();
                    DateTime localDate = DateTime.Now;
                    order.OrderDate = localDate;
                    repo.Save(order);//save Order ca sa iti genereze un OrderID
                    foreach (var line in lines)
                    {
                        line.Order = order;  //asta e pentru id
                                             //order.OrderLines.Add(line);
                        repository.Save(line);
                    }
                }
            }

        }

        [HttpPost]
        public void editOrder(Order order)
        {
            using (var session = sessionFactory.OpenSession())
            {

                if (!string.IsNullOrEmpty(order.Customer.Name))
                {
                    var repository = new OrderService(session);
                    var detailRepository = new OrderDetailService(session);

                    var lines = order.OrderLines;
                    order.OrderLines = new List<OrderDetail>();

                    repository.Update(order);

                    var oldLines = session.Query<OrderDetail>().Where(x => x.Order.Id == order.Id);

                    foreach (var oldLine in oldLines)
                    {
                        detailRepository.Delete(oldLine);
                    }

                    foreach (var line in lines)
                    {
                        line.Order = order;
                        line.Id = 0;

                        detailRepository.Save(line);
                    }
                }
            }
        }





        [HttpPost]
        public void deleteLine(OrderDetail orderLine)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var repository = new OrderDetailService(session);
                repository.Delete(orderLine);
            }
        }









        [HttpPost]
        public void save(OrderDetail orderDetail)
        {

            using (var session = sessionFactory.OpenSession())
            {
                var repository = new OrderDetailService(session);
                repository.Save(orderDetail);

            }

        }


        [HttpPost]
        public void editOrderDetail(OrderDetail orderDetail)
        {

            using (var session = sessionFactory.OpenSession())
            {
                session.Update(orderDetail);
                session.Flush();
            }

        }
        public OrderDetail getOrderDetailById(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var repository = new OrderDetailService(session);
                return repository.GetById(id);

            }
        }



        [HttpGet]
        public Order GetOrderById(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Order>();

                query = query.Where(p => p.Id == id);

                foreach (var order in query)
                {
                    foreach (var detail in order.OrderLines)
                        detail.Order = null;
                }

                return query.SingleOrDefault();
            }
        }


        [HttpPost]
        public void edit(Order order)
        {

            using (var session = sessionFactory.OpenSession())
            {
                session.Update(order);
                session.Flush();
            }

        }
        [HttpDelete]
        public void delete(int id)
        {

            using (var session = sessionFactory.OpenSession())
            {
                var order = session.Get<Order>(id);
                session.Delete(order);
                session.Flush();

            }

        }
        [HttpDelete]
        public void deleteOrderDetail(int id)
        {

            using (var session = sessionFactory.OpenSession())
            {

                var orderDetail = session.Get<OrderDetail>(id);
                orderDetail.Order.OrderLines = null;
                session.Delete(orderDetail);
                session.Flush();

            }

        }
        [HttpDelete]
        public void deleteOrder(int id)
        {

            using (var session = sessionFactory.OpenSession())
            {

                var order = session.Get<Order>(id);   //obtin obiectul cu orderid=id

                foreach (var line in order.OrderLines)
                {
                    line.Order = null;
                    session.Delete(line);

                }
                order.OrderLines = null;
                //dupa ce am sters toate orderLines
                session.Delete(order);
                session.Flush();


            }

        }

    }
}