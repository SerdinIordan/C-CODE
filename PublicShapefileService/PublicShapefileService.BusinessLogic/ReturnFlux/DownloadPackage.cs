using PublicShapefileService.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PublicShapefileService.BusinessLogic.ReturnFlux
{

    // Encapsulates the DownloadLink and the Files associated
    public class DownloadPackage
    {
        public DownloadLink DownloadLink { get; set; }
        public FileStream[] Files { get; set; }



    }
}
