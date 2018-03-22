using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicShapefileService.Common.Models
{
    public class Locality
    {
        public int LocalityId { get; set; }
        public string CountyCode { get; set; }
        public string SirutaCode { get; set; }
        public string LocalityName { get; set; }
        public string FullName { get; set; } //toponim

        public virtual ICollection<ShapefileRequest> GetShapefileRequests { get; set; }

    }
}
