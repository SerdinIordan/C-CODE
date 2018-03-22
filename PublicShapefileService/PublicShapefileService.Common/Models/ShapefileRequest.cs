using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PublicShapefileService.Common.Models
{
    public class ShapefileRequest
    {
        public int ShapefileRequestId { get; set; }
        public string SolicitantName { get; set; }
        public string CUI { get; set; }
        public string SolicitantEmail { get; set; }
        public string RequestDetails { get; set; }
        public Locality Locality { get; set; }
        public DownloadLink DownloadLink { get; set; }
        public OperatorResolution OperatorResolution { get; set; }
        

        public virtual ICollection<Layer> Layers { get; set; }

    }
}

