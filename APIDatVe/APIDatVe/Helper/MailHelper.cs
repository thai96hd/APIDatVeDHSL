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
        public string Email { get; set; } = "ngoxuanduong23@gmail.com";
        public string Password { get; set; } = "duongthanthanduong239608";
    }
    public class MailHelper
    {
        public static bool SendMailGuest(string sendTo, string subject, string body)
        {
            try
            {
                ConfigEmail configEmail = new ConfigEmail();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                // Mail send for Guest
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(configEmail.Email);
                mail.To.Add(sendTo);
                mail.Subject = subject;
                mail.Body = body;
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