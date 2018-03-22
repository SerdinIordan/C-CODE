using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Common.Models;

namespace WebStore.Backend.Models
{
    public class PageData<T>
    {

        public int Total { get; set; }

        public IEnumerable<T> Data { get; set; }
        public int TotalRecords { get; set; }
        public int TotalR { get; set; }
        public ReportSummary ReportSummary { get; set; }
    }
}