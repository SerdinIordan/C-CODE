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
using WebStore.Common.Models;

namespace WebStore.Backend.Controllers
{
    public class ProductsController:ApiController
    {
        private static string connectionString =
            @"Data Source=INTERN2017-25;Initial Catalog=WebstoreP;User ID=sa;Password=1234%asd;";

        private static ISessionFactory sessionFactory;

        static ProductsController()
        {
            var config = new NhConfig(connectionString);
            sessionFactory = config.Create();
        }

        [HttpPost]
        public PageData<Product> GetPage(GridDataRequest gridRequest)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Product>();
               
                query = ApplyFilter(query, gridRequest);
                var total = query.Count();
                // var totalPages = CountTotalPage(query, gridRequest);




                // query = ApplyPaging(query, gridRequest);



                var result = new PageData<Product>
                {
                    Data = query.ToList(),
                   // Total = totalPages,
                    TotalRecords = total

                };

                return result;
            }
        }
        [HttpPost]
        public int  GetNumberOfProducts(GridDataRequest gridRequest)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Product>();
                query = ApplyFilter(query, gridRequest);
                var total = query.Count();


                // var totalPages = CountTotalPage(query, gridRequest);

                int numarProduse = total;


                // query = ApplyPaging(query, gridRequest);



               

                return numarProduse;
            }
        }


        /*  private int CountTotalPage(IQueryable<Product> query, GridDataRequest gridRequest)
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

          private IQueryable<Product> ApplyPaging(IQueryable<Product> query, GridDataRequest gridRequest)
          {
              return query.Skip(gridRequest.PageNumber * gridRequest.PageSize)
                                  .Take(gridRequest.PageSize);
          }
          */
        

        private IQueryable<Product> ApplyFilter(IQueryable<Product> query, GridDataRequest gridRequest)
        {
            // query = query.Where(@"Code.StartsWith(@0)", "Sl");
            // query=query.Where(a=>a.Name.StartsWith(gridRequest))7
            if (gridRequest.FilterValue != null)
            {
                query = query.Where(a => a.Name.StartsWith(gridRequest.FilterValue));
            }

            if (gridRequest.FilterBetween1 == 0 && gridRequest.FilterBetween2 == 0)
            {
                query = query.Where(a => a.ListPrice >= 0);
            }
            else if (gridRequest.FilterBetween1==0 && gridRequest.FilterBetween2!=0)
            {

                query = query.Where((a => (a.ListPrice <= gridRequest.FilterBetween2)));
            }
            else
               if (gridRequest.FilterBetween1 != 0 && gridRequest.FilterBetween2 != 0)
            {
                query = query.Where((a => (a.ListPrice >= gridRequest.FilterBetween1) && a.ListPrice <= gridRequest.FilterBetween2));
            }
            else if (gridRequest.FilterBetween1 != 0 && gridRequest.FilterBetween2 ==0)
            {
                query = query.Where((a => (a.ListPrice >= gridRequest.FilterBetween1)));
            }

            
            return query;
        }

        [HttpPost]
        public void save(Product product)
        {

            using (var session = sessionFactory.OpenSession())
            {
                var repository = new ProductService(session);
                repository.Save(product);

            }

        }

        public Product getProductById(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var repository = new ProductService(session);
                return repository.GetById(id);

            }
        }


        [HttpGet]
        public int GetOrderByProductID(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Order>();


                foreach (var order in query)
                {
                    foreach (var detail in order.OrderLines)
                        if (detail.Product.Id == id)
                        {
                            return 1;
                        }
                }

                return 0;
            }
        }
        [HttpPost]
        public ProductSummary getDetailProduct(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<OrderDetail>();

                query = query.Where(a => a.Product.Id == id);
                if (query.Count() != 0)
                {
                    ProductSummary productSummary = new ProductSummary();
                    productSummary.Id = id;
                    productSummary.OrdersCount = query.Count();
                    productSummary.TotalQuantityOrdered = query.Sum(a => a.Quantity);
                    productSummary.TotalAmountCharged = query.Sum(a => a.TotalPrice);
                    return productSummary;
                }
                else
                {
                    ProductSummary productSummary = new ProductSummary();
                    productSummary.OrdersCount = query.Count();
                    productSummary.TotalQuantityOrdered = 0;
                    productSummary.TotalAmountCharged = 0;
                    return productSummary;
                }

            }
        }

       

        [HttpGet]
        public List<string> getNameProducts()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Product>();
                return query.Select(a => a.Name).Distinct().ToList();

            }
        }
        [HttpGet]
        public List<Product> getProducts()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Product>();
                return  query.Where(a => a.Name!=null).Distinct().ToList();
               

            }
        }
		
		
		
		
		
        [HttpPost]
        public void edit(Product product)
        {

            using (var session = sessionFactory.OpenSession())
            {
                session.Update(product);
                session.Flush();
            }

        }
      [HttpDelete]
        public void delete(int id)
        {

            using (var session = sessionFactory.OpenSession())
            {
                var product = session.Get<Product>(id);
                session.Delete(product);
                session.Flush();

            }

        }

       
    }
}