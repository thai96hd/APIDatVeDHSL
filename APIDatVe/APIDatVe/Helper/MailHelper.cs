using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace APIDatVe.Helper
{
    public class ConfigEmail
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class MailHelper
    {
        public static bool SendMailGuest(ConfigEmail configEmail, string sendTo, string subject, string body, List<string> cc = null, List<string> bcc = null)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                // Mail send for Guest
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(configEmail.Email);
                mail.To.Add(sendTo);
                mail.Subject = subject;
                mail.Body = body;
                if (cc != null && cc.Count > 0)
                {
                    cc.ForEach(x =>
                    {
                        if (x != "")
                            mail.CC.Add(x);
                    });
                }
                if (bcc != null && bcc.Count > 0)
                {
                    bcc.ForEach(x =>
                    {
                        if (x != "")
                            mail.Bcc.Add(x);
                    });
                }
                // Send
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(configEmail.Email, configEmail.Password);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (SmtpException e)
            {
                string s = e.Message;
                return false;
            }
        }
    }
}