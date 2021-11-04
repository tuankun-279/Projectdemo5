using PagedList;
using Project_Real__estate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace Project_Real__estate.Controllers
{
    public class AdminController : Controller
    {
        private projectEntities1 db = new projectEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {               
                ViewBag.AdsCount = db.Advertisements.Count();
                ViewBag.AgentCount = db.Agents.Count();
                ViewBag.SellerCount = db.Sellers.Count();
                ViewBag.ReportCount = db.Reports.Count();
                //Activate
                ViewBag.AdsActivateCount = db.Advertisements.Where(a=>a.isActivate == true).Count();
                ViewBag.AgentActivateCount = db.Agents.Where(a => a.isActivate == true).Count();
                ViewBag.SellerActivateCount = db.Sellers.Where(a => a.isActivate == true).Count();
                //not activate
                ViewBag.AdsNotActivateCount = db.Advertisements.Where(a => a.isActivate == false).Count();
                ViewBag.AgentNotActivateCount = db.Agents.Where(a => a.isActivate == false).Count();
                ViewBag.SellerNotActivateCount = db.Sellers.Where(a => a.isActivate == false).Count();


                return View();
                
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.Users.Where(s => s.UserName.Equals(username) && s.Password.Equals(f_password)).ToList();

                if (data.Count() > 0)
                {
                    Session["UserId"] = data.FirstOrDefault().UserId;
                    Session["UserName"] = data.FirstOrDefault().UserName;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Message = "Wrong username or password";
                }
            }
            return View();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        

    }
}