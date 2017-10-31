using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeInventory.Models;

namespace CafeInventory.Controllers
{
    public class CoffeeItemsController : Controller
    {
        private CoffeeBackStorageEntities db = new CoffeeBackStorageEntities();

        // GET: CoffeeItems
        public ActionResult Index()
        {
            return View(db.CoffeeItems.ToList());
        }

        // GET: CoffeeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeItem coffeeItem = db.CoffeeItems.Find(id);
            if (coffeeItem == null)
            {
                return HttpNotFound();
            }
            return View(coffeeItem);
        }

        // GET: CoffeeItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoffeeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemName,ItemDescription,Quantity,Price")] CoffeeItem coffeeItem)
        {
            if (ModelState.IsValid)
            {
                db.CoffeeItems.Add(coffeeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coffeeItem);
        }

        // GET: CoffeeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeItem coffeeItem = db.CoffeeItems.Find(id);
            if (coffeeItem == null)
            {
                return HttpNotFound();
            }
            return View(coffeeItem);
        }

        // POST: CoffeeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemName,ItemDescription,Quantity,Price")] CoffeeItem coffeeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coffeeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coffeeItem);
        }

        // GET: CoffeeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeItem coffeeItem = db.CoffeeItems.Find(id);
            if (coffeeItem == null)
            {
                return HttpNotFound();
            }
            return View(coffeeItem);
        }

        // POST: CoffeeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoffeeItem coffeeItem = db.CoffeeItems.Find(id);
            db.CoffeeItems.Remove(coffeeItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DeleteCoffeeItem(int id)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("DeleteCoffeeItem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlParameter ExecuteID = new System.Data.SqlClient.SqlParameter();
                ExecuteID.ParameterName = "@Id";
                ExecuteID.Value = id;
                cmd.Parameters.Add(ExecuteID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            
        }

        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            CoffeeItemsController BL = new CoffeeItemsController();

            BL.DeleteCoffeeItem(id);
            return RedirectToAction("Index");
        }
    }
}
