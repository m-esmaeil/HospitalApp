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
    public class TransactionsController : Controller
    {
        private readonly DataContext _context;

        public TransactionsController(DataContext context)
        {
            _context = context;
        }
        // GET: TransactionsController
        public ActionResult Index()
        {
            var Trans2Display = _context.Transactions
                                        .Include(x => x.AccountsTree)
                                        .Include(x => x.entriesSerialize)
                                        .OrderBy(x => x.SerialNumberId)
                                        .ToList();

            return View(Trans2Display);
        }

        // GET: TransactionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionsController/Create
        public ActionResult Create()
        {
            var Trans2Create = new TransactionsVM
            {
                AccountsTreeList = _context.AccountsTree.ToList()
            };


            return View(Trans2Create);
        }

        // POST: TransactionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionsVM transVM, IList<object> objArr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trans2add = new List<Transaction>();

                    EntriesSerialize serialeNum = new EntriesSerialize()
                    {
                        Description = transVM.EntrySerialize.Description
                    };

                    foreach (var item in transVM.AccountsTreeList)
                    {
                        var transdata = new Transaction
                        {
                            Date = transVM.DateVM,
                            SerialNumberId = serialeNum.Id,
                            ValuDebit = transVM.ValueDebit,
                            ValueCredit = transVM.ValueCredit,
                            AccountTreeId = transVM.AccountTreeIdVM,
                        };
                        trans2add.Add(transdata);
                    }

                    var sumDebit = trans2add.Sum(x => x.ValuDebit);
                    var sumCredit = trans2add.Sum(x => x.ValueCredit);

                    if (sumCredit != sumDebit)
                    {
                        ViewBag.TransSaveMSG = "القيد غير متوازن";
                        return View(trans2add);
                    }

                    _context.Transactions.AddRange(trans2add);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                ViewBag.TransSaveMSG = "لم يتم حفظ القيد ";
                return View(transVM);

            }
            catch
            {
                transVM.AccountsTreeList = _context.AccountsTree.ToList();
                ViewBag.TransSaveMSG = "لم يتم حفظ القيد ";
                return View(transVM);
            }
        }

        // GET: TransactionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
