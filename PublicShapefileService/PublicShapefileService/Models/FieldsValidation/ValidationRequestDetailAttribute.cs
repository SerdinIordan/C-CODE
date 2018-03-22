﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicShapefileService.Models.FieldsValidation
{
    public class ValidationRequestDetailAttribute : ValidationAttribute
    {
        // permitem  -  ! @ $ % ( ) _ = : ; " , . ? 
        char[] caractersSpecial = { '/', '*','+','~','#','^','&','*',
                     '[','{','}',']','|','\\','<','>','/' };

        //char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public override bool IsValid(object value)
        {
            string s = (string)value;
            bool p = true;
            if (s != null && (s.IndexOfAny(caractersSpecial) != -1) )
            {
                p = false;
            }
            return p;
        }
    }
}