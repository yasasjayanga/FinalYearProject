using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using EventManagement.Models;
using EventManagement.Models.AccountViewModels;
using EventManagement.Services;
using EventManagement.service;
using AutoMapper;
using EventManagement.data;

namespace EventManagement.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            Mapper.CreateMap<RegisterViewModel, User>();
            Mapper.CreateMap<User, RegisterViewModel>();

            Mapper.CreateMap<ContactViewModel, Contact_Us>();
            Mapper.CreateMap<Contact_Us, ContactViewModel>();

            Mapper.CreateMap<RegisterViewModel, UsersModel>();
            Mapper.CreateMap<UsersModel, RegisterViewModel>();

            Mapper.CreateMap<EventModel, Event>();
            Mapper.CreateMap<Event, EventModel>();

            Mapper.CreateMap<ForumModel, ForumView_Model>();
            Mapper.CreateMap<ForumView_Model, ForumModel>();

            Mapper.CreateMap<Post, PostView_Model>();
            Mapper.CreateMap<PostView_Model, Post>();

            //Mapper.CreateMap<Post, Post_Model>();
            //Mapper.CreateMap<Post_Model, Post_Model>();

            //Mapper.CreateMap<PostViewModel, Post_Model>();
            //Mapper.CreateMap<Post_Model, PostViewModel>();

            //Mapper.CreateMap<PostViewModel, PostView_Model>();
            //Mapper.CreateMap<PostView_Model, PostViewModel>();

            //Mapper.CreateMap<PostViewModel, PostViewModel>();

            Mapper.CreateMap<Topic, TopicView_Model>();
            Mapper.CreateMap<TopicView_Model, Topic>();

            Mapper.CreateMap<TopicViewModel, TopicView_Model>();
            Mapper.CreateMap<TopicView_Model, TopicViewModel>();

            //Mapper.CreateMap<User, UserView_Model>();
            //Mapper.CreateMap<UserView_Model, User>();

            Mapper.CreateMap<AspNetUser, AspNetUserView_Model>();
            Mapper.CreateMap<AspNetUserView_Model, AspNetUser>();

            Mapper.CreateMap<TopicViewModel, TopicDetailView_Model>();
            Mapper.CreateMap<TopicDetailView_Model, TopicViewModel>();

            Mapper.CreateMap<Topic, TopicDetail_Model>();
            Mapper.CreateMap<TopicDetail_Model, Topic>();

            Mapper.CreateMap<Post, PostDetail_Model>();
            Mapper.CreateMap<PostDetail_Model, Post>();

            Mapper.CreateMap<PostDetailViewModel, PostViewModel>();
            Mapper.CreateMap<PostViewModel, PostDetailViewModel>();

            Mapper.CreateMap<CreateEditTopicViewModel, CreateEditTopicView_Model>();
            Mapper.CreateMap<CreateEditTopicView_Model, CreateEditTopicViewModel>();

        }

        public async Task<string> GetUserId(UserManager<ApplicationUser> _userManager)
        {
            var current_User = await _userManager.FindByNameAsync(User.Identity.Name);
            return current_User.Id;
        }

        public int GenerateRandomNo()
        {
            int _min = 100000;
            int _max = 999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

    }
}
