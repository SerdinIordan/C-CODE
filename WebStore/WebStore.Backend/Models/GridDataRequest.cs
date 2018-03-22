using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Backend.Models
{
    public class GridDataRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public string FilterBy { get; set; }
        public string FilterOperator { get; set; }
        public string FilterValue { get; set; }
        public int FilterBetween1 { get; set; }
        public int FilterBetween2 { get; set; }
        public int FilterRecords { get; set; }
        public int TotalRecords { get; set; }
        public string Option { get; set; }
        public IList<string> ListCustomersName { get; set; }
        public DateTime? FilterDate1 { get; set; }
        public DateTime? FilterDate2 { get; set; }
        //pentru OrderDetail
        public int DiscountPercent { get; set; }
        //pentru add orderdetail
        public string ProductName { get; set; }
        public int PretSelect { get; set; }
    
}
}