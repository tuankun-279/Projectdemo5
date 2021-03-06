using PagedList;
using Project_Real__estate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;


namespace Project_Real__estate.Controllers
{
    public class HomeController : Controller
    {
        private projectEntities1 db = new projectEntities1();
   
        public ActionResult Index()
        {
            ViewBag.Agent = db.Agents.Where(a=>a.isActivate==true).ToList();

            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");

            ViewBag.Area = new List<SelectListItem>() {
                new SelectListItem(){Text="0-100",Value = "0-100"},
                new SelectListItem(){Text="100-300",Value = "100-300"},
                new SelectListItem(){Text="300-600",Value = "300-600"},
                new SelectListItem(){Text="600-900",Value = "600-900"},
                new SelectListItem(){Text="900-1200",Value = "900-1200"},
                new SelectListItem(){Text="1200-1500",Value = "1200-1500"}
            };

            var cityList = new List<string>();

            var citySelect = db.Advertisements.Select(s => s.Cityprovince);

            cityList.AddRange(citySelect.Distinct());
            ViewBag.City = new SelectList(cityList);

            ViewBag.Bedroom = new List<SelectListItem>() {
                new SelectListItem() { Text="1",Value="1"},
                new SelectListItem() { Text="2",Value="2"},
                new SelectListItem() { Text="3",Value="3"},
                new SelectListItem() { Text="4",Value="4"},
                new SelectListItem() { Text="5",Value="5"}
            };
            ViewBag.Bath = new List<SelectListItem>() {
                new SelectListItem() { Text="1",Value="1"},
                new SelectListItem() { Text="2",Value="2"},
                new SelectListItem() { Text="3",Value="3"},
                new SelectListItem() { Text="4",Value="4"},
                new SelectListItem() { Text="5",Value="5"}
            };
            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User).Where(a => a.isActivate == true);
            return View(advertisements.ToList());
        }
        public ActionResult IndexRentSearch(string bath, string bedroom, string area, string city, int? category)
        {
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");

            ViewBag.Area = new List<SelectListItem>() {
                new SelectListItem(){Text="0-100",Value = "0-100"},
                new SelectListItem(){Text="100-300",Value = "100-300"},
                new SelectListItem(){Text="300-600",Value = "300-600"},
                new SelectListItem(){Text="600-900",Value = "600-900"},
                new SelectListItem(){Text="900-1200",Value = "900-1200"},
                new SelectListItem(){Text="1200-1500",Value = "1200-1500"}
            };
            var cityList = new List<string>();

            var citySelect = db.Advertisements.Select(s => s.Cityprovince);

            cityList.AddRange(citySelect.Distinct());
            ViewBag.City = new SelectList(cityList);

            ViewBag.Bedroom = new List<SelectListItem>() {
                new SelectListItem() { Text="1",Value="1"},
                new SelectListItem() { Text="2",Value="2"},
                new SelectListItem() { Text="3",Value="3"},
                new SelectListItem() { Text="4",Value="4"},
                new SelectListItem() { Text="5",Value="5"}
            };
            ViewBag.Bath = new List<SelectListItem>() {
                new SelectListItem() { Text="1",Value="1"},
                new SelectListItem() { Text="2",Value="2"},
                new SelectListItem() { Text="3",Value="3"},
                new SelectListItem() { Text="4",Value="4"},
                new SelectListItem() { Text="5",Value="5"}
            };
            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User).Where(a => a.isActivate == true && a.StatusHouse.Equals("Rent"));

