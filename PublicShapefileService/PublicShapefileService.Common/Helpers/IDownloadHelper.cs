using PublicShapefileService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PublicShapefileService.Helpers
{
    public interface IDownloadHelper
    {
        FileStream[] DownloadFile(CacheFile locality, String[] layers);
    }
}
