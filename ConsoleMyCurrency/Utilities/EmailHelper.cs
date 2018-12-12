using ConsoleMyCurrency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMyCurrency.Helper
{
    class EmailHelper
    {
        public static List<ConsoleAlert> SendEmails(IEnumerable<ConsoleAlert> alerts, double value)
        {
            string Subject = null;
            string Message = null;
            string Receiver = null;
            List<ConsoleAlert> EmailDenied = new List<ConsoleAlert>();
            foreach (ConsoleAlert alert in alerts)
            {
                Subject = "MyCurrency Alert for " + alert.CurrencyName + " Price";
                Message =
                    "This is an authomatic message, please do not reply it. \n\n" +
                    "You are receiving this message according you set up for best value for buying the currency bellow : \n" +
                    "Currency : " + alert.CurrencyName +
                    "\nBest value defined :" + Math.Round(alert.BestValue, 5) +
                    "\nPosition :" + Math.Round(value, 5) +
                    "\nVariation of CAD$" + Math.Round(alert.BestValue - value, 5);
                Receiver = alert.Email;

                if (!isEmailSent(Receiver, Subject, Message))
                {
                    EmailDenied.Add(alert);
                }
            }
            return EmailDenied;
        }
        private static bool isEmailSent(string receiver, string subject, string message)
        {
            try
            {
                var senderEmail = new MailAddress("mycurrencyproject@gmail.com", "My Currency Project");
                var receiverEmail = new MailAddress(receiver, "Dear User");
                var password = "mycurrencypassword";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}