using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PublicShapefileService.Common.Models
{
    public class DownloadLink 
    {
        public int DownloadLinkId { get; set; }
        public int Validity { get; set; }
        public string InternalLink { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid PublicId { get; set; }

        public ShapefileRequest ShapefileRequest {get;set;}
    }
}
