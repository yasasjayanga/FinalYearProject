using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventManagement.Data;
using Microsoft.AspNetCore.Identity;
using EventManagement.Models;
using EventManagement.service;
using EventManagement.Models.AccountViewModels;
using AutoMapper;
using EventManagement.data;
using EventManagement.service.Forum;
//using System.Web.Mvc;

namespace EventManagement.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult ManageUsers()
        {
            List<RegisterViewModel> model = new List<RegisterViewModel>();
            var userInfo = new AdminService().getUserByRoles();
            Mapper.Map(userInfo, model);
            return View(model);
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = new UsersService().getUser(id);
            new AdminService().DeleteUser(id);
            var aspUser = await _userManager.FindByIdAsync(user.UserId);
            await _userManager.DeleteAsync(aspUser);
            return RedirectToAction("ManageUsers");
        }

        public IActionResult ManageEvents()
        {
            List<EventModel> model = new List<EventModel>();
            var eventList = new EventService().GetEvents();
            Mapper.Map(eventList, model);
            return View(model);
        }

        //public IActionResult GetEventById(long eventId)
        //{
        //    EventModel model = new EventModel();
        //    var userInfo = new EventService().GetEventById(eventId);
        //    Mapper.Map(userInfo, model);
        //    return Json(model);
        //}

        public IActionResult DeleteEvent(int id)
        {
            new EventService().DeleteEventById(id);
            return RedirectToAction("ManageEvents");
        }

        public IActionResult DeleteContactInfo(int id)
        {
            new AdminService().DeleteContactInfo(id);
            return RedirectToAction("ManageContactUs");
        }

        [HttpPost]
        public IActionResult UpdateEventStatus(int status, long eventId, string comment)
        {
            return Json(new EventService().UpdateEventStatus(status, eventId, comment));
        }

        public IActionResult GetUserInfo(string apnetId)
        {
            var cuser = new EventService().GetUserInfo(apnetId);
            return Json(cuser);
        }

        public IActionResult GetContactInfo(int contactId)
        {
            ContactViewModel model = new ContactViewModel();
            var contactList = new AdminService().GetContactInfo(contactId);
            Mapper.Map(contactList, model);
            return Json(model);
        }

        public ActionResult Forums(int? id)
        {
            ForumModel ForumModel = new ForumModel();
            TopicService topicService = new TopicService();
            ForumModel.Topics = topicService.GetAll();
            ForumModel.Posts = new List<Post>();
            if (id.HasValue)
            {
                PostService postService = new PostService();
                ViewBag.successId = id;
                ForumModel.Posts = postService.GetPostsByTopic(id.Value);
            }

            ForumView_Model forumView_Model = new ForumView_Model();
            Mapper.Map(ForumModel, forumView_Model);
            return View(forumView_Model);
        }

        public ActionResult Close(int id)
        {
            TopicService topicService = new TopicService();
            topicService.OpenCloseTopic(id, true);
            return RedirectToAction("Forums");
        }

        public ActionResult OpenForum(int id)
        {
            TopicService topicService = new TopicService();
            topicService.OpenCloseTopic(id, false);
            return RedirectToAction("Forums");
        }

        public ActionResult DeleteForum(int id)
        {
            TopicService topicService = new TopicService();
            topicService.DeleteForum(id);
            return RedirectToAction("Forums");
        }

        public ActionResult DeletePost(int id)
        {
            TopicService topicService = new TopicService();
            topicService.DeletePost(id);
            return RedirectToAction("Forums");
        }

        public ActionResult ManageContactUs()
        {
            List<ContactViewModel> model = new List<ContactViewModel>();
            var contactList = new AdminService().GetContacts();
            Mapper.Map(contactList, model);
            return View(model);
        }

    }
    public class ForumModel
    {
        public List<Topic> Topics { get; set; }
        public List<Post> Posts { get; set; }
    }
}