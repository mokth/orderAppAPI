using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MRP.BL;
using POS.DATA;
using PriceSetAPI.DataBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Security;

namespace ERPWebAPI.Model
{
    public class AuthenticationHelper
    {
       SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dtAdUser;
        string _errmgs;
        string hashmethod = "SHA1";

        public string Errmgs { get => _errmgs; set => _errmgs = value; }

        //public bool ChangedPassowrd(string id,string newpass,string oldpass)
        //{
        //    _errmgs = "";
        //    bool success = false;
        //    string sql = string.Format(@"select * from aduser where id='{0}'",id);
        //    if (!CSys.OpenCon(ref con))
        //    {
        //        _errmgs = "System  error, try again later...";
        //        return false;
        //    }
        //    dtAdUser = BaseADOPG.GetData(sql);
        //    if (dtAdUser.Rows.Count == 0)
        //    {
        //        _errmgs = "Invalid user ID.";
        //        return false;
        //    }
        //    DataRow drAdUser = dtAdUser.Rows[0];
        //    string hashedPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(oldpass, hashmethod);
        //    if (drAdUser["PWord"].ToString() != hashedPassword)
        //    {
        //        _errmgs = "Invalid Old Password.";
        //        return false;
        //    }

        //    hashedPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(newpass, hashmethod);
        //    drAdUser["PWord"] = hashedPassword;
                     

        //    SqlTransaction sqlTrans;
        //    sqlTrans = con.BeginTransaction();

        //    CAdapter.GenerateAdUserCommand(ref da);
        //    if (UpdateTable(ref dtAdUser,sqlTrans))
        //    {
        //        sqlTrans.Commit();
        //        success = true;
        //    }
        //    else
        //        sqlTrans.Rollback();

        //    con.Close();

        //    return success;

        //}

        //public bool ResetPassowrd(string id, IConfigurationRoot configuration, string webRootPath)
        //{
        //    _errmgs = "";
        //    bool success=false;
            

        //    string sql = string.Format(@"select * from aduser where id='{0}'",id);
        //    if (!CSys.OpenCon(ref con))
        //    {
        //        _errmgs = "System  error, try again later...";
        //        return false;
        //    }
        //    dtAdUser = BaseADOPG.GetData(sql);
        //    if (dtAdUser.Rows.Count == 0)
        //    {
        //        _errmgs = "Invalid email or user ID.";
        //        return false;
        //    }
            
        //    DataRow drAdUser = dtAdUser.Rows[0];

        //    string password = Membership.GeneratePassword(6, 2);
        //    string hashedPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, hashmethod);
        //    drAdUser["PWord"] = hashedPassword;
                     

        //    SqlTransaction sqlTrans;
        //    sqlTrans = con.BeginTransaction();

        //    CAdapter.GenerateAdUserCommand(ref da);
        //    if (UpdateTable(ref dtAdUser, sqlTrans))
        //    {
        //        sqlTrans.Commit();
        //        success = SendTempPassEmail(email, password, webRootPath);
                
        //    }
        //    else
        //        sqlTrans.Rollback();

        //    con.Close();

        //    return success;
        //}

       

        //private bool UpdateTable(ref DataTable tablename, SqlTransaction sqlTrans)
        //{

        //    da.UpdateCommand.Connection = con;
        //    da.InsertCommand.Connection = con;
        //    da.DeleteCommand.Connection = con;

        //    da.InsertCommand.Transaction = sqlTrans;
        //    da.UpdateCommand.Transaction = sqlTrans;
        //    da.DeleteCommand.Transaction = sqlTrans;

        //    try
        //    {

        //        da.Update(tablename);
        //        da.UpdateCommand.Parameters.Clear();

        //    }
        //    catch (Exception ex)
        //    {
        //        Errmgs = "Error saving data, please contact your system admistrator!" + ex.Message;
        //        return false;

        //    }

        //    return true;
        //}

        //protected bool SendTempPassEmail(string toEmail,string password,string webRootPath)
        //{
        //    SmtpClient objSmtpMail = new SmtpClient();
        //    bool success = false;
        //    try
        //    {
        //        string senderEmail = "";// ConfigurationManager.AppSettings["SMTP/Sender"];
        //        MailAddress Sender = new MailAddress(senderEmail);
        //        MailAddress From = new MailAddress(senderEmail);
        //        MailMessage objPOMailMessage = new MailMessage();
        //        objPOMailMessage.Sender = Sender;
        //        objPOMailMessage.From = From;
        //        objPOMailMessage.To.Add(toEmail);

        //        string body = System.IO.File.ReadAllText(Path.Combine(webRootPath, "resetpass.html"));
        //        body = body.Replace("@pass", password);
        //        //Attachment attMailAttachment = new Attachment(hdAttachmentPath.Value); 
        //        objPOMailMessage.Subject = "Your temporary password reseted.";
        //        objPOMailMessage.IsBodyHtml = true;
        //        objPOMailMessage.Body = body;
        //        objSmtpMail.Send(objPOMailMessage);
        //        success = true;

        //    }
        //    catch (Exception err)
        //    {
        //        Errmgs = "error sending email. "+err.Message;
        //    }

        //    return success;
        //}
    }
}
