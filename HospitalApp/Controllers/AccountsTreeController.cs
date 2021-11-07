using HospitalApp.Models;
using HospitalApp.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.Controllers
{
    public class AccountsTreeController : Controller
    {
        private readonly DataContext _context;

        public AccountsTreeController(DataContext context)
        {
            _context = context;
        }
        // GET: AccounsTreeController
        public ActionResult Index()
        {
            var accounts = _context.AccountsTree
                                    .Include(z => z.Categories)
                                    .Include(z => z.Parent)
                                    .ToList();
            ViewBag.CompanyName = "لشركة مستشفي الخير الدولي ";



            return View(accounts);
        }

        // GET: AccounsTreeController/Details/5
        public ActionResult Details(int id)
        {
            var Acc2Display = _context.AccountsTree
                                      .Include(s => s.Categories)
                                      .Include(x => x.Parent)
                                      .FirstOrDefault(z => z.Id == id);

            return View(Acc2Display);
        }

        // GET: AccounsTreeController/Create
        public ActionResult Create()
        {
            var accountsList = new AccountsTreeVM
            {
                CategoriesVM = _context.Categories.ToList(),
                accountsTreeVM = _context.AccountsTree.ToList()
            };


            return View(accountsList);
        }


        // POST: AccounsTreeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountsTreeVM AccountSent2Add)
        {
            try
            {
                var Cat2Valide = _context.Categories.FirstOrDefault(x => x.Id == AccountSent2Add.CategoryId);
                if (Cat2Valide == null)
                {
                    AccountSent2Add.CategoriesVM = _context.Categories.ToList();
                    AccountSent2Add.accountsTreeVM = _context.AccountsTree.ToList();

                    return View(AccountSent2Add);
                }
                var Accout2Valide = _context.AccountsTree.Where(z => z.Name == AccountSent2Add.Name).Any();
                // Valide Exist Account
                if (Accout2Valide)
                {
                    ViewBag.MSG = " يوجد حساب بهذا الاســـــم ؟؟ ";

                    AccountSent2Add.CategoriesVM = _context.Categories.ToList();
                    AccountSent2Add.accountsTreeVM = _context.AccountsTree.ToList();

                    return View(AccountSent2Add);
                }

                // Valide Model
                if (ModelState.IsValid)
                {
                    var accountInValide = new AccountsTree 
                    {
                        CategoryId = AccountSent2Add.CategoryId,
                        Name = AccountSent2Add.Name,
                        FollowTo = AccountSent2Add.FollowTo,
                        
                    };

                    _context.Add(accountInValide);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                AccountSent2Add.CategoriesVM = _context.Categories.ToList();
                AccountSent2Add.accountsTreeVM = _context.AccountsTree.ToList();

                return View(AccountSent2Add);
            }
            catch
            {

                AccountSent2Add.CategoriesVM = _context.Categories.ToList();
                AccountSent2Add.accountsTreeVM = _context.AccountsTree.ToList();
                return View(AccountSent2Add);
            }
        }

        // GET: AccounsTreeController/Edit/5
        public ActionResult Edit(int id)
        {
            var Acc2Display = _context.AccountsTree
                                      .Include(x => x.Categories)
                                      .Include(x => x.Parent)
                                      .FirstOrDefault(s => s.Id == id);
            
            ViewBag.Acc = _context.AccountsTree.ToList();
            ViewBag.Cat = _context.Categories.ToList();

            return View(Acc2Display);
        }

        // POST: AccounsTreeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AccountsTree accFromView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Acc2Edit = _context.AccountsTree.Find(id);

                    Acc2Edit.Id = accFromView.Id;
                    Acc2Edit.Name = accFromView.Name;
                    Acc2Edit.FollowTo = accFromView.FollowTo;
                    Acc2Edit.CategoryId = accFromView.CategoryId;

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                return View(accFromView);
            }
            catch
            {
                return View(accFromView);
            }
        }

        // GET: AccounsTreeController/Delete/5
        public ActionResult Delete(int id)
        {
            var AccFromView = _context.AccountsTree.Find(id);

            if (AccFromView == null)
            {
                ViewBag.MSGDel = "لم يتم حذف الحساب ؟؟";
                return RedirectToAction(nameof(Index));
            }

            _context.AccountsTree.Remove(AccFromView);
            _context.SaveChanges();

            ViewBag.MSGConfirmDel = "تم حذف الحساب بنجاح !!";
            return RedirectToAction(nameof(Index));
        }


        // method To return value of FollowTo property in table
        //public string FollowToVAL(int? id)
        //{
        //    if (id == null)
        //    {
        //        return "";
        //    }
        //    return _context.AccountsTree
        //                   .Where(z => z.Id == id)
        //                   .Select(x => x.Name)
        //                   .ToString();
        //}
    }
}
