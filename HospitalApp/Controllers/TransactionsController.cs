using HospitalApp.Models;
using HospitalApp.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalApp.HelperClasses;

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
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var Trans2Display = _context.Transactions
                                        .Where(x => x.entriesSerialize.date >= startDate.getFirstDayInCurrentYear() && 
                                                    x.entriesSerialize.date <= endDate.getLastDayInCurrentYear().getEndTimeOFDay())
                                        .Include(x => x.AccountsTree)
                                        .Include(x => x.entriesSerialize)
                                        .Include(x => x.AccountsTree.Categories)
                                        .OrderBy(x => x.entriesSerialize.Id)
                                        .ThenBy(x => x.Id)
                                        .ToList();

            ViewBag.startDate = startDate.getFirstDayInCurrentYear().ToShortDateString();
            ViewBag.endDate = endDate.getLastDayInCurrentYear().ToShortDateString();

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
                AccountsTreeList = _context.AccountsTree.ToList(),
                DateVM = DateTime.Now,
                SerialNumberIdVM = _context.EntriesSerialize.Max(x => x.Id) + 1 
            };

            ViewBag.EntriesNum = _context.EntriesSerialize.Max(x => x.Id) + 1;

            return View(Trans2Create);
        }

        // POST: TransactionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionForm transForm)
        {
            var Trans2Create = new TransactionsVM
            {
                AccountsTreeList = _context.AccountsTree.ToList(),
                DateVM = DateTime.Now,
                SerialNumberIdVM = _context.EntriesSerialize.Max(m => m.Id) + 1
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var sumDebit = transForm.debit.Sum();
                    var sumCredit = transForm.credit.Sum();

                    if (sumCredit != sumDebit)
                    {
                        ModelState.AddModelError("", "القيد غير متوازن");
                        return View(Trans2Create);
                    }

                    var serialNum = _context.EntriesSerialize.Count() + 1;

                    EntriesSerialize serialeNum = new EntriesSerialize()
                    {
                        Description = transForm.Description,
                        Serial = serialNum,
                        date = transForm.DateVm
                    };

                    _context.EntriesSerialize.Add(serialeNum);
                    _context.SaveChanges();

                    var lastSerialNum = _context.EntriesSerialize.Max(x => x.Id);

                    for (int i = 0; i < transForm.account.Length; i++)
                    {
                        var trans2Add = new Transaction()
                        {
                            AccountTreeId = transForm.account[i],
                            ValuDebit = transForm.debit[i],
                            ValueCredit = transForm.credit[i],
                            SerialNumberId = lastSerialNum
                        };

                        _context.Transactions.Add(trans2Add);
                    }

                    _context.SaveChanges();
                    ViewBag.SaveMSG = "تم حفظ القيد بنجاح !";

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "لم يتم حفظ القيد ,,,,");

            }
            catch
            {
                
                ViewBag.DoNotSaveMSG = "لم يتم حفظ القيد ";
                return View(transForm);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: TransactionsController/Edit/5
        public ActionResult Edit(int id)
        {
           var Trans2Edite = _context.Transactions
                                     .Where(x => x.SerialNumberId == id)
                                     .Include(x => x.entriesSerialize)
                                     .Include(x => x.AccountsTree)
                                     .ToList();
            if (Trans2Edite == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.serialOfEntryJournal = id;
            ViewBag.AccountsTreeList = _context.AccountsTree.ToList();

            return View(Trans2Edite);
        }

        // POST: TransactionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int serialNumberId, TransactionForm transactionForm)
        {
            try
            {
                var sumDebit = transactionForm.debit.Sum();
                var sumCredit = transactionForm.credit.Sum();

                if (sumCredit != sumDebit)
                {
                    ModelState.AddModelError("", "القيد غير متوازن");
                }

                if (ModelState.IsValid)
                {
                    List<Transaction> oldTransactions = _context.Transactions
                                                            .Where(x => x.SerialNumberId == serialNumberId)
                                                            .Include(x => x.entriesSerialize)
                                                            .Include(x => x.AccountsTree)
                                                            .ToList();

                    // add new transactions => id=0
                    var submittedTransactions = transactionForm.GetTransactions(serialNumberId);
                    _AddNewTransactions(submittedTransactions);

                    // update old transactions
                    // remove deleted transactions
                    _UpdateOrDeletedExistingTransactions(oldTransactions, submittedTransactions);

                    // update description
                    var oldJournalEntry = _context.EntriesSerialize.Find(serialNumberId);
                    oldJournalEntry.Description = transactionForm.Description;
                    oldJournalEntry.date = transactionForm.DateVm;
                    
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                // error - return to form, Html.ValidationSummary()
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction(nameof(Edit),transactionForm);
            }
        }

        private void _UpdateOrDeletedExistingTransactions(List<Transaction> oldTransactions, IEnumerable<Transaction> submittedTransactions)
        {
            foreach (var oldTrans in oldTransactions)
            {
                // if  exist edit, else delete
                var newTrans = submittedTransactions.FirstOrDefault(t => t.Id == oldTrans.Id);
                if (newTrans != null)
                {
                    // update
                    oldTrans.ValuDebit = newTrans.ValuDebit;
                    oldTrans.ValueCredit = newTrans.ValueCredit;
                    oldTrans.AccountTreeId = newTrans.AccountTreeId;
                }
                else
                {
                    // delete
                    _context.Transactions.Remove(oldTrans);
                }
            }
        }

        private void _AddNewTransactions(IEnumerable<Transaction> submittedTransactions)
        {
            foreach (var trans in submittedTransactions.Where(t => t.Id == 0))
                _context.Transactions.Add(trans);
        }

        // GET: TransactionsController/Delete/5
        public ActionResult Delete(int id)
        {

            var transaction2Delete = _context.Transactions.Where(x => x.SerialNumberId == id).ToList();
            var entryRelated = _context.EntriesSerialize.Find(id);

            if (entryRelated != null && transaction2Delete != null)
            {
                foreach (var item in transaction2Delete)
                {
                    _context.Remove(item);
                }

                _context.Remove(entryRelated);
                _context.SaveChanges();

                ViewBag.EntryDeleted = "تم حذف القيد بنجاح !";

                return RedirectToAction(nameof(Index));
                
            };


            return RedirectToAction(nameof(Index));
        }

        //// POST: TransactionsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
