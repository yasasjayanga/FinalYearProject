using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EventManagement.data;
using System.Data;

namespace EventManagement.service.Forum
{
    public enum PostOrderBy
    {
        Standard,
        Newest,
        Votes
    }

    public class PostService
    {
        private readonly EventManagementEntities _context = new EventManagementEntities();

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }

        public Post GetTopicStarterPost(int topicId, int? starterPostId)
        {

            var post = _context.Posts
                        .Include(x => x.Topic)
                        .Include(x => x.AspNetUser)
                        .FirstOrDefault(x => x.Topic.Id == topicId && x.Id == starterPostId);
            return post;
        }

        public IEnumerable<Post> GetAllWithTopics()
        {
            return _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => x.Pending != true);
        }

        public IList<Post> GetLowestVotedPost(int amountToTake)
        {
            return _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => x.Pending != true)
                .Take(amountToTake)
                .ToList();
        }

        public IList<Post> GetHighestVotedPost(int amountToTake)
        {
            return _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => x.Pending != true)
                //.OrderByDescending(x => x.VoteCount)
                .Take(amountToTake)
                .ToList();
        }

        public IList<Post> GetByMember(string memberId, int amountToTake)
        {
            return _context.Posts
                    .Include(x => x.Topic)
                    .Include(x => x.AspNetUser)
                    .Where(x => x.AspNetUser.Id == memberId && x.Pending != true)
                    .OrderByDescending(x => x.DateCreated)
                    .Take(amountToTake)
                    .ToList();
        }

        /// <summary>
        /// Get all posts that are solutions, by user
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IList<Post> GetSolutionsByMember(string memberId)
        {
            return _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => x.AspNetUser.Id == memberId)
                .Where(x => x.IsSolution && x.Pending != true)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }

        public List<Post> GetPostsByTopic(int topicId)
        {
            return _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => x.Topic.Id == topicId && (x.IsTopicStarter == null || x.IsTopicStarter == false))
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }

        public IList<Post> GetPostsByTopics(List<int> topicIds)
        {
            return _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => topicIds.Contains(x.Topic.Id) && x.Pending != true)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }

        public List<Post> GetPagedPendingPosts(int pageIndex, int pageSize)
        {
            var total = _context.Posts.Count(x => x.Pending == true);
            var results = _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => x.Pending == true)
                .OrderBy(x => x.DateCreated).ToList();

            return results;
        }

        public List<Post> GetPagedPostsByTopic(int topicId, int? starterPostId)
        {
            // We might only want to display the top 100
            // but there might not be 100 topics
            //var total = _context.Posts.Count(x => x.Topic.Id == topicId && x.Pending != true);

            // Get the topics using an efficient
            var results = _context.Posts
                                  .Include(x => x.AspNetUser)
                                  .Include(x => x.Topic)
                                  .Where(x => x.Topic.Id == topicId && x.Id != starterPostId);


            results = results.OrderBy(x => x.DateCreated);


            // sort the paging out
            var posts = results.ToList();

            // Return a paged list
            return posts;
        }

        public IList<Post> GetPostsByMember(string memberId)
        {
            return _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => x.AspNetUser.Id == memberId && x.Pending != true)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }

        public IList<Post> GetAllSolutionPosts()
        {
            return _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .Where(x => x.IsSolution && x.Pending != true)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }

        public List<Post> SearchPosts(int pageIndex, int pageSize, int amountToTake, List<string> searchTerms)
        {
            // We might only want to display the top 100
            // but there might not be 100 topics
            var query = _context.Posts
                            .Include(x => x.AspNetUser)
                            .Where(x => x.Pending != true);

            // Loop through each word and see if it's in the post
            foreach (var term in searchTerms)
            {
                var sTerm = term.Trim();
                query = query.Where(x => x.PostContent.ToLower().Contains(sTerm) || x.Topic.Name.ToLower().Contains(sTerm));
            }

            // Get the count
            var total = query.Count();

            if (amountToTake < total)
            {
                total = amountToTake;
            }

            // Get the Posts and then get the topics from the post
            // This is an interim solution, as its flawed due to multiple posts in one topic so the paging might
            // be incorrect if all posts are from one topic.
            var results = query
                        .OrderByDescending(x => x.DateCreated)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Return a paged list
            return results;
        }

        public Post Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public Post Get(int id)
        {
            return _context.Posts.Include(x => x.Topic)
                .Include(x => x.AspNetUser).FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Post item)
        {
            _context.Posts.Remove(item);
            _context.SaveChanges();
        }

        public void DeletePostByTopicId(int topicId)
        {
            var posts = _context.Posts.Where(m => m.Topic_Id == topicId).ToList();
            foreach (var item in posts)
            {
                _context.Posts.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Update(Post item)
        {
            //// Check there's not an object with same identifier already in context
            //if (_context.Posts.Local.Select(x => x.Id == item.Id).Any())
            //{
            //    throw new ApplicationException("Object already exists in context - you do not need to call Update. Save occurs on Commit");
            //}
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int PostCount()
        {
            return _context.Posts.Count();
        }

        public Post AddNewPost(string postContent, Topic topic, string userid)
        {
            PostService _postRepository = new PostService();
            // Has permission so create the post
            var newPost = new Post
            {
                PostContent = postContent,
                Topic_Id = topic.Id,
                DateCreated = DateTime.UtcNow,
                DateEdited = DateTime.UtcNow,
                UserId = userid
            };

            //newPost = SanitizePost(newPost);
            _postRepository.Add(newPost);
            return _postRepository.Get(newPost.Id);
            //return newPost;
        }

        //public Post SanitizePost(Post post)
        //{
        //    post.PostContent = StringUtils.GetSafeHtml(post.PostContent);
        //    return post;
        //}
    }
}
