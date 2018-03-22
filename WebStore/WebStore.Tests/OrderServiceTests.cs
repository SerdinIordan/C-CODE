using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Common.Services;
using WebStore.Common.Entities;
using System.Collections.Generic;
using NHibernate;
using WebStore.Business;
using WebStore.Business.Repositories;
using WebStore.Common.Transformers;

namespace WebStore.Tests
{
    [TestClass]
    public class OrderServiceTests : EntityServiceTests<Order, IOrderService>
    {


        private string connectionString = @"Data Source=INTERN2017-25;Initial Catalog=WebStore;User ID=sa;Password=1234%asd";
        protected ISession session;
        protected ISessionFactory sessionFactory;

        [TestInitialize]
        public void Initialize()
        {
            sessionFactory = new NhConfig(connectionString).Create();
            session = sessionFactory.OpenSession();
            // session = sessionFactory.OpenSession();

            //            using (var command = session.Connection.CreateCommand())
            //      {
            //  command.CommandText = "delete Country";
            //   command.ExecuteNonQuery();
            //    }
        }

        [TestCleanup]
        public void Cleanup()
        {
            session.Flush();
            session.Dispose();
        }

        [TestMethod]
        public void WhenReadingFileAndInsertingInDbWeGet245()
        {
            var repository = new ProductService(session);
            IList<Product> products = new List<Product>();
            Product product = new Product();
            product.Id = 5;
            product.Name = "Bors";
            product.ListPrice = 200;
            products.Add(product);
            // var parser = new CsvCountrieFileParser();

            var target = new CsvFileToDbTransformer(repository, products);
            target.Execute();
            //  target.Execute(@"Countries.csv");

            var count = repository.GetAll().Count;

            Assert.AreEqual(1, count);
        }

  /*      [TestMethod]
        public void WhenReadingFileAndInsertingInDbWeGet246()
        {
            var repository = new ProductService(session);
            IList<Product> products = new List<Product>();
            Product product = new Product();
            // product.Id = 5;
            // product.Name = "Bors";
            // product.ListPrice = 200;
            int id = 2;
          //  products.Add(product);
           // repository.Update
            // var parser = new CsvCountrieFileParser();

            var target = new CsvFileToDbTransformer(repository, products);
            target.Execute();
            //  target.Execute(@"Countries.csv");

            var count = repository.GetAll().Count;

            Assert.AreEqual(1, count);
        }
        */







        private ICustomerService CreateCustomerService()
        {

            throw new NotImplementedException();
        }

        private IProductService CreateProductService()
        {




            throw new NotImplementedException();
        }

        protected override IOrderService CreateTarget()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void WhenAddingOneOrderWith2LinesWeGetCorrectReportOnDates()
        {
            var target = CreateTarget();

            var order = CreateInput();

            target.Save(order);

            var summaries = target.ReportByDateRange(new DateTime(2017, 10, 1), new DateTime(2017, 10, 30));

            Assert.IsNotNull(summaries);
            Assert.AreEqual(1, summaries.Length);

            var summary = summaries[0];
            Assert.IsNotNull(summary);
            Assert.AreEqual(2, summary.LinesCount);
            Assert.AreEqual(270, summary.TotalPriceWithoutDiscount);
            Assert.AreEqual(270 - 27, summary.TotalPrice);
        }

        protected override Order CreateInput()
        {
            return new Order
            {
                OrderDate = new DateTime(2017, 10, 11),
                DiscountPercent = 10,
                Customer = CreateCustomerService().GetByName("IBM"),
                OrderLines = new List<OrderDetail>
                   {
                       new OrderDetail { Price = 10, Quantity = 6, Product = CreateProductService().GetByName("A4 Paper") },
                       new OrderDetail { Price = 30, Quantity = 7, Product = CreateProductService().GetByName("Printer EPSON X1") }
                   }
            };
        }

        protected override void AssertAreEqual(Order expected, Order actual)
        {
            throw new NotImplementedException();
        }
    }
}
