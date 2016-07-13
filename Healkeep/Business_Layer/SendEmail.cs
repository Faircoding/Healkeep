using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Healkeep.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using SendGrid;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Healkeep.Business_Layer
{
    public class SendEmail
    {
        
            //// Create the email object first, then add the properties.
            //var myMessage = new SendGridMessage();
            //// Add the message properties.
            //myMessage.From = new MailAddress("support@healkley.com");
            //// Add multiple addresses to the To field.
            //List<String> recipients = new List<String>
            //    {
            //        @"Jairo Calderon <jairo.a.calderon@gmail.com>"
            //        //,@"Anna Lidman <anna@example.com>",
            //    };
            //myMessage.AddTo(recipients);
            //myMessage.Subject = "Nuevo tratamiento para aprovacion";
            ////Add the HTML and Text bodies
            //myMessage.Html = "<p>El tratamiento </p>" + naturalTreatment.Name + "<p> debe ser aprobado.</p>";
            //myMessage.Text = "El tratamiento " + naturalTreatment.Name + " debe ser aprobado.";

            //var credentials = new NetworkCredential(
            //           ConfigurationManager.AppSettings["mailAccount"],
            //           ConfigurationManager.AppSettings["mailPassword"]
            //           );

            //// Create a Web transport for sending email.
            //var transportWeb = new Web(credentials);
            //transportWeb.DeliverAsync(myMessage);
            //// Send the email.
    }
}