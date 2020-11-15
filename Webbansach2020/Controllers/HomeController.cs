using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach2020.Models;

namespace Webbansach2020.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string searchString)
        {
            var Loc = from p in db.sanPhams
                      select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                Loc = Loc.Where(s => s.TenSP.Contains(searchString));
            }
            return View(Loc);
        }
        //public static string getString(string s)
        //{
        //    if (s.Length > 10)
        //    {
        //        return s.Substring(0, 20) + "...";
        //    }
        //    else
        //        return s;
        //}
        public ActionResult AddToCart(int? id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = db.sanPhams.Find(id);
                cart.Add(new Item()
                {
                    SanPham = product,
                    SoLuong = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.sanPhams.Find(id);
                foreach(var item in cart)
                {
                    if (Session["cart"] == null)
                    {
                        cart.Add(new Item()
                        {
                            SanPham = product,
                            SoLuong = 1
                        });
                    }
                    else
                    {
                        int next = item.SoLuong;
                        if(next > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                SanPham = product,
                                SoLuong = next + 1
                            });
                        }
                        break;
                    }
                    Session["cart"] = cart;
                }    
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult DecreaseQ(int? id)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.sanPhams.Find(id);
                foreach (var item in cart)
                {
                    if (item.SanPham.ID == id)
                    {
                        int PrevQ = item.SoLuong;
                        if (PrevQ > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                SanPham = product,
                                SoLuong = PrevQ - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

    }
}