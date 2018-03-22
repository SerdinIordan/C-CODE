using PublicShapefileService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicShapefileService.Common.Interfaces
{
    public interface IRepository 
    {
        DownloadLink SaveDownloadLink(DownloadLink downloadLink);
        OperatorResolution SaveOperatorResolution(OperatorResolution operatorResolution);
        ShapefileRequest SaveShapefileRequest(ShapefileRequest shapefileRequest);

        DownloadLink GetDownloadLink(Guid publicId);
        ShapefileRequest GetShapefileRequestById(int shapeFileRequestId);
    }
}
