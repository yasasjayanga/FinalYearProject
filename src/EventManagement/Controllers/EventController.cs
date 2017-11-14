using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using EventManagement.Models.AccountViewModels;
using EventManagement.service;
using AutoMapper;
using EventManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using EventManagement.data;

namespace EventManagement.Controllers
{
    public class EventController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _environment;
        public EventController(UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            List<EventModel> model = new List<EventModel>();
            var events = new EventService().GetEventByUserId(await GetUserId(_userManager));
            Mapper.Map(events, model);
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new EventModel();
            model.Number = GenerateRandomNo().ToString();
            model.Tickets = 1;
            model.Date = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                model.UserId = await GetUserId(_userManager);
                var _event = new Event();
                Mapper.Map(model, _event);
                _event.Status = 1;
                new EventService().Create(_event);

                string fileName = string.Empty;
                if (file != null)
                {
                    var path = Path.Combine(_environment.WebRootPath, "uploads");
                    fileName = _event.Id + "~" + file.FileName;
                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    _event.Poster = fileName;
                    if (_event.Type == 1)
                    {
                        _event.Tickets = null;
                    }
                    new EventService().Update(_event);
                }
            }
            else
            {
                ViewBag.IsModelValid = false;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            EventModel model = new EventModel();
            var userInfo = new EventService().GetEventById(id);
            Mapper.Map(userInfo, model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventModel model, IFormFile file)
        {
            string fileName = string.Empty;
            if (file != null)
            {
                var path = Path.Combine(_environment.WebRootPath, "uploads");
                fileName = model.Id + "~" + file.FileName;
                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                model.Poster = fileName;
            }
            var _event = new Event();
            Mapper.Map(model, _event);
            new EventService().Update(_event);
            //return View(model);
            return RedirectToAction("Index");
        }

        public IActionResult GetEventById(long eventId)
        {
            EventModel model = new EventModel();
            var userInfo = new EventService().GetEventById(eventId);
            Mapper.Map(userInfo, model);
            if(!string.IsNullOrEmpty(model.Poster))
            {
                model.Poster = "/uploads/" + model.Poster;
            }
            return Json(model);
        }

        public IActionResult DeleteEvent(int id)
        {
            new EventService().DeleteEventById(id);
            return RedirectToAction("Index");
        }
    }
}
