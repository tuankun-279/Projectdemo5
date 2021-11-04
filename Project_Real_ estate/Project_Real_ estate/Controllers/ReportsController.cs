using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Project_Real__estate.Models;

namespace Project_Real__estate.Controllers
{
    public class ReportsController : BaseController
    {
        private projectEntities1 db = new projectEntities1();

        // GET: Reports
        public ActionResult Index()
        {
            var reports = db.Reports.Include(r => r.Advertisement).Include(r => r.Agent).Include(r => r.Seller);
            return View(reports.ToList());
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            ViewBag.AdsId = new SelectList(db.Advertisements, "adsId", "Tiltle");
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName");
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportId,ReportDate,AdsId,SellerId,AgentId,Price")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdsId = new SelectList(db.Advertisements, "adsId", "Tiltle", report.AdsId);
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName", report.AgentId);
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name", report.SellerId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdsId = new SelectList(db.Advertisements, "adsId", "Tiltle", report.AdsId);
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName", report.AgentId);
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name", report.SellerId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportId,ReportDate,AdsId,SellerId,AgentId,Price")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdsId = new SelectList(db.Advertisements, "adsId", "Tiltle", report.AdsId);
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName", report.AgentId);
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name", report.SellerId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult SendEmail(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }
        [HttpPost]
        public ActionResult SendEmail(int id, string subject, string message)
        {
            try
            {
                string receiver = "";
                if (ModelState.IsValid)
                {
                    Report find = db.Reports.Find(id);
                    if (find.SellerId != null)
                    {
                        receiver = find.Seller.Email;
                    }
                    else if (find.AgentId != null)
                    {
                        receiver = find.Agent.Email;
                    }
                    var senderEmail = new MailAddress("nguyenhuytuan04@gmail.com", "Dream House Estate");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Tuankun.1301";
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
                        Body = "Cảnh báo bài viết \""+find.Advertisement.Tiltle+"\" của tài khoản "+receiver+" sắp hết hạn bài đăng. \nBạn có muốn gia hạn cho bài đăng không?\n"+body
                    })
                    {
                        smtp.Send(mess);
                        ViewBag.Mes = "Gửi thành công";
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Mes = "Không thể gửi ";
            }
            return View();
        }
    }
}
