using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
        [HttpGet("/stylists")]
        public ActionResult Index()
        {
          List<Stylist> allStylists = Stylist.GetAll();
          return View(allStylists);
        }
        [HttpGet("/stylists/new")]
        public ActionResult New()
        {
          return View();
        }
        [HttpPost("/stylists")]
        public ActionResult Create(string stylelistname, string phonenum)
        {
          int phoneNum = Int32.Parse(phonenum);
          Stylist createdStylist = new Stylist(stylelistname, phoneNum);
          createdStylist.Save();
          return RedirectToAction("Index");
        }
        [HttpGet("/stylists/{id}")]
        public ActionResult Show(int id)
        {
          Stylist foundStylist = Stylist.Find(id);
          return View(foundStylist);
        }
        [HttpGet("/stylists/{id}/edit")]
        public ActionResult Edit(int id)
        {
          Stylist thisStylist = Stylist.Find(id);
          return View(thisStylist);
        }
        [HttpPost("/stylists/{id}")]
        public ActionResult Update(int id, string stylelistname, string phonenum)
        {
          Stylist thisStylist = Stylist.Find(id);
          int phoneNum = Int32.Parse(phonenum);
          thisStylist.Edit(stylelistname, phoneNum);
          return RedirectToAction("Show", thisStylist);
        }
        [HttpPost("/stylists/destroy/{id}")]
        public ActionResult Destroy(int id)
        {
          Stylist thisStylist = Stylist.Find(id);
          thisStylist.Delete();
          return RedirectToAction("Index");
        }
        [HttpPost("/stylists/destroy")]
        public ActionResult DestroyAll()
        {
          Stylist.ClearAll();
          return RedirectToAction("Index");
        }
    }
}
