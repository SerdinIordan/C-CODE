using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PublicShapefileService.Models.FieldsValidation
{
    public class ValidationEmailAttribute: ValidationAttribute
    {
        //la email am scos @ _ si .
        char[] caractersSpecial = { '/', '*','-','+','~','!','#','$','%','^','&','*','(',')','-',
                     '=','[','{','}',']','|','\\',':',';','"','<',',','>','/','?','\'' };

        public override bool IsValid(object value)
        {

            string s = (string)value;
            bool p = true;

            if (s != null && (s.IndexOfAny(caractersSpecial) != -1))
            {
                p = false;
            }


            Regex regexEmail = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            if (s!=null && !regexEmail.IsMatch(s))
            {
                p= false;
            }
            return p;
        }
    }
}