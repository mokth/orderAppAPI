using galaCoreAPI.Model;
using MRP;
using MRP.BL;
using PriceSetAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebAPI.Shared
{
    public class AuthHelper
    {
        public static bool CheckValidUser(UserInfo user)
        {
            // string hashmethod = "SHA1";
            string hashedPassword = HashString(user.password);// System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(user.password, hashmethod);
            return CheckLogin(user, hashedPassword);
        }

        public static bool CheckValidUserNormal(UserInfo user)
        {
            // string hashmethod = "SHA1";
            string hashedPassword = HashString(user.password);//System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(user.password, hashmethod);
            return CheckLoginNormal(user, hashedPassword);
        }

        private static bool CheckLogin(UserInfo user, string hashedPassword)
        {
            DataRow[] dr;

            DataTable dtUser = BaseADOPG.GetData("Select * from CustAcct where id = '" + user.name + "' AND PWord = '" + hashedPassword + "'  ");

            dr = dtUser.Select("id = '" + user.name + "'");

            if (dr.Length > 0)
            {
                user.fullname = dr[0]["name"].ToString().ToUpper();
                user.companyCode = dr[0]["CustomerCode"].ToString().ToUpper();                
            }

            return (dr.Length > 0);
            
        }

        private static bool CheckLoginNormal(UserInfo user, string hashedPassword)
        {
            DataRow[] dr;

            DataTable dtUser = BaseADOPG.GetData("Select * from CustAcct where id = '" + user.name + "' AND pword = '" + hashedPassword + "' ");
            //DataTable dtCust = BaseADOPG.GetData("Select CustCode,CustName from sySaCustAcc where CustCode = '" + user.name + "' AND Active = 1 ");
            Console.WriteLine("Select * from CustAcct where id = '" + user.name + "' AND pword = '" + hashedPassword + "' ");
            dr = dtUser.Select("id = '" + user.name + "'");
            if (dr.LongLength > 0)
            {
                user.fullname = dr[0]["id"].ToString().ToUpper();
                user.companyCode = dr[0]["CustomerCode"].ToString().ToUpper();
                bool chgpass = Convert2NumTool<bool>.ConvertVal(dr[0]["chgpass"]);
                if (chgpass)
                {
                    user.access = "chgpass";
                }


            }

            return (dr.Length > 0);


        }

        public static bool IsValidAccessRight(string userID, string screenID)
        {
            bool isValid = false;
            //DataTable dtUserRight = AdUserBL.GetUserRight(userID);
            //if (CUser.CheckUserRight2((int)CCommon.enGroupRight.Access, userID, dtUserRight, screenID))
            //{
            //    isValid = true;
            //}
            return isValid;
        }

        public static string HashString(string inputString)
        {
            string hashName = "SHA1";
            var algorithm = HashAlgorithm.Create(hashName);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name", hashName);

            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            return Convert.ToBase64String(hash);
        }
        // Verify a hash against a string.
        static bool VerifyHash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = HashString(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
