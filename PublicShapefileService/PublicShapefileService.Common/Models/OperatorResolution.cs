using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PublicShapefileService.Common.Models
{
    public class OperatorResolution
    {
        public int OperatorResolutionId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Resolution { get; set; }
        public string OperatorName { get; set; }
        public string ResolutionDetails { get; set; }

       // public int ShapefileRequestId { get; set; }
        public ShapefileRequest ShapefileRequest { get; set; }
    }
}
