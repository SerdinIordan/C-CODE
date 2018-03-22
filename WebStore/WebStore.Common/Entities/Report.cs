using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Common.Entities
{
    public class Report
    {
        public virtual int Id { get; set; }
       // public virtual Product Product { get; set; }
        public virtual IList<OrderDetail> OrderLines { get; set; }   //vom avea in report toate orderDetailurile de la orderId
    //    public virtual decimal Price { get; set; }
      //  public virtual decimal Quantity { get; set; }
        public virtual decimal TotalPriceWithoutDiscount { get; set; }
        public virtual decimal TotalPrice { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual decimal LinesCount { get; set; }

    }
}
