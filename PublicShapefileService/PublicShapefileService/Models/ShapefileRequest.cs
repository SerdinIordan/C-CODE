using PublicShapefileService.Common.Models;
using PublicShapefileService.Models.FieldsValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicShapefileService.Models
{
    public class ShapefileRequest
    {
        public int ShapefileRequestId { get; set; }

        public virtual DownloadLink DownloadLink { get; set; }
        public virtual OperatorResolution OperatorResolution { get; set; }
        public virtual ICollection<Layer> Layers { get; set; }
        public Locality Locality { get; set; }

        [Required(ErrorMessage = "Va rugam sa introduceti un nume ")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Numele solicitantului trebuie sa contina intre 3 si 30 de caractere")]
        [ValidationNameAttribute(ErrorMessage = "Nu sunt permise caractere speciale sau cifre")]
        [SQLInjectionAttribute(ErrorMessage = "Nu sunt permise sintaxe SQL")]
        public string SolicitantName { get; set; }

        [Required(ErrorMessage = "Va rugam sa introduceti un cod CUI")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Codul CUI trebuie sa contina intre 5 si 20 de cifre")]
        [ValidationCUIAttribute(ErrorMessage= "Codul de inregistrare trebuie sa fie unul valid")]
        [SQLInjectionAttribute(ErrorMessage = "Nu sunt permise sintaxe SQL")]
        public string CUI { get; set; }

        [Required(ErrorMessage = "Va rugam sa introduceti o zona de interes")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Zona de interes trebuie sa contina intre 5 si 20 caractere")]
        [ValidationInterestArea(ErrorMessage ="Nu sunt permise caractere speciale sau cifre")]
        [SQLInjectionAttribute(ErrorMessage = "Nu sunt permise sintaxe SQL")]
        public string InterestArea { get; set; }
        

        [Required(ErrorMessage = "Va rugam sa introduceti o adresa de email")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Adresa de email trebuie sa contina intre 5 si 30 de caractere")]
        [ValidationEmailAttribute(ErrorMessage = "Emailul trebuie sa fie unul valid")]
        [SQLInjectionAttribute(ErrorMessage = "Nu sunt permise sintaxe SQL")]
        public string SolicitantEmail { get; set; }

        
        [StringLength(150, MinimumLength = 0, ErrorMessage = "Detaliile trebuie sa contina maxim 100 de caractere")]
        [ValidationRequestDetail(ErrorMessage ="Detaliile trebuie sa fie unele valide")]
        public string RequestDetails { get; set; }

    }
}