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
    public class ReportsStatementController : Controller
    {
        private readonly DataContext _context;
        public ReportsStatementController(DataContext context)
        {
            _context = context;
        }


        // GET: all accounts in transactions
        public ActionResult BalanceSheet(DateTime? startDate, DateTime? endDate)
        {
            var AccountsName = _context.AccountsTree.ToList();
            var TransactionsList = _context.Transactions
                                          .Include(x => x.AccountsTree)
                                          .Where(x => x.entriesSerialize.date >= startDate.getFirstDayInCurrentYear() &&
                                                      x.entriesSerialize.date <= endDate.getLastDayInCurrentYear().getEndTimeOFDay())
                                          .ToList();



            List<AccountsSumVM> accountsSumVM = new List<AccountsSumVM>();

            foreach (var item in AccountsName)
            {
                accountsSumVM.Add(new AccountsSumVM
                {
                    Account = item.Name,
                    SumCredit = TransactionsList.Where(x => x.AccountTreeId == item.Id && x.ValueCredit != null).Sum(x => x.ValueCredit),
                    SumDebit = TransactionsList.Where(x => x.AccountTreeId == item.Id && x.ValuDebit != null).Sum(x => x.ValuDebit)
                });
            }


            ViewBag.startDate = startDate.getFirstDayInCurrentYear().ToShortDateString();
            ViewBag.endDate = endDate.getLastDayInCurrentYear().ToShortDateString();

            return View(accountsSumVM.Where(x => x.SumDebit > 0 || x.SumCredit > 0));
        }


        // GET: incomeStatementController/Details/5
        public ActionResult DetailsJournalLedger(int id)
        {
            var JournalEntry = _context.Transactions
                                       .Where(x => x.SerialNumberId == id)
                                       .Include(x => x.entriesSerialize)
                                       .ToList();

            ViewBag.serialOfEntryJournal = id;
            ViewBag.AccountsTreeList = _context.AccountsTree.ToList();

            return View(JournalEntry);
        }

        // GET: incomeStatementController/Create
        public ActionResult LedgerAccounts(DateTime? startDate, DateTime? endDate, int id)
        {
            var accounts = _context.Transactions
                                   .Include(x => x.entriesSerialize)
                                   .Where(x => x.entriesSerialize.date >= startDate.getFirstDayInCurrentYear() && 
                                               x.entriesSerialize.date <= endDate.getLastDayInCurrentYear().getEndTimeOFDay())
                                   .Include(x => x.AccountsTree)
                                   .ToList();

            ViewBag.startDate = startDate.getFirstDayInCurrentYear().ToShortDateString();
            ViewBag.endDate = endDate.getLastDayInCurrentYear().ToShortDateString();

            ViewBag.AccountsData = _context.AccountsTree.ToList();

            if (id > 0)
            {
                return View(accounts.Where(c => c.AccountTreeId == id));
            }

            return View(accounts);
        }

        // POST: incomeStatementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: incomeStatementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: incomeStatementController/Edit/5
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

        // GET: incomeStatementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: incomeStatementController/Delete/5
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
