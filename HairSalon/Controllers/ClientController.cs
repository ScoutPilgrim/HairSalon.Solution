using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/stylists/{id}/clients")]
        public ActionResult Index(int id)
        {
          Stylist stylist = Stylist.Find(id);
          List<Client> allClients = stylist.GetAllClients();
          return View(allClients);
        }
        [HttpGet("/stylists/{stylistId}/clients/new")]
        public ActionResult New(int stylistId)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist stylist = Stylist.Find(stylistId);
          model.Add("stylist", stylist);
          return View(model);
        }
        [HttpPost("/stylists/{id}/clients")]
        public ActionResult Create(string clientname, string phonenum, int id)
        {
          int phoneNum = Int32.Parse(phonenum);
          Client createdClient = new Client(clientname, phoneNum, id);
          createdClient.Save();
          return RedirectToAction("Index");
        }
    }
}