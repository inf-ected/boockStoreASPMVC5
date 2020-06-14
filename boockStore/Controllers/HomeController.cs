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