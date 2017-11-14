using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using EventManagement.Models.AccountViewModels;
using EventManagement.service;
using AutoMapper;
using EventManagement.data;

namespace EventManagement.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult InformationLink()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            try
            {
                var contactUs = new data.Contact_Us();
                Mapper.Map(model, contactUs);
                new HomeService().ContactUs(contactUs);
                TempData["test"] = true;
                return RedirectToAction("Contact");
            }
            catch
            {
                return View(model);
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
