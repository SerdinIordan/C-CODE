using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PublicShapefileService.Models.FieldsValidation
{
    public class SQLInjectionAttribute:ValidationAttribute
    {
        string[] SQLSyntaxUpperCase = {"ALTER","CREATE","DELETE","DROP","EXECUTE","EXEC","INSERT",
                "INSERT INTO","MERGE","SELECT","UPDATE","VERSION","ORDER","UNION","UNION ALL"};
        string[] SQLSyntaxLowerCase = {"alter","create","delete","drop","execute","exec","insert",
                "insert into","merge","select","update","version","order","union","union all"};

        public override bool IsValid(object value)
        {
            string s = (string)value;
            bool p = true;
            //vedem daca sirul contine sintaxe SQL 
            //daca nu sunt atunci p va fi setat pe true
            if (s != null)
            {
               p= SQLSyntaxUpperCase.Where(sir=>s.Contains(sir.ToLower()) || s.Contains(sir.ToUpper() )).Count() == 0;
            }
            


            return p;
            
        }

    }
}