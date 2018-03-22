using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicShapefileService.Common.Models
{
    public class Layer
    {
        public int LayerId { get; set; }
        public string LayerName { get; set; }

        public virtual ICollection<ShapefileRequest> ShapefileRequests { get; set; }
    }
}
