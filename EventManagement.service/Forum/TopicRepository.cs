using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EventManagement.data;
using System.Data;
using EventManagement.Models;

namespace EventManagement.service.Forum
{
    public class TopicService
    {
        private readonly EventManagementEntities _context = new EventManagementEntities();
        
        public List<Topic> GetAll()
        {
            return _context.Topics.Include(x => x.AspNetUser).ToList();
        }

        /// <summary>
        /// Get the highest viewed topics
        /// </summary>
        /// <param name="amountToTake"></param>
        /// <returns></returns>
        public IList<Topic> GetHighestViewedTopics(int amountToTake)
        {
            return _context.Topics.Include(x => x.AspNetUser)
                                .AsNoTracking()
                            .Where(x => x.Pending != true)
                            .OrderByDescending(x => x.Views)
                            .Take(amountToTake)
                            .ToList();
        }

        public IList<Topic> GetPopularTopics(DateTime from, DateTime to, int amountToShow)
        {
            var topics = _context.Posts
                .Include(x => x.Topic)
                .Include(x => x.AspNetUser)
                .OrderByDescending(x => x.Topic.Posts.Count(c => c.DateCreated >= from && c.DateCreated <= to))

                .ThenByDescending(x => x.Topic.Views)
                .Take(amountToShow)
                .Select(x => x.Topic)
                .ToList();

            return topics;
        }

        public IList<Topic> GetTodaysTopics(int amountToTake)
        {
            return _context.Topics


                                .Include(x => x.AspNetUser)

                                .AsNoTracking()
                        .Where(c => c.CreateDate >= DateTime.Today && c.Pending != true)
                        .OrderByDescending(x => x.CreateDate)
                        .Take(amountToTake)
                        .ToList();
        }

        public Topic Add(Topic topic)
        {
            _context.Topics.Add(topic);
            _context.SaveChanges();
            return topic;
        }

