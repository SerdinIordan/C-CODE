using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebStore.Backend.Models;
using WebStore.Business;
using WebStore.Common.Entities;
using WebStore.Common.Models;

namespace WebStore.Backend.Controllers
{
    public class ReportsController : ApiController
    {
        private static string connectionString =
           @"Data Source=INTERN2017-25;Initial Catalog=WebstoreP;User ID=sa;Password=1234%asd;";

        private static ISessionFactory sessionFactory;

        static ReportsController()
        {
            var config = new NhConfig(connectionString);
            sessionFactory = config.Create();
        }

        [HttpPost]
        public List<Report> getPageReport(GridDataRequest gridRequest)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Order>();  //facem query de Order pentru a returna datele de tip Report
                var total = query.Count();
                query = query.OrderByDescending(c => c.OrderDate);
                query = ApplyFilter(query, gridRequest);

                var orders = query.ToList();

                //trebuie sa returnam un var reports
                var reports = new List<Report>();
                foreach (var order in orders)
                {
                    var report = new Report();
                    report.Id = order.Id;
                    report.LinesCount = order.OrderLines.Count();
                    report.OrderLines = order.OrderLines;
                    report.OrderDate = order.OrderDate;
                    // report.Quantity
                    //   report.Product
                    foreach (var orderLine in report.OrderLines)
                    {
                        report.TotalPrice = report.TotalPrice + orderLine.TotalPrice;
                        report.TotalPriceWithoutDiscount = report.TotalPriceWithoutDiscount + orderLine.TotalPriceWithoutDiscount;
                    }

                    reports.Add(report);

                }
                ReportSummary reportSummary = new ReportSummary();
                foreach (var report in reports)
                {
                    reportSummary.TotalPrice = reportSummary.TotalPrice + report.TotalPrice;
                    reportSummary.TotalPriceWithoutDiscount = reportSummary.TotalPriceWithoutDiscount + report.TotalPriceWithoutDiscount;
                }

                foreach (var order in orders)
                {
                    order.OrderLines = null;
                }
                //int number;



                return reports;
            }


        }


        [HttpPost]
        public ReportSummary getReportSummary(GridDataRequest gridRequest)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Order>();  //facem query de Order pentru a returna datele de tip Report
                var total = query.Count();
                query = query.OrderByDescending(c => c.OrderDate);
                query = ApplyFilter(query, gridRequest);


                //  var totalPages = CountTotalPage(query, gridRequest);
                //  query = ApplyPaging(query, gridRequest);


                var orders = query.ToList();

                //trebuie sa returnam un var reports
                var reports = new List<Report>();
                foreach (var order in orders)
                {
                    var report = new Report();
                    report.Id = order.Id;
                    report.LinesCount = order.OrderLines.Count();
                    report.OrderLines = order.OrderLines;
                    report.OrderDate = order.OrderDate;
                    // report.Quantity
                    //   report.Product
                    foreach (var orderLine in report.OrderLines)
                    {
                        report.TotalPrice = report.TotalPrice + orderLine.TotalPrice;
                        report.TotalPriceWithoutDiscount = report.TotalPriceWithoutDiscount + orderLine.TotalPriceWithoutDiscount;
                    }

                    reports.Add(report);

                }
                ReportSummary reportSummary = new ReportSummary();
                foreach (var report in reports)
                {
                    reportSummary.TotalPrice = reportSummary.TotalPrice + report.TotalPrice;
                    reportSummary.TotalPriceWithoutDiscount = reportSummary.TotalPriceWithoutDiscount + report.TotalPriceWithoutDiscount;
                }

                foreach (var order in orders)
                {
                    order.OrderLines = null;
                }
                //int number;

                return reportSummary;
            }


        }


        [HttpPost]
        private IQueryable<Order> ApplyFilter(IQueryable<Order> query, GridDataRequest gridRequest)
        {


            //filtrare dupa data
            if (gridRequest.FilterDate1 == null && gridRequest.FilterDate2 == null)
            {
                DateTime dateTime = new DateTime(1970, 8, 18);
                query = query.Where(a => a.OrderDate >= dateTime);
            }
            else if (gridRequest.FilterDate1 == null && gridRequest.FilterDate2 != null)
            {
                gridRequest.FilterDate2 = DateTime.Parse(gridRequest.FilterDate2.ToString()).AddHours(17);

                // gridRequest.FilterDate2 = DateTime.Parse(gridRequest.FilterDate1+"");
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









    }
}