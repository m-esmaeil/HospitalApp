using HospitalApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }
        // GET: CategoriesController1
        public ActionResult Index()
        {
            var Cat2Display = _context.Categories.ToList();
            
            return View(Cat2Display);
        }

        // GET: CategoriesController1/Details/5
        public ActionResult Details(int id)
        {
            var Cat2Details = _context.Categories.Find(id);
            return View(Cat2Details);
        }

        // GET: CategoriesController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category cat2Add)
        {
            try
            {
                _context.Attach(cat2Add).State = EntityState.Added;
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController1/Edit/5
        public ActionResult Edit(int id)
        {
            var catFromDataBase = _context.Categories.SingleOrDefault(b => b.Id == id);

            return View(catFromDataBase);
        }

        // POST: CategoriesController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category Cat2Edit)
        {
            try
            {
                if (Cat2Edit != null)
                {
                    _context.Attach(Cat2Edit);
                    _context.Entry(Cat2Edit).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        //GET: CategoriesController1/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var Cat2Del = _context.Categories.Find(id);
                    _context.Remove(Cat2Del);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }

        }

        // POST: CategoriesController1/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ConfiDelete(int id)
        //{
            //try
            //{
            //    if (id > 0)
            //    {
            //        var Cat2Del = _context.Categories.Find(id);
            //        _context.Remove(Cat2Del);
            //        _context.SaveChanges();
            //        return RedirectToAction(nameof(Index));
            //    }
            //    return View();
            //}
            //catch
            //{
            //    return View();
            //}
        //}
    }
}