            //type
            if (category != null)
                advertisements = advertisements.Where(s => s.CategoryId == category);
            // area
            switch (area)
            {
                case "0-100":
                    advertisements = advertisements.Where(s => s.EstatePrice > 0 && s.EstatePrice < 100);
                    break;
                case "100-300":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 100 && s.EstatePrice < 300);
                    break;
                case "300-600":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 300 && s.EstatePrice < 600);
                    break;
                case "600-900":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 600 && s.EstatePrice < 900);
                    break;
                case "900-1200":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 900 && s.EstatePrice < 1200);
                    break;
                case "1200-1500":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 1200 && s.EstatePrice < 1500);
                    break;
            }

            //city
            if (!String.IsNullOrEmpty(city))
                advertisements = advertisements.Where(s => s.Cityprovince.Contains(city));
            //bedrom
            if (!String.IsNullOrEmpty(bedroom))
                advertisements = advertisements.Where(s => s.Bedrooms.ToString().Equals(bedroom));
            // bath
            if (!String.IsNullOrEmpty(bath))
                advertisements = advertisements.Where(s => s.Toilets.ToString().Equals(bath));

            return RedirectToAction("ListPost", advertisements.ToList());

        }
        public ActionResult IndexBuySearch(string bath, string bedroom, string area, string city, int? category)
        {
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");

            ViewBag.Area = new List<SelectListItem>() {
                new SelectListItem(){Text="0-100",Value = "0-100"},
                new SelectListItem(){Text="100-300",Value = "100-300"},
                new SelectListItem(){Text="300-600",Value = "300-600"},
                new SelectListItem(){Text="600-900",Value = "600-900"},
                new SelectListItem(){Text="900-1200",Value = "900-1200"},
                new SelectListItem(){Text="1200-1500",Value = "1200-1500"}
            };

            var cityList = new List<string>();

            var citySelect = db.Advertisements.Select(s => s.Cityprovince);

            cityList.AddRange(citySelect.Distinct());
            ViewBag.City = new SelectList(cityList);

            ViewBag.Bedroom = new List<SelectListItem>() {
                new SelectListItem() { Text="1",Value="1"},
                new SelectListItem() { Text="2",Value="2"},
                new SelectListItem() { Text="3",Value="3"},
                new SelectListItem() { Text="4",Value="4"},
                new SelectListItem() { Text="5",Value="5"}
            };
            ViewBag.Bath = new List<SelectListItem>() {
                new SelectListItem() { Text="1",Value="1"},
                new SelectListItem() { Text="2",Value="2"},
                new SelectListItem() { Text="3",Value="3"},
                new SelectListItem() { Text="4",Value="4"},
                new SelectListItem() { Text="5",Value="5"}
            };

            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User).Where(a => a.isActivate == true && a.StatusHouse.Equals("Sale"));
            //status

            //type
            if (category != null)
                advertisements = advertisements.Where(s => s.CategoryId == category);
            // area
            switch (area)
            {
                case "0-100":
                    advertisements = advertisements.Where(s => s.EstatePrice > 0 && s.EstatePrice < 100);
                    break;
                case "100-300":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 100 && s.EstatePrice < 300);
                    break;
                case "300-600":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 300 && s.EstatePrice < 600);
                    break;
                case "600-900":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 600 && s.EstatePrice < 900);
                    break;
                case "900-1200":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 900 && s.EstatePrice < 1200);
                    break;
                case "1200-1500":
                    advertisements = advertisements.Where(s => s.EstatePrice >= 1200 && s.EstatePrice < 1500);
                    break;
            }

            //city
            if (!String.IsNullOrEmpty(city))
                advertisements = advertisements.Where(s => s.Cityprovince.Contains(city));
            //bedrom
            if (!String.IsNullOrEmpty(bedroom))
                advertisements = advertisements.Where(s => s.Bedrooms.ToString().Equals(bedroom));
            // bath
            if (!String.IsNullOrEmpty(bath))
                advertisements = advertisements.Where(s => s.Toilets.ToString().Equals(bath));

            return RedirectToAction("ListPost", advertisements.AsEnumerable());

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
        public ActionResult NewPost()
        {
            var agentId = Session["AgentId"];
            var sellerId = Session["SellerId"];
            if (agentId != null || sellerId != null)
            {
                ViewBag.AgentId = Session["AgentId"];
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
                ViewBag.AgentId = Session["SellerId"];
             
                
            
            }
            else
            {
                return RedirectToAction("Login", "RegisterLoginView");
            }
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost([Bind(Include = "adsId,Tiltle,ReleaseDate,ExpirationDate,SellerId,AgentId,PaymentId,CategoryId,Describe,CurrentSymbol,priceOfAds,EstatePrice,Facade,Gateway,floors,Bedrooms,Toilets,furniture,Area,Cityprovince,District,Ward,Street,isActivate,UserId,StatusHouse")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                List<Image> imglist = new List<Image>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var random = Guid.NewGuid() + fileName;
                        var ext = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                        if (ext.ToLower() == "jpeg" || ext.ToLower() == "jpg" || ext.ToLower() == "png")
                        {
                            Image img = new Image()
                            {
                                FileName = random,
                                Extension = Path.GetExtension(fileName)
                            };
                            imglist.Add(img);

                            var path = Path.Combine(Server.MapPath("~/Images/"), img.FileName);
                            file.SaveAs(path);


                            WebImage imgSize = new WebImage(file.InputStream);
                            if (imgSize.Width > 400)
                                imgSize.Resize(600, 400);
                            imgSize.Save(path);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "File Extension Is InValid - Only Upload JPG/JPEG/PNG File";
                            return View();
                        }
                    }
                }
                if (Session["AgentId"] != null)
                {
                    advertisement.AgentId = Convert.ToInt32(Session["AgentId"]);
                    advertisement.SellerId = null;
                }
                else if (Session["SellerId"] != null)
                {
                    advertisement.SellerId = Convert.ToInt32(Session["SellerId"]);
                    advertisement.AgentId = null;
                }

                advertisement.Reports = new List<Report>() {
                    new Report() { ReportDate= DateTime.Now, AdsId = advertisement.adsId, AgentId = advertisement.AgentId, SellerId = advertisement.SellerId, Price = advertisement.priceOfAds } };

                advertisement.Images = imglist;
                advertisement.isActivate = false;
                db.Advertisements.Add(advertisement);
                int a = 0;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName", advertisement.AgentId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", advertisement.CategoryId);
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", advertisement.CategoryId);
            return View(advertisement);
        }


        public ActionResult ListPost(string status, string bath, string bedroom, string area, string city, int? category, string sortOrder, string currentFilter, string searchString, int? page)
        {
            //ViewBag.CurrentSort = sortOrder;
            ViewBag.LowSortParm = String.IsNullOrEmpty(sortOrder) ? "Price" : "";
            ViewBag.HighSortParm = String.IsNullOrEmpty(sortOrder) ? "Price_desc" : "";
            ViewBag.SellSortParm = String.IsNullOrEmpty(sortOrder) ? "Sell" : "";
            ViewBag.RentSortParm = String.IsNullOrEmpty(sortOrder) ? "Rent" : "";

            ViewBag.Status = new List<SelectListItem>() {
                new SelectListItem(){Text="For Sale",Value = "Sale" },
                new SelectListItem(){Text="For Rent",Value = "Rent" },
                new SelectListItem(){Text="Project",Value = "Project" }
            };

            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");

            ViewBag.Area = new List<SelectListItem>() {
                new SelectListItem(){Text="0-100",Value = "0-100"},
                new SelectListItem(){Text="100-300",Value = "100-300"},
                new SelectListItem(){Text="300-600",Value = "300-600"},
                new SelectListItem(){Text="600-900",Value = "600-900"},
                new SelectListItem(){Text="900-1200",Value = "900-1200"},
                new SelectListItem(){Text="1200-1500",Value = "1200-1500"}
            };

            var cityList = new List<string>();

            var citySelect = db.Advertisements.Select(s => s.Cityprovince);

            cityList.AddRange(citySelect.Distinct());
            ViewBag.City = new SelectList(cityList);

            ViewBag.Bedroom = new List<SelectListItem>() {
                new SelectListItem() { Text="1",Value="1"},
                new SelectListItem() { Text="2",Value="2"},
                new SelectListItem() { Text="3",Value="3"},
                new SelectListItem() { Text="4",Value="4"},
                new SelectListItem() { Text="5",Value="5"}
            };
            ViewBag.Bath = new List<SelectListItem>() {
                new SelectListItem() { Text="1",Value="1"},
                new SelectListItem() { Text="2",Value="2"},
                new SelectListItem() { Text="3",Value="3"},
                new SelectListItem() { Text="4",Value="4"},
                new SelectListItem() { Text="5",Value="5"}
            };
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User).Where(a => a.isActivate == true);
            //status
            if (!String.IsNullOrEmpty(status))
                advertisements = advertisements.Where(s => s.StatusHouse.Contains(status));
            //type
            if (category != null)
                advertisements = advertisements.Where(s => s.CategoryId == category);
            // area
            switch (area)
            {
                case "0-100":
                    advertisements = advertisements.Where(s => s.Area > 0 && s.Area < 100);
                    break;
                case "100-300":
                    advertisements = advertisements.Where(s => s.Area >= 100 && s.Area < 300);
                    break;
                case "300-600":
                    advertisements = advertisements.Where(s => s.Area >= 300 && s.Area < 600);
                    break;
                case "600-900":
                    advertisements = advertisements.Where(s => s.Area >= 600 && s.Area < 900);
                    break;
                case "900-1200":
                    advertisements = advertisements.Where(s => s.Area >= 900 && s.Area < 1200);
                    break;
                case "1200-1500":
                    advertisements = advertisements.Where(s => s.Area >= 1200 && s.Area < 1500);
                    break;
            }
            int i = 0;
            //city
            if (!String.IsNullOrEmpty(city))
                advertisements = advertisements.Where(s => s.Cityprovince.Equals(city));
            //bedrom
            if (!String.IsNullOrEmpty(bedroom))
                advertisements = advertisements.Where(s => s.Bedrooms.ToString().Equals(bedroom));
            // bath
            if (!String.IsNullOrEmpty(bath))
                advertisements = advertisements.Where(s => s.Toilets.ToString().Equals(bath));

            switch (sortOrder)
            {
                case "Price":
                    advertisements = advertisements.OrderBy(s => s.EstatePrice);
                    break;
                case "Price_desc":
                    advertisements = advertisements.OrderByDescending(s => s.EstatePrice);
                    break;
                case "Sell":
                    advertisements = advertisements.Where(s => s.StatusHouse.Equals("Sale")).OrderBy(s => s.Tiltle);
                    break;
                case "Rent":
                    advertisements = advertisements.Where(s => s.StatusHouse.Equals("Rent")).OrderBy(s => s.Tiltle);
                    break;
                default:  // Name ascending 
                    advertisements = advertisements.OrderBy(s => s.Tiltle);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.CateCount = db.Advertisements.Include(c => c.Category).GroupBy(a => a.CategoryId).Count();

            return View(advertisements.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);

            if (advertisement == null)
            {
                return HttpNotFound();
            }
            var images = new List<Image>();
            images = db.Images.Where(x => x.AdsId == id).ToList();
            ViewBag.file = images;
            return View(advertisement);
        }
        public ActionResult AgencyList(string Search)
        {
            var agent = db.Agents.Include(a => a.Payment).Include(a => a.User).Where(a => a.isActivate == true);
            if (!string.IsNullOrEmpty(Search))
            {
                agent = agent.Where(a => a.AgentName.Contains(Search));
            }
            return View(agent.ToList());
        }
        public ActionResult SellerList(string Search)
        {
            var seller = db.Sellers.Include(a => a.User).Where(a => a.isActivate == true);
            if (!string.IsNullOrEmpty(Search))
            {
                seller = seller.Where(a => a.Name.Contains(Search));

            }
            return View(seller.ToList());
        }
        public ActionResult AgentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            var ads = new List<Advertisement>();
            ads = db.Advertisements.Where(a => a.AgentId == id && a.isActivate == true).ToList();
            ViewBag.AdsList = ads;             
            return View(agent);
        }
        public ActionResult SellerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }
        public ActionResult AgentEdit(int? id)
        {
            if (Session["AgentId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Agent agent = db.Agents.Find(id);
                if (agent == null)
                {
                    return HttpNotFound();
                }
                ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
                return View(agent);
            }
            else
            {
                return RedirectToAction("Login", "RegisterLoginView");
            }

        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgentEdit(Agent agent)
        {
            if (ModelState.IsValid)
            {
                var data = db.Agents.Find(agent.AgentId);
                agent.Password = GetMD5(agent.Password);
                agent.ConfirmPassword = agent.Password;
                agent.isActivate = data.isActivate;
                agent.UserId = data.UserId;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(agent);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
            return View(agent);
        }
        public ActionResult SellerEdit(int? id)
        {
            if (Session["SellerId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Seller seller = db.Sellers.Find(id);
                if (seller == null)
                {
                    return HttpNotFound();
                }
                return View(seller);
            }
            else
            {
                return RedirectToAction("Login", "RegisterLoginView");
            }

        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SellerEdit(Seller seller)
        {
            if (ModelState.IsValid)
            {
                var data = db.Sellers.Find(seller.SellerId);
                seller.Password = GetMD5(seller.Password);
                seller.ConfirmPassword = seller.Password;
                seller.isActivate = data.isActivate;
                seller.UserId = data.UserId;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(seller);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(seller);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
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

        public ActionResult AdvertisementEdit(int? id)
        {
            if (Session["AgentId"] != null || Session["SellerId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Advertisement ad = db.Advertisements.Find(id);

                if (ad == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AgentId = Session["AgentId"];
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
                ViewBag.AgentId = Session["SellerId"];
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
                ViewBag.Type = new List<SelectListItem>()
            {
                new SelectListItem() { Value="Bán", Text= "Bán" },
                new SelectListItem() { Value="Thuê", Text= "Thuê" },
                new SelectListItem() { Value="Dự Án", Text= "Dự Án" }
             };
                return View(ad);
            }
            else
            {
                return RedirectToAction("Login", "RegisterLoginView");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdvertisementEdit(Advertisement advertisement)
        {

            if (ModelState.IsValid)
            {

                var data = db.Advertisements.Find(advertisement.adsId);
                List<Image> imgs = db.Images.Where(r => r.AdsId == data.adsId).ToList();
                foreach (var item in imgs)
                {
                    db.Images.Remove(item);
                }

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var random = Guid.NewGuid() + fileName;
                        var ext = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                        if (ext.ToLower() == "jpeg" || ext.ToLower() == "jpg" || ext.ToLower() == "png")
                        {
                            Image img = new Image()
                            {

                                FileName = random,
                                Extension = Path.GetExtension(fileName)
                            };

                            data.Images.Add(img);
                            img.AdsId = advertisement.adsId;
                            var path = Path.Combine(Server.MapPath("~/Images/"), img.FileName);
                            file.SaveAs(path);


                            WebImage imgSize = new WebImage(file.InputStream);
                            if (imgSize.Width > 200)
                                imgSize.Resize(200, 200);
                            imgSize.Save(path);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "File Extension Is InValid - Only Upload JPG/JPEG/PNG File";
                            return View();
                        }
                    }
                }
                advertisement.isActivate = data.isActivate;
                advertisement.UserId = data.UserId;
                if (Session["AgentId"] != null)
                {
                    advertisement.AgentId = Convert.ToInt32(Session["AgentId"]);
                    advertisement.SellerId = null;
                }
                else if (Session["SellerId"] != null)
                {
                    advertisement.SellerId = Convert.ToInt32(Session["SellerId"]);
                    advertisement.AgentId = null;
                }

                advertisement.Reports = new List<Report>() {
                    new Report() { ReportDate= DateTime.Now, AdsId = advertisement.adsId, AgentId = advertisement.AgentId, SellerId = advertisement.SellerId, Price = advertisement.priceOfAds } };

                advertisement.Images = data.Images;

                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(advertisement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.Type = new List<SelectListItem>()
            {
                new SelectListItem() { Value="Bán", Text= "Bán" },
                new SelectListItem() { Value="Thuê", Text= "Thuê" },
                new SelectListItem() { Value="Dự Án", Text= "Dự Án" }
             };
            return View(advertisement);
        }
        [HttpPost]
        public ActionResult AdvertisementDelete(Advertisement advertisement)
        {
            int id = Convert.ToInt32(Request.Form["id"]);
            if (advertisement != null)
            {
                if (Session["AgentId"] != null)
                {
                    Advertisement ads = db.Advertisements.FirstOrDefault(t => t.adsId == id);
                    foreach (var item in ads.Reports.ToList())
                    {
                        db.Reports.Remove(item);
                    }
                    foreach (var image in ads.Images.ToList())
                    {
                        db.Images.Remove(image);
                    }
                    db.Advertisements.Remove(ads);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                if (Session["SellerId"] != null)
                {
                    Advertisement ads = db.Advertisements.FirstOrDefault(t => t.adsId == id);
                    foreach (var item in ads.Reports.ToList())
                    {
                        db.Reports.Remove(item);
                    }
                    foreach (var image in ads.Images.ToList())
                    {
                        db.Images.Remove(image);
                    }
                    db.Advertisements.Remove(ads);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(advertisement);
        }
    }
}