using EventManagement.data;
using EventManagement.Models;
using EventManagement.service.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.service
{
    public class AdminService
    {
        public List<UsersModel> getUserByRoles()
        {
            using (var context = new EventManagementEntities())
            {
                return (from u in context.AspNetUsers
                        join t in context.Users on u.Id equals t.UserId
                        where u.AspNetRoles.Select(y => y.Name).Contains("user")
                        select new UsersModel
                        {
                            Id = t.Id,
                            Email = u.Email,
                            UserName = u.UserName,
                            FirstName = t.FirstName,
                            LastName = t.LastName,
                            Address = t.Address,
                            Gender = t.Gender,
                            ContactNumber = t.ContactNumber
                        }).ToList();
            }
        }

        public void DeleteUser(long id)
        {
            using (var context = new EventManagementEntities())
            {
                var user = context.Users.Where(m => m.Id == id).FirstOrDefault();
                if (user != null)
                {
                    var events = context.Events.Where(m => m.UserId == user.UserId).ToList();
                    if (events.Count > 0)
                    {
                        foreach (var item in events)
                        {
                            context.Events.Remove(item);
                            context.SaveChanges();
                        }
                    }

                    TopicService _topicService = new TopicService();
                    PostService _postService = new PostService();

                    var topics = context.Topics.Where(m => m.UserId == user.UserId).ToList();
                    if (topics.Count() > 0)
                    {
                        foreach (var topic in topics)
                        {
                            _postService.DeletePostByTopicId(topic.Id);
                            _topicService.DeleteTopisById(topic.Id);
                        }
                    }

                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
        }


        public List<Contact_Us> GetContacts()
        {
            using (var context = new EventManagementEntities())
            {
                return context.Contact_Us.OrderByDescending(m => m.Id).ToList();
            }
        }

        public Contact_Us GetContactInfo(int contactId)
        {
            using (var context = new EventManagementEntities())
            {
                return context.Contact_Us.Where(m => m.Id == contactId).OrderByDescending(m => m.Id).FirstOrDefault();
            }
        }

        public void DeleteContactInfo(int contactId)
        {
            using (var context = new EventManagementEntities())
            {
                var contactUs = context.Contact_Us.Where(m => m.Id == contactId).OrderByDescending(m => m.Id).FirstOrDefault();
                context.Contact_Us.Remove(contactUs);
                context.SaveChanges();
            }
        }

    }
}
