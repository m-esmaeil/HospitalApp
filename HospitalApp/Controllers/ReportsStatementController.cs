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
    public class ReportsStatementController : Controller
    {
        private readonly DataContext _context;
        public ReportsStatementController (DataContext context)
        {
            _context = context;
        }


        // GET: incomeStatementController
        public ActionResult BalanceSheet(DateTime? startDate, DateTime? endDate)
        {
            var AccountsName = _context.AccountsTree.ToList();
            var TransactionsList = _context.Transactions
                                           .Include(x => x.AccountsTree)
                                           .ToList();



            List<AccountsSumVM> accountsSumVM = new List<AccountsSumVM>();

            if (startDate != null && endDate != null)
            {
                foreach (var item in AccountsName)
                {
                    accountsSumVM.Add(new AccountsSumVM
                    {
                        Account = item.Name,
                        SumCredit = TransactionsList.Where(x => x.AccountTreeId == item.Id && x.ValueCredit != null).Sum(x => x.ValueCredit),
                        SumDebit = TransactionsList.Where(x => x.AccountTreeId == item.Id && x.ValuDebit != null).Sum(x => x.ValuDebit)
                    });
                }

                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;

            }
            else
            {
                foreach (var item in AccountsName)
                {
                    accountsSumVM.Add(new AccountsSumVM
                    {
                        Account = item.Name,
                        SumCredit = TransactionsList.Where(x => x.AccountTreeId == item.Id && x.ValueCredit != null).Sum(x => x.ValueCredit),
                        SumDebit = TransactionsList.Where(x => x.AccountTreeId == item.Id && x.ValuDebit != null).Sum(x => x.ValuDebit)
                    });
                }

                ViewBag.startDate = null;
                ViewBag.endDate = null;
            }

            

            return View(accountsSumVM.Where(x => x.SumDebit - x.SumCredit != 0));
        }

        // GET: incomeStatementController/Details/5
        public ActionResult DetailsJournalLedger(int id)
        {
            var JournalEntry = _context.Transactions.Where(x => x.SerialNumberId == id).ToList();
            return View(JournalEntry);
        }

        // GET: incomeStatementController/Create
        public ActionResult LedgerAccounts()
        {
            var accounts = _context.Transactions
                                    .Include(x => x.AccountsTree)
                                    .Include(x => x.entriesSerialize)
                                    .ToList();
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
