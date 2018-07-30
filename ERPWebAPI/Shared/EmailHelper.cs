using galaEatAPI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace galaCoreAPI.Shared
{
    public class EmailHelper
    {
        public static bool SendTempPassEmail(string toEmail, string password, ICommonService conserv, string webRootPath,ref string msg)
        {
            SmtpClient objSmtpMail = new SmtpClient();
            bool success = false;
            try
            {
                IConfiguration configuration = conserv.GetConfiguration();
                string senderEmail = configuration["smtp:sender"];
                MailAddress Sender = new MailAddress(senderEmail);
                MailAddress From = new MailAddress(senderEmail);
                MailMessage objPOMailMessage = new MailMessage();
                objPOMailMessage.Sender = Sender;
                objPOMailMessage.From = From;
                objPOMailMessage.To.Add(toEmail);

                string body = System.IO.File.ReadAllText(Path.Combine(webRootPath, "resetpass.html"));
                body = body.Replace("@pass", password);
                //Attachment attMailAttachment = new Attachment(hdAttachmentPath.Value); 
                objPOMailMessage.Subject = "Your temporary password reseted.";
                objPOMailMessage.IsBodyHtml = true;
                objPOMailMessage.Body = body;
                objSmtpMail.Host = configuration["smtp:host"];
                objSmtpMail.Port = Convert.ToInt32(configuration["smtp:port"]);
                objSmtpMail.Credentials = new System.Net.NetworkCredential(configuration["smtp:sender"], configuration["smtp:password"]);
                objSmtpMail.Send(objPOMailMessage);
                success = true;

            }
            catch (Exception err)
            {
                msg = "error sending email. " + err.Message;
            }

            return success;
        }
    }
}
