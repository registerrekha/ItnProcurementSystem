using System;
using System.Collections.Generic;
using Itn.Utilities;

namespace Itn.Shared.Notification
{
    public class DiEmailMessage
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Recipients { get; set; }
       
      
        /// <summary>
        /// Create an Email Message
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="recipients"></param>
        /// <returns></returns>
        public static DiEmailMessage Create(string subject, string body, string recipients)
        {
            return new DiEmailMessage { From = AppConfig.GetConfigVal("Email.From"), Subject = subject, Body = body, Recipients = recipients.SplitToList(';') };
        }

        public static DiEmailMessage Create(string subject, string body, List<string> recipients, string acctKey = "")
        {
            return new DiEmailMessage { From = AppConfig.GetConfigVal("Email.From"), Subject = subject, Body = body, Recipients = recipients };
        }

        public static DiEmailMessage Create()
        {
            return new DiEmailMessage()
            {
                Recipients = new List<string>()
            };
        }

        public static DiEmailMessage Create(string from, string subject, string body, string recipients, string acctKey = "")
        {
            return new DiEmailMessage { From = from, Subject = subject, Body = body, Recipients = recipients.SplitToList(';')};
        }

     


        public static DiEmailMessage Create(string from, string subject, string body, string recipient)
        {
            return new DiEmailMessage
            {
                From = from,
                Subject = subject,
                Body = body,
                Recipients = new List<string> { recipient }
            };
        }



        public string To()
        {
            return Recipients.ToConcatString(";");
        }

        public override string ToString()
        {
            return string.Format("{0} To:{1} Msg:{2}", Subject, To(), Body.Left(20));
        }

    }
}
