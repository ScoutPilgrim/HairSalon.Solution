using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {
        [HttpGet("/specialties")]
        public ActionResult Index()
        {
          List<Specialty> allSpecialties = Specialty.GetAll();
          return View(allSpecialties);
        }
        [HttpGet("/specialties/new")]
        public ActionResult New()
        {
          return View();
        }
        [HttpPost("/specialties")]
        public ActionResult Create(string spec)
        {
          Specialty createdSpecialty = new Specialty(spec);
          createdSpecialty.Save();
          return RedirectToAction("Index");
        }
        [HttpGet("/specialties/{id}")]
        public ActionResult Show(int id)
        {
          Specialty foundSpecialty = Specialty.Find(id);
          return View(foundSpecialty);
        }
        [HttpGet("/specialties/{id}/edit")]
        public ActionResult Edit(int id)
        {
          Specialty thisSpecialty = Specialty.Find(id);
          return View(thisSpecialty);
        }
        [HttpPost("/specialties/{id}")]
        public ActionResult Update(int id, string spec)
        {
          Specialty thisSpecialty = Specialty.Find(id);
          thisSpecialty.Edit(spec);
          return RedirectToAction("Show", thisSpecialty);
        }
        [HttpPost("/specialties/destroy/{id}")]
        public ActionResult Destroy(int id)
        {
          Specialty thisSpecialty = Specialty.Find(id);
          thisSpecialty.Delete();
          return RedirectToAction("Index");
        }
        [HttpPost("/specialties/destroy")]
        public ActionResult DestroyAll()
        {
          Specialty.ClearAll();
          return RedirectToAction("Index");
        }
        [HttpPost("/specialties/{specialtyId}/stylists/new")]
        public ActionResult AddStylist(int specialtyId, int stylistId)
        {
          Specialty mySpec = Specialty.Find(specialtyId);
          Stylist myStylist = Stylist.Find(stylistId);
          mySpec.AddStylist(myStylist);
          return RedirectToAction("Show", new {id = specialtyId});
        }
    }
}
