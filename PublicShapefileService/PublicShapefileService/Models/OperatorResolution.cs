using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicShapefileService.Models
{
    public class OperatorResolution
    {

        public int OperatorResolutionId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Resolution { get; set; }
        public string OperatorName { get; set; }
        public string ResolutionDetails { get; set; }
        public int ShapefileRequestId { get; set; }

        public  Common.Models.ShapefileRequest ShapefileRequest { get; set; }
    }
}