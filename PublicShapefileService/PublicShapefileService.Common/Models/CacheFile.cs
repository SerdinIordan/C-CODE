using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicShapefileService.Models
{
    public class CacheFile
    {
        public String CountyCode { get; set; }
        public String SirutaCode { get; set; }
        public String Name { get; set; }
        public String[] Layers { get; set; }

        public CacheFile(String countyCode, String sirutaCode, String name)
        {
            this.CountyCode = countyCode;
            this.SirutaCode = sirutaCode;
            this.Name = name;
        }

        public CacheFile(String countyCode, String sirutaCode, String name, String[] layers)
        {
            this.CountyCode = countyCode;
            this.SirutaCode = sirutaCode;
            this.Name = name;
            this.Layers = layers;
        }
    }
}