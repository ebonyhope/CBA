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
    [Authorize(Roles = "Admin,Manager")]
    public class GLCategoryController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: GLCategory
        public ActionResult Index()
        {
            return View(context.GLCategories.ToList());
        }

        // GET: GLCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLCategory gLCategory = context.GLCategories.Find(id);
            if (gLCategory == null)
            {
                return HttpNotFound();
            }
            return View(gLCategory);
        }

        // GET: GLCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GLCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GLCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                GLCategory glCategory = new GLCategory();
                glCategory.Name = model.Name;
                glCategory.Description = model.Description;
                glCategory.MainCategory = (MainCategory)model.MainCategoryID;

                context.GLCategories.Add(glCategory);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: GLCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLCategory glCategory = context.GLCategories.Find(id);
            if (glCategory == null)
            {
                return HttpNotFound();
            }

            GLCategoryViewModel model = new GLCategoryViewModel();
            model.Id = glCategory.Id;
            model.Name = glCategory.Name;
            model.Description = glCategory.Description;
            model.MainCategoryID = (int)glCategory.MainCategory;

            return View(model);
        }

        // POST: GLCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GLCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                GLCategory glCategory = context.GLCategories.Find(model.Id);
                glCategory.Name = model.Name;
                glCategory.Description = model.Description;
                glCategory.MainCategory = (MainCategory)model.MainCategoryID;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: GLCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLCategory gLCategory = context.GLCategories.Find(id);
            if (gLCategory == null)
            {
                return HttpNotFound();
            }
            return View(gLCategory);
        }

        // POST: GLCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GLCategory gLCategory = context.GLCategories.Find(id);
            context.GLCategories.Remove(gLCategory);
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
