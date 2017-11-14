using EventManagement.data;
using EventManagement.Models;
using EventManagement.service.Forum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Net;
using AutoMapper;

namespace EventManagement.Controllers
{
    public class ForumsController : BaseController
    {
        public IActionResult Index()
        {
            TopicService _topicService = new TopicService();
            // Get the topics
            var topics = _topicService.GetRecentTopics();

            //// Get the Topic View Models
            //var topicViewModels = ViewModelMapping.CreateTopicViewModels(topics).Select(m=>m.Topic).ToList();

            List<TopicView_Model> topicView_Model = new List<TopicView_Model>();
            Mapper.Map(topics, topicView_Model);
            return View(topicView_Model);
        }

        public IActionResult Detail(int id)
        {
            TopicService _topicService = new TopicService();
            PostService _postService = new PostService();
            // Get the topic
            var topic = _topicService.Get(id);

            if (topic != null)
            {
                // Get the posts
                var posts = topic.Posts.Where(m => m.Id != topic.PostId).ToList(); // _postService.GetPagedPostsByTopic(topic.Id, topic.PostId);

                // Get the topic starter post
                var starterPost = topic.Posts.Where(m => m.Id == topic.PostId).FirstOrDefault(); // _postService.GetTopicStarterPost(topic.Id, topic.PostId);

                var viewModel = ViewModelMapping.CreateTopicViewModel(topic, posts.ToList(), starterPost, new AspNetUser(), true);
                TopicDetailView_Model topicDetailView_Model = new TopicDetailView_Model();
                Mapper.Map(viewModel, topicDetailView_Model);
                return View(topicDetailView_Model);
            }
            return View();
        }

        public IActionResult Create()
        {
            CreateEditTopicView_Model model = new CreateEditTopicView_Model();
            model.IsTopicStarter = true;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateEditTopicView_Model topicViewModel)
        {
            TopicService _topicService = new TopicService();
            topicViewModel.IsTopicStarter = true;

            if (ModelState.IsValid)
            {
                var topic = new Topic();

                var id = (string)IdentityExtensions.GetUserId(this.User.Identity);
                topic = new Topic
                {
                    Name = topicViewModel.Name,
                    UserId = id,
                    CreateDate = DateTime.Now,
                    Solved = false,
                    Slug = "",
                    IsSticky = false,
                    IsLocked = false
                };

                // See if the user has actually added some content to the topic
                if (!string.IsNullOrEmpty(topicViewModel.Content))
                {
                    topicViewModel.Content = topicViewModel.Content;

                    topic = _topicService.Add(topic);

                    var post = _topicService.AddLastPost(topic, topicViewModel.Content);

                    //update topic
                    //topic = _topicService.Get(topic.Id);
                    //topic.PostId = post.Id;
                    _topicService.UpdateStartPost(topic.Id, post.Id);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeletePost(int id, int postId)
        {
            TopicService _topicService = new TopicService();
            PostService _postService = new PostService();

            var post = _postService.Get(postId);

            var userId = (string)IdentityExtensions.GetUserId(this.User.Identity);
            if (post.AspNetUser.Id == userId)
            {
                if (post.Topic.PostId == postId)
                {
                    _postService.DeletePostByTopicId(id);
                    _topicService.DeleteTopisById(id);
                }
                else
                {
                    _postService.Delete(post);
                    return RedirectToAction("Detail", new { id = id });
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            PostService _postService = new PostService();
            // Get the post
            var post = _postService.Get(id);

            // Get the topic
            var topic = post.Topic;
            var userId = (string)IdentityExtensions.GetUserId(this.User.Identity);
            // Is the user allowed to edit this post
            if (post.AspNetUser.Id == userId)
            {
                // If this user hasn't got any allowed cats OR they are not allowed to post then abandon

                // Create the model for just the post
                var viewModel = new CreateEditTopicViewModel
                {
                    Content = WebUtility.HtmlDecode(post.PostContent),
                    Id = post.Id,
                    Name = topic.Name
                };

                // Populate the properties we can
                viewModel.IsLocked = topic.IsLocked;
                viewModel.IsSticky = topic.IsSticky;
                viewModel.Id = post.Id;
                viewModel.TopicId = topic.Id;
                viewModel.IsTopicStarter = post.IsTopicStarter.HasValue ? post.IsTopicStarter.Value : false;
                CreateEditTopicView_Model createEditTopicView_Model = new CreateEditTopicView_Model();
                Mapper.Map(viewModel, createEditTopicView_Model);
                return View(createEditTopicView_Model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateEditTopicView_Model editPostViewModel)
        {
            if (ModelState.IsValid)
            {
                PostService _postService = new PostService();
                TopicService _topicService = new TopicService();
                // Got to get a lot of things here as we have to check permissions
                // Get the post
                var post = _postService.Get(editPostViewModel.Id);

                // Get the topic
                var topic = _topicService.Get(post.Topic_Id);
                var id = (string)IdentityExtensions.GetUserId(this.User.Identity);
                if (post.AspNetUser.Id == id)
                {
                    // User has permission so update the post
                    post.PostContent = (editPostViewModel.Content);
                    post.DateEdited = DateTime.UtcNow;

                    topic.IsLocked = editPostViewModel.IsLocked;
                    topic.IsSticky = editPostViewModel.IsSticky;
                    //if (editPostViewModel.IsTopicStarter)
                    //{
                    //    topic.Name = StringUtils.GetSafeHtml(editPostViewModel.Name);
                    //}
                    //else
                    //{
                    //}
                        topic.Name = topic.Name;
                    _postService.Update(post);
                    _topicService.Update(topic);
                }
                editPostViewModel.TopicId = topic.Id;
            }
            return View(editPostViewModel);
        }

        [HttpPost]
        public ActionResult CreatePost(CreateAjaxPostViewModel post)
        {
            TopicService _topicService = new TopicService();
            PostService _postService = new PostService();

            Post newPost;
            Topic topic;

            topic = _topicService.Get(post.Topic);

            var postContent = post.PostContent;

            var id = (string)IdentityExtensions.GetUserId(this.User.Identity);
            newPost = _postService.AddNewPost(postContent, topic, id);

            //Check for moderation
            if (newPost.Pending == true)
            {
                return PartialView("_PostModeration");
            }

            return RedirectToAction("Detail", new { id = post.Topic });

            // Create the view model
            //var viewModel = ViewModelMapping.CreatePostViewModel(newPost, topic, newPost.AspNetUser);

            // Return view
            //return PartialView("_Post", viewModel);

        }
    }
}