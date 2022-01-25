using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity; // Model içindeki entity klasörüne ulaşıyorum.
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        // tablolarımı tutan modelim yani mvcdbstokentities sınıfından nesne üretiyorumki tablolarıma ulaşabileyim.
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 4);
            //var degerler = db.TBLKATEGORILER.ToList();
            // degerler değişkeniyle db nesnemdeki tablomu listeliyorum. ve degerleri gösteriyorum.
            return View(degerler);
        }
        [HttpGet]
        public ActionResult Yenikategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir (int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult Guncelle (TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");

            // p1 parametresi yarattım ve sonrasında ktg nesnesi oluşturup tablom içinden p1.kategoriid mi bul dedim.
            //parametredeki adımı nesnemedekine attım
        }
    }
}