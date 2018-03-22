using PublicShapefileService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicShapefileService.BusinessLogic.ReturnFlux
{
    // Builds the DownloadLink field of the OperatorResolution passed by the second form
    class LinkManager
    {
        
        const string domain = "www.domain.ro"; 

        // First step of the return flux
        public DownloadLink BuildLink (OperatorResolution _resolution)
        {
            DownloadLink downloadLink = new DownloadLink
            {
                PublicId = Guid.NewGuid()
            };

            downloadLink.ShapefileRequest = _resolution.ShapefileRequest;
            downloadLink.Timestamp = DateTime.Now;
            downloadLink.Validity = 24;
            downloadLink.InternalLink = domain + "/" + downloadLink.PublicId;               // the guid is used to create the unique download link

            return downloadLink;
        }
    }
}
