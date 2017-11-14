using EventManagement.data;
using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.service
{
    public class EventService
    {
        public List<Event> GetEvents()
        {
            using (var context = new EventManagementEntities())
            {
                return context.Events.OrderByDescending(m => m.Id).ToList();
            }
        }

        public Event GetEventById(long eventId)
        {
            using (var context = new EventManagementEntities())
            {
                return context.Events.Where(m => m.Id == eventId).FirstOrDefault();
            }
        }

        public void DeleteEventById(long eventId)
        {
            using (var context = new EventManagementEntities())
            {
                var _event = context.Events.Where(m => m.Id == eventId).FirstOrDefault();
                context.Events.Remove(_event);
                context.SaveChanges();
            }
        }

        public int UpdateEventStatus(int status, long eventId, string comment)
        {
            using (var context = new EventManagementEntities())
            {
                var _event = context.Events.Where(m => m.Id == eventId).FirstOrDefault();
                _event.Status = status;
                //1: Pending, 2: Approve, 3: Disapprove
                if (status == 3)
                {
                    _event.DisapproveComments = comment;
                }
                else
                {
                    _event.DisapproveComments = string.Empty;
                }
                context.SaveChanges();
                return _event.Status;
            }
        }

        public List<Event> GetEventByUserId(string userId)
        {
            using (var context = new EventManagementEntities())
            {
                return context.Events.Where(m => m.UserId == userId).OrderByDescending(m => m.Id).ToList();
            }
        }

        public UsersModel GetUserInfo(string aspnetId)
        {
            using (var context = new EventManagementEntities())
            {
                return (from u in context.AspNetUsers
                        join t in context.Users on u.Id equals t.UserId
                        where u.Id == aspnetId
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
                        }).FirstOrDefault();
            }
        }

        public void Create(Event model)
        {
            using (var context = new EventManagementEntities())
            {
                context.Events.Add(model);
                context.SaveChanges();
            }
        }

        public void Update(Event model)
        {
            using (var context = new EventManagementEntities())
            {
                var _event = context.Events.Where(m => m.Id == model.Id).FirstOrDefault();
                _event.Poster = model.Poster;
                _event.Name = model.Name;
                _event.Number = model.Number;
                _event.Type = model.Type;
                _event.Tickets = model.Type == 1 ? null : model.Tickets;
                _event.Date = model.Date;
                _event.Venue = model.Venue;
                _event.Description = model.Description;
                _event.ContactEmail = model.ContactEmail;
                _event.ContactName = model.ContactName;
                _event.ContactPhone = model.ContactPhone;
                context.SaveChanges();
            }
        }
    }
}
