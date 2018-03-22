using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PublicShapefileService.Models.FieldsValidation
{
    public class ValidationCUIAttribute: ValidationAttribute
    {
        //la Cod CUI nu trebuie sa avem incluse spatii si putem avea caractere
        char[] caractersSpecial = { '/', '*','-','+','~','!','@','#','$','%','^','&','*','(',')','-',
                     '_','=','[','{','}',']','|','\\',':',';','"','<',',','>','.','/','?' ,' ','\''};

        public override bool IsValid(object value)
        {
            string s = (string)value;
            bool p = true;
            if (s != null && ((s.IndexOfAny(caractersSpecial) != -1) ))
            {
                p = false;
            }
            return p;

            /* Regex regexCUI = new Regex(@"(^[-+]?\d+(\.\d+)?$)");
             if (s!=null && !regexCUI.IsMatch(s))
             {
                 return false;
             }
             return true;*/
        }

    }
}