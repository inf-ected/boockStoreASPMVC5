using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using boockStore.Models;

namespace boockStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        public ActionResult Index()
        {
            ViewBag.Message = "Это частичное представление";

            // получаем список всех книг
            //var books = db.Books;
            //ViewBag.Books = books;
            //return View();
            ///* Второй вариант со строго типизированными представлениями */
            var books = db.Books;
            return View(books);

        }
        [HttpGet]
        public ActionResult Create() {

            return View();        
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            db.Books.Add(book); //SQL INSERT
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(int id)
        //{
        //    Book b = db.Books.Find(id); // 1ый запрос 
        //    if (b != null) {
        //        db.Books.Remove(b);// SQl DELETE // 2-ой запрос
        //        db.SaveChanges();
        //    }

        //    //вариант в 1 запрос 
        //    //Book bb = new Book { Id = id };
        //    //db.Entry(bb).State = System.Data.Entity.EntityState.Deleted;
        //    //db.SaveChanges();

        //    // НО! <img src="нашсайт/Home/Delete/1" />

        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult Delete(int id) {
            Book b = db.Books.Find(id); 
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost , ActionName("Delete") ]
        public ActionResult DeleteConfirmed(int id) {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(b);// SQl DELETE // 2-ой запрос
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();

            Book b = db.Books.Find(id);
            if (b != null)
            {
                return View(b);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            db.Entry(book).State = System.Data.Entity.EntityState.Modified; // sql UPDATE
            db.SaveChanges();
            return RedirectToAction("Index");
        }





        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }

        public ActionResult GetList() {
            ViewBag.Message = "Это частичное представление";
            string[] states = { "USA", "FR", "DE" };
            return PartialView("~/Views/Home/_getList.cshtml",states);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}