        public void Update(Topic item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateStartPost(int topicId, int postId)
        {
            var post = _context.Topics.Where(m => m.Id == topicId).FirstOrDefault();
            post.PostId = postId;
            _context.SaveChanges();
        }

        public Topic Get(int id)
        {
            var topic = _context.Topics


                                .Include(x => x.AspNetUser)

                            .FirstOrDefault(x => x.Id == id);

            return topic;
        }

        public List<Topic> Get(List<int> ids)
        {
            return _context.Topics


                .Include(x => x.AspNetUser)

                .Where(x => ids.Contains(x.Id)).ToList();
        }

        public void Delete(Topic item)
        {
            _context.Topics.Remove(item);
            _context.SaveChanges();
        }

        public void DeleteTopisById(int id)
        {
            var topic = Get(id);
            Delete(topic);
        }

        public void OpenCloseTopic(int id, bool solve)
        {
            var topic = _context.Topics.Where(m => m.Id == id).FirstOrDefault();
            topic.Solved = solve;
            _context.SaveChanges();
        }

        public List<Topic> GetRecentTopics()
        {
            var results = _context.Topics.Include(x => x.AspNetUser)
                                .ToList();

            return results;
        }

        public void DeleteForum(int id)
        {
            var topic = _context.Topics.Where(m => m.Id == id).FirstOrDefault();
            foreach (var item in topic.Posts.ToList())
            {
                var post = _context.Posts.Where(m => m.Id == item.Id).FirstOrDefault();
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }

            _context.Topics.Remove(topic);
            _context.SaveChanges();
        }

        public IList<Topic> GetRecentRssTopics(int amountToTake)
        {
            // Get the topics using an efficient
            var results = _context.Topics


                                .Include(x => x.AspNetUser)

                                .AsNoTracking()
                                .Where(x => x.Pending != true)
                                .OrderByDescending(s => s.CreateDate)
                                .Take(amountToTake)
                                .ToList();

            return results;
        }

        public void DeletePost(int id)
        {
            var post = _context.Posts.Where(m => m.Id == id).FirstOrDefault();
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public IList<Topic> GetTopicsByUser(string memberId)
        {
            // Get the topics using an efficient
            var results = _context.Topics


                                .Include(x => x.AspNetUser)

                                .AsNoTracking()
                                .Where(x => x.AspNetUser.Id == memberId)
                                .Where(x => x.Pending != true)
                                .ToList();
            return results;
        }

        public IList<Topic> GetAllTopicsByCategory(Guid categoryId)
        {
            var results = _context.Topics


                                .Include(x => x.AspNetUser)

                                .AsNoTracking()
                                .Where(x => x.Pending != true)
                                .ToList();

            return results;
        }

        public IList<Topic> GetTopicsByLastPost(List<int> postIds)
        {
            return _context.Topics

                    .Where(x => postIds.Contains(x.Id))
                    .Where(x => x.Pending != true)
                    .ToList();
        }

        public List<Topic> GetPagedPendingTopics(int pageIndex, int pageSize)
        {
            // We might only want to display the top 100
            // but there might not be 100 topics
            var total = _context.Topics.Count(x => x.Pending == true);

            // Get the topics using an efficient
            var results = _context.Topics


                                .Include(x => x.AspNetUser)

                                .AsNoTracking()
                                .Where(x => x.Pending == true)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            // Return a paged list
            return results;
        }

        //public List<Topic> GetPagedTopicsByCategory(int pageIndex, int pageSize, int amountToTake, Guid categoryId)
        //{

        //    // We might only want to display the top 100
        //    // but there might not be 100 topics
        //    var total = _context.Topics.Count(x => x.Category.Id == categoryId);
        //    if (amountToTake < total)
        //    {
        //        total = amountToTake;
        //    }

        //    // Get the topics using an efficient
        //    var results = _context.Topics


        //                        .Include(x => x.AspNetUser)

        //                        .AsNoTracking()
        //                        .Where(x => x.Category.Id == categoryId)
        //                        .Where(x => x.Pending != true)
        //                        .OrderByDescending(x => x.IsSticky)
        //                        .ThenByDescending(x => x.LastPost.DateCreated)
        //                        .Skip((pageIndex - 1) * pageSize)
        //                        .Take(pageSize)
        //                        .ToList();

        //    // Return a paged list
        //    return new List<Topic>(results, pageIndex, pageSize, total);
        //}

        public List<Topic> GetPagedTopicsAll(int pageIndex, int pageSize, int amountToTake)
        {
            // We might only want to display the top 100
            // but there might not be 100 topics
            var total = _context.Topics.Count();
            if (amountToTake < total)
            {
                total = amountToTake;
            }

            // Get the topics using an efficient
            var results = _context.Topics


                                .Include(x => x.AspNetUser)

                                .AsNoTracking()
                                .Where(x => x.Pending != true)
                                .OrderByDescending(x => x.IsSticky)
                                .Take(pageSize)
                                .Skip((pageIndex - 1) * pageSize)
                                .ToList();

            // Return a paged list
            return results;
        }

        public List<Topic> SearchTopics(int pageIndex, int pageSize, int amountToTake, List<string> searchTerms)
        {
            // We might only want to display the top 100
            // but there might not be 100 topics
            var query = _context.Posts
                            .Include(x => x.Topic)
                            .Include(x => x.AspNetUser)
                            .Where(x => x.Pending != true);

            // Loop through each word and see if it's in the post
            foreach (var term in searchTerms)
            {
                var sTerm = term.Trim();
                query = query.Where(x => x.PostContent.ToLower().Contains(sTerm) || x.Topic.Name.ToLower().Contains(sTerm));
            }

            // Distinct by the topic id
            var result = query;

            // Get the count
            var total = result.Count();

            if (amountToTake < total)
            {
                total = amountToTake;
            }

            // Get the Posts and then get the topics from the post
            // This is an interim solution, as its flawed due to multiple posts in one topic so the paging might
            // be incorrect if all posts are from one topic.
            var results = result
                        .OrderByDescending(x => x.DateCreated)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .Select(x => x.Topic)
                        .ToList();

            // Return a paged list
            return results;
        }

        public List<Topic> GetMembersActivity(int pageIndex, int pageSize, int amountToTake, string memberGuid)
        {
            // We might only want to display the top 100
            // but there might not be 100 topics
            var total = _context.Posts.Where(x => x.AspNetUser.Id == memberGuid && x.Pending != true).Count();
            if (amountToTake < total)
            {
                total = amountToTake;
            }

            // Get the Posts and then get the topics from the post
            // This is an interim solution, as its flawed due to multiple posts in one topic so the paging might
            // be incorrect if all posts are from one topic.
            var results = _context.Posts
                            .Include(x => x.Topic)
                            .Include(x => x.AspNetUser)
                            .Where(x => x.AspNetUser.Id == memberGuid && x.Pending != true)
                            .OrderByDescending(x => x.DateCreated)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            // Return a paged list
            return results.Select(x => x.Topic).ToList();
        }

        //public List<Topic> GetTopicsByCsv(int pageIndex, int pageSize, int amountToTake, List<Guid> csv)
        //{
        //    // Get the count
        //    var total = _context.Topics
        //                        .Join(csv,
        //                              topic => topic.Id,
        //                              guidFromCsv => guidFromCsv,
        //                              (topic, guidFromCsv) => new { topic, guidFromCsv }
        //                              ).Count(x => x.guidFromCsv == x.Topics.Id);

        //    // Now get the paged stuff
        //    var results = _context.Topics


        //                        .Include(x => x.AspNetUser)

        //        .Join(csv,
        //                topic => topic.Id,
        //                guidFromCsv => guidFromCsv,
        //                (topic, guidFromCsv) => new { topic, guidFromCsv }
        //            )
        //            .Where(x => x.guidFromCsv == x.Topics.Id)
        //            .Where(x => x.Topics.Pending != true)
        //            .OrderByDescending(x => x.Topics.LastPost.DateCreated)
        //            .Skip((pageIndex - 1) * pageSize)
        //            .Take(pageSize)
        //            .Select(x => x.Topics)
        //            .ToList();

        //    // Return a paged list
        //    return new List<Topic>(results, pageIndex, pageSize, total);
        //}

        //public IList<Topic> GetTopicsByCsv(int amountToTake, List<Guid> topicIds)
        //{
        //    var topics = _context.Topics


        //                        .Include(x => x.AspNetUser)

        //                    .Where(x => topicIds.Contains(x.Id))
        //                    .Where(x => x.Pending != true)
        //                    .OrderByDescending(x => x.LastPost.DateCreated)
        //                    .Take(amountToTake)
        //                    .ToList();

        //    return topics;
        //}

        //public IList<Topic> GetRssTopicsByCategory(int amountToTake, Guid categoryId)
        //{
        //    var topics = _context.Topics


        //                        .Include(x => x.AspNetUser)

        //                    .Where(x => x.Category.Id == categoryId)
        //                    .Where(x => x.Pending != true)
        //                    .OrderByDescending(x => x.LastPost.DateCreated)
        //                    .Take(amountToTake)
        //                    .ToList();

        //    return topics;
        //}

        //public List<Topic> GetPagedTopicsByTag(int pageIndex, int pageSize, int amountToTake, string tag)
        //{
        //    // We might only want to display the top 100
        //    // but there might not be 100 topics
        //    var total = _context.Topics.Count(e => e.Tags.Any(t => t.Slug == tag));
        //    if (amountToTake < total)
        //    {
        //        total = amountToTake;
        //    }

        //    // Get the topics using an efficient
        //    var results = _context.Topics


        //                        .Include(x => x.AspNetUser)

        //                        .Include(x => x.Tags)
        //                        .Where(x => x.Pending != true)
        //                        .OrderByDescending(x => x.IsSticky)
        //                        .ThenByDescending(x => x.LastPost.DateCreated)
        //                        .Where(e => e.Tags.Any(t => t.Slug == tag))
        //                        .Take(pageSize)
        //                        .Skip((pageIndex - 1) * pageSize)
        //                        .ToList();

        //    // Return a paged list
        //    return new List<Topic>(results, pageIndex, pageSize, total);
        //}

        public Topic GetTopicBySlug(string slug)
        {
            return _context.Topics


                                .Include(x => x.AspNetUser)

                .FirstOrDefault(x => x.Slug == slug);
        }

        public IList<Topic> GetTopicBySlugLike(string slug)
        {
            return _context.Topics


                                .Include(x => x.AspNetUser)

                            .Where(x => x.Slug.Contains(slug))
                            .ToList();
        }

        public int TopicCount()
        {
            return _context.Topics.Count();
        }

        public Topic SanitizeTopic(Topic topic)
        {
            topic.Name = StringUtils.SafePlainText(topic.Name);
            return topic;
        }

        public Post AddLastPost(Topic topic, string postContent)
        {
            PostService _postRepository = new PostService();
            topic = SanitizeTopic(topic);

            // Create the post
            var post = new Post
            {
                AspNetUser = topic.AspNetUser,
                PostContent = (postContent),
                DateCreated = DateTime.Now,
                Topic_Id = topic.Id,
                UserId = topic.UserId,
                DateEdited = DateTime.Now,
                IsSolution = false,
                IsTopicStarter = true
            };

            // Add the post
            _postRepository.Add(post);
           
            return post;
        }

        /// <summary>
        /// Get all posts that are solutions, by user
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IList<Topic> GetSolvedTopicsByMember(string memberId)
        {
            var results = _context.Topics


                                .Include(x => x.AspNetUser)

                                .AsNoTracking()
                            .Where(x => x.AspNetUser.Id == memberId)
                            .Where(x => x.Pending != true)
                            .ToList();

            return results.Where(x => x.Posts.Select(p => p.IsSolution).Contains(true)).ToList();
        }

    }
}