using AttributeRouting.Web.Http;
using PublicShapefileService.BusinessLogic.Interfaces;
using PublicShapefileService.DataLayer;
using PublicShapefileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicShapefileService.Controllers
{
    public class OperatorController : Controller
    {
        //
        // GET: /DetailsOperator/
           private PublicShapefileServiceContext publicShapefileServiceOperations =
              PublicShapefileServiceContext.Instance;
        private IManager manager { get; set; }

        public OperatorController()
        {

        }

        public OperatorController(IManager manager)
        {
            this.manager = manager;
        }

       /* [HttpGet]
        public ActionResult Operator()
        {
            ViewData["AcceptShapefileRequest"] = createObject();
            return View();
        }*/
       

        [HttpGet]
        [HttpRoute("Approval/{id}")]
        public ActionResult Approval(int id)
        {
            var shapeFileRequestBussiness = publicShapefileServiceOperations.GetShapefileRequestById(id);
            var shapefileRequestFrontend = AutoMapper.Mapper.Map<Common.Models.ShapefileRequest, Models.ShapefileRequest>(shapeFileRequestBussiness);

            shapefileRequestFrontend.OperatorResolution = new OperatorResolution();

            //ViewData["AcceptShapefileRequest"] = shapefileRequestFrontend;
            return View("Operator", shapefileRequestFrontend);
        }


        
       


       [HttpPost]
       [HttpRoute("Operator/AcceptRequest")]
        public ActionResult AcceptRequest(OperatorResolution operatorResolution)
        {
             try
            {

            ResolveRequest(operatorResolution, true);

            ViewBag.MessageAcceptRequest = "Cererea a fost aprobata cu succes!";
             return View("Result");
           }
            catch
            {
                //TODO: Log error				
                return View();
            }

        }

        [HttpPost]
        [HttpRoute("Operator/RejectRequest")]
        public ActionResult RejectRequest(OperatorResolution operatorResolution)
        {
            try
            {
                ResolveRequest(operatorResolution, false);
                ViewBag.MessageRejectRequest = "Cererea a fost respinsa cu succes!";
                return View("Result");
                // return View();
            }
            catch
            {
                //TODO: Log error				
                return View();
            }

        }
        private void ResolveRequest(OperatorResolution operatorResolution, bool accept)
        {
                var operatorResolutionSave = new OperatorResolution();
                
                operatorResolutionSave.ShapefileRequest = publicShapefileServiceOperations.GetShapefileRequestById(operatorResolution.ShapefileRequestId);
                operatorResolutionSave.OperatorName = operatorResolution.OperatorName;
                operatorResolutionSave.ResolutionDetails = operatorResolution.ResolutionDetails;
                //se completeaza detalii daca se respinge cererea
                operatorResolutionSave.Timestamp = DateTime.Now;
                operatorResolutionSave.Resolution = accept;

                var operatorResolutionBussinessSave = AutoMapper.Mapper.Map<Models.OperatorResolution, Common.Models.OperatorResolution>(operatorResolutionSave);
                manager.StartReturnFlux(operatorResolutionBussinessSave);
        }

    }
}
