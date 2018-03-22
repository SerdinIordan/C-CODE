using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using PublicShapefileService.Common.Models;
using PublicShapefileService.Helpers;
using PublicShapefileService.BusinessLogic.ForwardFlux;
using PublicShapefileService.BusinessLogic.ReturnFlux;
using PublicShapefileService.BusinessLogic.Interfaces;

namespace PublicShapefileService.BusinessLogic.Manager
{



    // BussinessLogic manager
    public class Manager : IManager
    {
        private Validator _validator { get; set; }
        private EFManager _efManager { get; set; }
        private MailManager _mailManager { get; set; }
        private LinkManager _linkManager { get; set; }
        private DownloadPackage _downloadPackage { get; set; }
        private IDownloadHelper _downloadHelper { get; set; }

        public Manager(IDownloadHelper downloadHelper)
        {
            this._downloadHelper = downloadHelper;

            _validator = new Validator();
            _efManager = new EFManager();
            _mailManager = new MailManager();
            _linkManager = new LinkManager();
            _downloadPackage = new DownloadPackage();
        }


        // Starts from the first form and ends at the request email. Called from the controller of the first form
        public void StartForwardFlux(ShapefileRequest _request)
        {
           // if (_validator.ValidateRequest(_request))
           // {
                _efManager.RegisterRequest(_request);
                _mailManager.SendRequest(_request);
           // }
        }


        // Starts from the resolution form and ends at the resolution email. Called from the controller of the second form
        public DownloadPackage StartReturnFlux(OperatorResolution _resolution)
        {
            if (_resolution.Resolution)
            {
                _resolution.ShapefileRequest.DownloadLink = _linkManager.BuildLink(_resolution);
                _downloadPackage.DownloadLink = _resolution.ShapefileRequest.DownloadLink;
                
            }

            //_downloadPackage.Files = downloadHelper.DownloadFile("va urma");
            _efManager.RegisterResolution(_resolution);
            _mailManager.SendResponse(_resolution);

            return _downloadPackage;
        }


    }
}
