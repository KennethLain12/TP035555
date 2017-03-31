using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ShipmentsController : Controller
    {
        private MLineEntities1 db = new MLineEntities1();

        // GET: Shipments
        public ActionResult Index()
        {
            var shipments = db.Shipments.Include(s => s.Warehouse).Include(s => s.Warehouse1).Include(s => s.Cargo);
            return View(shipments.ToList());
        }

        // GET: Shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Shipments/Create
        public ActionResult Create()
        {
            ViewBag.Source = new SelectList(db.Warehouses, "WarehouseId", "Location");
            ViewBag.Destination = new SelectList(db.Warehouses, "WarehouseId", "Location");
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "CargoId");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipmentId,Source,Destination,CargoId,Departure_Time,Arrival_Time")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Shipments.Add(shipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Source = new SelectList(db.Warehouses, "WarehouseId", "Location", shipment.Source);
            ViewBag.Destination = new SelectList(db.Warehouses, "WarehouseId", "Location", shipment.Destination);
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "CargoId", shipment.CargoId);
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Source = new SelectList(db.Warehouses, "WarehouseId", "Location", shipment.Source);
            ViewBag.Destination = new SelectList(db.Warehouses, "WarehouseId", "Location", shipment.Destination);
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "CargoId", shipment.CargoId);
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipmentId,Source,Destination,CargoId,Departure_Time,Arrival_Time")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Source = new SelectList(db.Warehouses, "WarehouseId", "Location", shipment.Source);
            ViewBag.Destination = new SelectList(db.Warehouses, "WarehouseId", "Location", shipment.Destination);
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "CargoId", shipment.CargoId);
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shipment shipment = db.Shipments.Find(id);
            db.Shipments.Remove(shipment);
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
    }
}
