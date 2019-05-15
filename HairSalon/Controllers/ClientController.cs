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
          string redStr = "/stylists/" + id.ToString();
          return Redirect(redStr);
        }
        [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
        public ActionResult Show(int stylistId, int clientId)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist stylist = Stylist.Find(stylistId);
          model.Add("stylist", stylist);
          Client client = Client.Find(clientId);
          model.Add("client", client);
          return View(model);
        }
        [HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
        public ActionResult Edit(int stylistId, int clientId)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist stylist = Stylist.Find(stylistId);
          model.Add("stylist", stylist);
          Client client = Client.Find(clientId);
          model.Add("client", client);
          return View(model);
        }
        [HttpPost("/stylists/{stylistId}/clients/{clientId}")]
        public ActionResult Update(int stylistId, int clientId, string clientname, string phonenum)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist stylist = Stylist.Find(stylistId);
          model.Add("stylist", stylist);
          Client client = Client.Find(clientId);
          int newPhone = Int32.Parse(phonenum);
          client.Edit(clientname, newPhone);
          model.Add("client", client);
          return RedirectToAction("Show", model);
        }
        [HttpPost("/stylists/{stylistId}/clients/destroy/{clientId}")]
        public ActionResult Destroy(int stylistId, int clientId)
        {
          Client thisClient = Client.Find(clientId);
          thisClient.Delete();
          string redStr = "/stylists/" + stylistId.ToString();
          return Redirect(redStr);
        }
        [HttpPost("/stylists/{stylistId}/clients/destroy")]
        public ActionResult DestroyAll(int stylistId)
        {
          Client.ClearAllAtStylist(stylistId);
          string redStr = "/stylists/" + stylistId.ToString();
          return Redirect(redStr);
        }
    }

}
