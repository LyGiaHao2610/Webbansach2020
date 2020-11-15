using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webbansach2020.Models;

namespace Webbansach2020.Controllers
{
    public class SanPhamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SanPhams
        public ActionResult Index()
        {
            return View(db.sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTG = new SelectList(db.tacGias, "ID", "TenTacGia", sanPham.MaTG);
            ViewBag.MaNXB = new SelectList(db.nXBs, "ID", "TenNXB", sanPham.MaNXB);
            ViewBag.MaLoai = new SelectList(db.theloais, "ID", "TenTheLoai", sanPham.MaLoai);
            ViewBag.MaKM = new SelectList(db.khuyenMais, "ID", "TenKM", "PTKM", sanPham.MaKM);
            return View(sanPham);
        }
        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaTG = new SelectList(db.tacGias, "ID", "TenTacGia");
            ViewBag.MaNXB = new SelectList(db.nXBs, "ID", "TenNXB");
            ViewBag.MaLoai = new SelectList(db.theloais, "ID", "TenTheLoai");
            ViewBag.MaKM = new SelectList(db.khuyenMais, "ID", "TenKM", "PTKM");

            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenSP,MaTG,MaNXB,NamXB,MaLoai,MaKM,DanhGia,BinhLuan,Mota,ChieuCao,ChieuRong,SoTrang,HinhAnh,GiaSP,PTKM")] SanPham sanPham, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    string _file = Path.GetFileName(img.FileName);
                    sanPham.HinhAnh = _file;
                    string _path = Path.Combine(Server.MapPath("~/HinhAnh"), _file);
                    img.SaveAs(_path);
                }
                db.sanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTG = new SelectList(db.tacGias, "ID", "TenTacGia", sanPham.MaTG);
            ViewBag.MaNXB = new SelectList(db.nXBs, "ID", "TenNXB", sanPham.MaNXB);
            ViewBag.MaLoai = new SelectList(db.theloais, "ID", "TenTheLoai", sanPham.MaLoai);
            ViewBag.MaKM = new SelectList(db.khuyenMais, "ID", "TenKM", "PTKM", sanPham.MaKM);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenSP,MaTG,MaNXB,NamXB,MaLoai,MaKM,DanhGia,BinhLuan,Mota,ChieuCao,ChieuRong,SoTrang,HinhAnh,GiaSP,PTKM")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.sanPhams.Find(id);
            db.sanPhams.Remove(sanPham);
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
    }
}
