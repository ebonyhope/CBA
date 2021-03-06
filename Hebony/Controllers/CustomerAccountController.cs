﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hebony.Models;

namespace Hebony.Controllers
{
    public class CustomerAccountController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: CustomerAccount
        public ActionResult Index()
        {
            var accounts = context.CustomerAccounts
                .Include(b => b.Branch)
                .Include(c => c.Customer)
                .ToList();
            return View(
                accounts
                );
        }

        // GET: CustomerAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerAccount customerAccount = context.CustomerAccounts.Find(id);
            if (customerAccount == null)
            {
                return HttpNotFound();
            }
            return View(customerAccount);
        }

        // GET: CustomerAccount/Create
        public ActionResult Create()
        {
            ViewData["Branches"] = context.Branches.ToList();
            ViewData["Customers"] = context.Customers.ToList();
            //ViewData["CustomerAccountTypes"] = context.CustomerAccountTypes.ToList();
            //ViewData["LinkedAccounts"] = context.CustomerAccounts.Where(c => c.Customer.Id == )
            return View();
        }

        // POST: CustomerAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerAccountViewModel model)
        {
            ViewData["Branches"] = context.Branches.ToList();
            ViewData["Customers"] = context.Customers.ToList();
            if (ModelState.IsValid)
            {
                CustomerAccount customerAccount = new CustomerAccount();
                customerAccount.AccNo = Helper.GenerateCustomerAccNo(model.AccountType, model.CustomerID);
                customerAccount.AccountType = (AccountType)model.AccountType;
                customerAccount.Balance = model.Balance;
                customerAccount.Branch = context.Branches.Find(model.BranchID);
                customerAccount.Customer = context.Customers.Find(model.CustomerID);
                context.CustomerAccounts.Add(customerAccount);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: CustomerAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerAccount customerAccount = context.CustomerAccounts.Include(b => b.Branch).SingleOrDefault( c => c.Id == id);
            if (customerAccount == null)
            {
                return HttpNotFound();
            }
            CustomerAccountViewModel model = new CustomerAccountViewModel();
            model.BranchID = customerAccount.Branch.ID;
            ViewData["Branches"] = context.Branches.ToList();
            return View(model);
        }

        // POST: CustomerAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerAccountViewModel model)
        {
            ViewData["Branches"] = context.Branches.ToList();
            if (ModelState.IsValid)
            {
                CustomerAccount customerAccount = context.CustomerAccounts.Find(model.Id);
                customerAccount.Branch = context.Branches.Find(model.BranchID);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //GET: CustomerAccount/Close/5
        public ActionResult Close(int id)
        {
            CustomerAccount customerAccount = context.CustomerAccounts.Find(id);
            customerAccount.IsActive = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CustomerAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerAccount customerAccount = context.CustomerAccounts.Find(id);
            if (customerAccount == null)
            {
                return HttpNotFound();
            }
            return View(customerAccount);
        }

        // POST: CustomerAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerAccount customerAccount = context.CustomerAccounts.Find(id);
            context.CustomerAccounts.Remove(customerAccount);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
