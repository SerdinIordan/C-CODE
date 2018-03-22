using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicShapefileService.Common.Models
{
    public class LayerShapefileRequest
    {
        public int LayerId { get; set; }
        public int ShapefileRequestId { get; set; }


        public ShapefileRequest ShapefileRequest { get; set; }
        public Layer Layer { get; set; }
    }
}
