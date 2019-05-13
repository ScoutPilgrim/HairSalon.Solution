using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HairSalonController : Controller
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
    }
}
