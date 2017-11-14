using EventManagement.data;
using EventManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace EventManagement.service.Forum
{
    public static class ViewModelMapping
    {
        static EventManagementEntities _context = new EventManagementEntities();
        #region Membership
        
        public static AspNetRole RoleViewModelToRole(AspNetRole roleViewModel)
        {
            var viewModel = new AspNetRole
            {
                Name = roleViewModel.Name
            };
            return viewModel;
        }

        #endregion





        public static TopicViewModel CreateTopicViewModel(Topic topic,
                                                    List<Post> posts,
                                                    Post starterPost,
                                                    AspNetUser loggedOnUser,
                                                    bool getExtendedData = false)
        {

            var userIsAuthenticated = loggedOnUser != null;
            var viewModel = new TopicViewModel
            {
                Topic = topic,
                DisablePosting = loggedOnUser != null
            };

            // Get votes for all posts
            var postIds = posts.Select(x => x.Id).ToList();
            //postIds.Add(starterPost.Id);

            // Create the starter post viewmodel
            viewModel.StarterPost = CreatePostViewModel(starterPost, topic, loggedOnUser);

            // Create the ALL POSTS view models
            viewModel.Posts = CreatePostViewModels(posts, topic, loggedOnUser);

            // ########### Full topic need everything   


            return viewModel;
        }

        public static List<TopicViewModel> CreateTopicViewModels(List<Topic> topics)
        {
            PostService postRepository = new PostService();
            //// Get all topic Ids
            var topicIds = topics.Select(x => x.Id).ToList();

            //// Gets posts for topics
            //var postService = ServiceFactory.Get<IPostService>();
            var posts = postRepository.GetPostsByTopics(topicIds);
            var groupedPosts = posts.ToLookup(x => x.Topic.Id);

            //// Get all permissions
            //var permissions = GetPermissionsForTopics(topics, roleService, usersRole);

            // Create the view models
            var viewModels = new List<TopicViewModel>();
            foreach (var topic in topics)
            {
                var id = topic.Id;
                var topicPosts = (groupedPosts.Contains(id) ? groupedPosts[id].ToList() : new List<Post>());
                viewModels.Add(CreateTopicViewModel(topic, topicPosts, null, null, false));
            }
            return viewModels;
        }
        #region Post
        public static PostViewModel CreatePostViewModel(Post post, Topic topic, AspNetUser loggedOnUser)
        {
            return new PostViewModel
            {
                Post = post,
                ParentTopic = topic
            };
        }

        /// <summary>
        /// Maps the posts for a specific topic
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="votes"></param>
        /// <param name="permission"></param>
        /// <param name="topic"></param>
        /// <param name="loggedOnUser"></param>
        /// <param name="settings"></param>
        /// <param name="favourites"></param>
        /// <returns></returns>
        public static List<PostViewModel> CreatePostViewModels(IEnumerable<Post> posts, Topic topic, AspNetUser loggedOnUser)
        {
            var viewModels = new List<PostViewModel>();
            foreach (var post in posts)
            {
                var id = post.Id;
                viewModels.Add(CreatePostViewModel(post, topic, loggedOnUser));
            }
            return viewModels;
        }

        /// <summary>
        /// Maps posts from any topic must be pre filtered by checked the user has access to them
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="votes"></param>
        /// <param name="permissions"></param>
        /// <param name="loggedOnUser"></param>
        /// <param name="settings"></param>
        /// <param name="favourites"></param>
        /// <returns></returns>
        public static List<PostViewModel> CreatePostViewModels(IEnumerable<Post> posts, AspNetUser loggedOnUser)
        {
            var viewModels = new List<PostViewModel>();
            foreach (var post in posts)
            {
                var id = post.Id;
                var topic = post.Topic;
                viewModels.Add(CreatePostViewModel(post, topic, loggedOnUser));
            }
            return viewModels;
        }
        #endregion

        #region Category

        //TODO - Create generic category mapping

        #endregion
    }
}