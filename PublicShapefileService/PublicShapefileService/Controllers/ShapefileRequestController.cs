using AttributeRouting.Web.Http;
using CaptchaMvc.HtmlHelpers;
using Ionic.Zip;
using Microsoft.Security.Application;
using PublicShapefileService.BusinessLogic.Interfaces;
using PublicShapefileService.BusinessLogic.Manager;
using PublicShapefileService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PublicShapefileService.Controllers
{
    public class ShapefileRequestController : Controller
    {
        //
        // GET: /Solicitant/
        private IManager manager { get; set; }

        public ShapefileRequestController()
        {
        }

        public ShapefileRequestController(IManager manager)
        {
            this.manager = manager ;
        }
       
        public ActionResult Solicitant()
        {
            return View();
        }


        public ActionResult Menu()
        {
            return View("AddShapefileRequest");
        }


        public ActionResult DownloadFiles()
        {
            Common.Models.DownloadLink downloadLink1 = new Common.Models.DownloadLink();
            downloadLink1.DownloadLinkId = 1;
            downloadLink1.InternalLink = "www.domain.ro";
            downloadLink1.Timestamp = DateTime.Now;
            downloadLink1.Validity = 24;

            Common.Models.DownloadLink downloadLink2 = new Common.Models.DownloadLink();
            downloadLink2.DownloadLinkId = 2;
            downloadLink2.InternalLink = "www.domain2.ro";
            downloadLink2.Timestamp = new DateTime(2018, 1, 1);
            downloadLink2.Validity = 24;
            TimeSpan span = DateTime.Now-downloadLink1.Timestamp;
            int hours = span.Days * 24 + span.Hours;
            if (hours < downloadLink1.Validity)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(Server.MapPath("~"));
                    zip.Save(Server.MapPath("~/Arhiva.zip"));
                    return File(Server.MapPath("~/Arhiva.zip"),
                                       "application/zip", "Arhiva.zip");
                }
            }
            else
            {
                ViewBag.ErrorValidationDownload = "Continutul nu mai este disponibil!";
                return View("DownloadFiles");
            }
                //return View("DownloadFiles");
        }


        //[HttpPost]
        [ValidateInput(false)]
        public ActionResult AddShapefileRequest(ShapefileRequest shapefileRequest)
        {
            try
            {
                if (verificaManipulariJavaScriptXSS(shapefileRequest))
                {
                    ViewBag.MessageManipulariJavaScript = "Nu sunt permise manipulari JavaScript/XSS(taguri, scripturi html)";
                }

                if (ModelState.IsValid && !verificaManipulariJavaScriptXSS(shapefileRequest))
                {

                    if (this.IsCaptchaValid("Captcha is not valid"))
                    {
                        ViewBag.MessageSuccessAddSolicitant = "Solicitantul a fost adaugat cu succes!";
                        Common.Models.ShapefileRequest shapefileRequestBussiness = new Common.Models.ShapefileRequest();
                        Convertor.CopyObject(shapefileRequest, shapefileRequestBussiness);
                        manager.StartForwardFlux(shapefileRequestBussiness);
                        return View("Result", shapefileRequest);
                        //return RedirectToAction("Index");
                    }
                    ViewBag.ErrMessageCaptcha = "Codul Captcha nu este valid.";
                    return View("AddShapefileRequest");
                }
                else
                {

                    return View("AddShapefileRequest");
                }
            }
            catch
            {
                return View();
            }
        }

        



        //
        // GET: /Index/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Index/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Index/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Index/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Index/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Index/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Index/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //vom face o metoda care verifica fiecare camp si vede daca avem manipulari javaScript(folosindu-ne de biblioteca XSS) 
        public bool verificaManipulariJavaScriptXSS(ShapefileRequest op)
        {
            //incercam sa luam campurile fara scripturi, taguri html
            //daca avem manipulari javascript atunci va returna true altfel false
            string numeSolicitant = Sanitizer.GetSafeHtmlFragment(op.SolicitantName);
            string cui = Sanitizer.GetSafeHtmlFragment(op.CUI);
            string zonaDeInteres = Sanitizer.GetSafeHtmlFragment(op.InterestArea);
            string emailSolicitant = Sanitizer.GetSafeHtmlFragment(op.SolicitantEmail);
            string detaliiCerere = Sanitizer.GetSafeHtmlFragment(op.RequestDetails);
            if ((!numeSolicitant.Equals(op.SolicitantName) && (op.SolicitantName != null)))
            {
                return true;
            }
            if (!cui.Equals(op.CUI) && (op.CUI != null))
            {
                return true;
            }
            if (!emailSolicitant.Equals(op.SolicitantEmail) && (op.SolicitantEmail != null))
            {
                return true;
            }
            if (!detaliiCerere.Equals(op.RequestDetails) && (op.RequestDetails != null))
            {
                return true;
            }
            if (!zonaDeInteres.Equals(op.InterestArea) && (op.InterestArea != null))
            {
                return true;
            }
            return false;
        }



    }
}
