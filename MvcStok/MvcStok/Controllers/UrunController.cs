using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> 
                degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }
                 ).ToList();
            ViewBag.dgr = degerler;
            //Bu işlemlerin hepsi get işlemi yapıldığında yani sayfa yüklendiğinde yapılacak [httpget]
            //Yukarıdaki kod bloğunda linq sorgusu yaptım. önce tblkategolerin listesini çek sonra bunu i değişenine ata.
            // bu yeni listeyi seçiyorum ve sonrasında textini ve ıd sini çekeceğim için...
            // text e listemdeki textleri, value ile id lerini alıyorum ve sonrasında listeliyorum.
            //viewbag.dgr ile de  controller içindeki ifadeyi başka yere taşıyacağım. dgr nesnesi oluşturup başka yerde kullacanağım.
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
            //Kategorideki ıd yi almak için linq sorgusu kullandım. bloğun ilk kodunda where p1 den gelen
            // kategorilerdeki katıgoriıd yi getirecek.firstofdefault linq da (seçilen ilk değeri getirir.)
            // ktg yi p1 kategorilere ata.
            //sonra redirecttoaction da kaydet yapıldıktan sonra istediğim sayfaya götürüyor.
        }

        public ActionResult SIL (int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
            //Bu blokta db nesnesinin ürünler sayfasındaki id lerini bul.
            //sonrasında kategoriden gelen değeri sil deyip kaydedip ındex sayfasına beni yönlendir dedim.
        }

        public ActionResult UrunGetir (int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem>
                degerler = (from i in db.TBLKATEGORILER.ToList()
                            select new SelectListItem
                            {
                                Text = i.KATEGORIAD,
                                Value = i.KATEGORIID.ToString()
                            }
                 ).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle (TBLURUNLER p)
        {
                var urun = db.TBLURUNLER.Find(p.URUNID);
                urun.URUNAD = p.URUNAD;
                urun.MARKA = p.MARKA;
                urun.STOK = p.STOK;
                urun.FIYAT = p.FIYAT;
                //urun.URUNKATEGORI = p.URUNKATEGORI;
                var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
                urun.URUNKATEGORI = ktg.KATEGORIID;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            
        }
    }
}