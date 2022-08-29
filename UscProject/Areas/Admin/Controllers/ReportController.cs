using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Vml.Office;
using UscProject.Models;
using System.Net.Mail;
using UscProject.Areas.Admin.ViewModels;
using System.Net;
using System.Data.Entity;

namespace UscProject.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        // GET: Admin/Reports
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        public ActionResult Reports()
        {
            List<ViewModels.spineline.DataPoint> dataPoints = new List<ViewModels.spineline.DataPoint>();

            var karfarma = db.UserTB.Where(p => p.RoleID == 2).OrderBy(p => p.RegesterDate).ToList();

            var mindate = karfarma.First().RegesterDate.Year;

            var maxdate = karfarma.OrderByDescending(p => p.RegesterDate).First().RegesterDate.Year;

            for (int i = mindate; i <= maxdate; i++)
            {
                var count = karfarma.Where(p => p.RegesterDate.Year == i).Count();
                dataPoints.Add(new ViewModels.spineline.DataPoint(Convert.ToDouble(i), count));

            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            //****************************************************************************//

            List<ViewModels.spineline.DataPoint> dataPoints2 = new List<ViewModels.spineline.DataPoint>();

            var karjo = db.UserTB.Where(p => p.RoleID == 3).OrderBy(p => p.RegesterDate).ToList();

            var mindate2 = karjo.First().RegesterDate.Year;

            var maxdate2 = karjo.OrderByDescending(p => p.RegesterDate).First().RegesterDate.Year;

            for (int i = mindate2; i <= maxdate2; i++)
            {
                var count = karjo.Where(p => p.RegesterDate.Year == i).Count();
                dataPoints2.Add(new ViewModels.spineline.DataPoint(Convert.ToDouble(i), count));

            }
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            //****************************************************************************//

            List<DataPoint> dataPoints3 = new List<DataPoint>();
            var forms = db.FormTB.OrderBy(p => p.RequestDtae).ToList();
            var mindate3 = forms.First().RequestDtae.Year;
            var maxdate3 = forms.OrderByDescending(p => p.RequestDtae).First().RequestDtae.Year;
            for (int i = mindate3; i <= maxdate3; i++)
            {
                var count = forms.Where(p => p.RequestDtae.Year == i).Count();
                dataPoints3.Add(new DataPoint(i.ToString(), count));
            }

            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);
            ////****************************************************************************//
            List<ContactTB> contact = new List<ContactTB>();
            contact = db.ContactTB.ToList();
            List<CommentTB> Comment = new List<CommentTB>();
            Comment = db.CommentTB.Where(o => o.CommentResponse == null).ToList();
            contact_comment_vm model = new contact_comment_vm();
            model.comment = Comment;
            model.contact = contact;
            return View(model);
        }

        [HttpPost]
        public ActionResult SendEmail(int contactid, string receiver, string subject, string message)
        {

            if (ModelState.IsValid)
            {
                var senderEmail = new MailAddress("soroushseifpour.usc@gmail.com", "soroush");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = "sor891sor891";
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

                var comment = db.CommentTB.Where(o => o.CommentID == contactid).First();
                comment.CommentResponse = message;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("Reports");
            }
            return Redirect("Reports");
        }
        public void DeleteComment(int id)
        {
            var j = db.CommentTB.Find(id);
            db.Entry(j).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}