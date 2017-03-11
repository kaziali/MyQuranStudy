using System;

namespace QuranStudy
{
    public class FunctionLib
    {
        public FunctionLib()
        {
        }
        public static bool IsNumeric(object value)
        {
            try
            {
                double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
       
        public static bool IsInteger(object value)
        {
            try
            {
                double d = System.Int32.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidEmail(string email, ref string msg)
        {
            if (email.Length < 7) //len("a@bb.cc")=7
            {
                msg = "Invalid email address.";
                return false;
            }
            else if (email.IndexOf("@") < 1) // @abc.com not allowed 
            {
                msg = "Invalid email address.";
                return false;
            }
            else if (email.IndexOf(".") < 1)  // xyz@abc not allowed  
            {
                msg = "Invalid email address.";
                return false;
            }
            //else if (email.IndexOf(".") >= email.Length-2)  //   a@bb.c not allowed, at least 2 chars after dot 
            //{
            //    msg = "Invalid email address.";// +email.IndexOf(".").ToString() + email.Length.ToString();
            //    return false;
            //}
            else if (email.IndexOf(" ") > 0) // xyz@ abc not allowed 
            {
                msg = "Invalid email address.";
                return false;
            }
            else if (email.IndexOf("@.") > 1)   // xyz@.abc not allowed 
            {
                msg = "Invalid email address.";
                return false;
            }
            else if (email.IndexOf(".@") > 1)   // xyz.@abc.com not allowed 
            {
                msg = "Invalid email address.";
                return false;
            }
            else if (email.IndexOf("@") != email.LastIndexOf("@")) //no multiple email ids
            {
                msg = "Invalid email address.";
                return false;
            }
            else   //must contain at least 1 letter. 11@11.11 not allowed.
            {
                bool isValid = false;
                for (int i = 0; i < email.Length; i++)
                {
                    char ch = email.Substring(i, 1).ToCharArray()[0];
                    int ascVal = (int)ch;
                    if ((ascVal >= 65 && ascVal <= 90)
                        || (ascVal >= 97 && ascVal <= 122))
                        isValid = true;

                }
                if (isValid == false)
                {
                    msg = "Invalid email address. Must contain letters. ";
                    return false;
                }
            }
            //check to not allow aa@bb. or aa@bb.123
            bool isDotCom = false;
            string sDotCom = "111";
            if (email.IndexOf(".") > -1) sDotCom = email.Substring(email.LastIndexOf(".") + 1);
            for (int i = 0; i < sDotCom.Length; i++)
            {
                char ch = sDotCom.Substring(i, 1).ToCharArray()[0];
                int ascVal = (int)ch;
                if ((ascVal >= 65 && ascVal <= 90)
                    || (ascVal >= 97 && ascVal <= 122))
                    isDotCom = true;

            }
            if (isDotCom == false)
            {
                msg = "Invalid email address. Must contain letters. ";
                return false;
            }


            return true;

        }
        public static bool IsValidPassword(string pword, ref string msg)
        {

            /************************************************************************/
            /* --Current password rule---
            * numbers and letters only,no space    
            * 
            * A-Z(65-90)
            * a-z(97-122)
            * 0-9(48-57)
            * space(32)                                                                 */
            /************************************************************************/


            bool isValid = true;
            if (pword.Length < 4)
            {
                msg = "Password must be at least 4 characters long.";
                isValid = false;
            }
            else if (pword.Length > 20)
            {
                msg = "Password must NOT be more than 20 characters long.";
                isValid = false;
            }
            else if (pword.IndexOf(" ") > -1)  //space not allowed
            {
                msg = "Password must not contain a space.";
                isValid = false;
            }
            else
            {
                string allowedSymbols = "! @ $ % * ( ) { } : . - _ = + | ~";
                string allowedSymbolsNoSpace = allowedSymbols.Replace(" ", "");
                for (int i = 0; i < pword.Length; i++)
                {
                    char ch = pword.Substring(i, 1).ToCharArray()[0];
                    string sChar = pword.Substring(i, 1);
                    int ascVal = (int)ch;
                    if (
                        (ascVal >= 65 && ascVal <= 90)
                        || (ascVal >= 97 && ascVal <= 122)
                        || (ascVal >= 48 && ascVal <= 57)
                        || allowedSymbolsNoSpace.IndexOf(sChar) > -1
                        ) { }
                    else
                    {
                        msg = "Password should a combination of letters and numbers or some symbols. <br />These symbols are allowed: <br />" + allowedSymbols;
                        isValid = false;
                    }
                }


            }

            return isValid;

        }
    }
    	

		
}