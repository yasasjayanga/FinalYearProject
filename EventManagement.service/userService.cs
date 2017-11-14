using EventManagement.data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.service
{
    public class UsersService
    {
        public User GetUserByUserName(string userName)
        {
            using (var context = new EventManagementEntities())
            {
                var users = (from u in context.Users
                            join a in context.AspNetUsers on u.UserId equals a.Id
                            where a.UserName == userName
                            select u).FirstOrDefault();
                return users;
            }
        }

        public User getUser(string aspnetId)
        {
            using (var context = new EventManagementEntities())
            {
                return context.Users.Where(m => m.UserId == aspnetId).FirstOrDefault();
            }
        }
        public User getUser(int Id)
        {
            using (var context = new EventManagementEntities())
            {
                return context.Users.Where(m => m.Id == Id).FirstOrDefault();
            }
        }
        public void SaveUser(User model)
        { 
            using (var context = new EventManagementEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
        }

        public void UpdateUser(User model)
        {
            using (var context = new EventManagementEntities())
            {
                var user = context.Users.Where(m => m.UserId == model.UserId).FirstOrDefault();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Dob = model.Dob;
                user.ContactNumber = model.ContactNumber;
                user.Address = model.Address;
                user.City = model.City;
                user.Country = model.Country;
                user.ProfilePhoto = model.ProfilePhoto;
                context.SaveChanges();
            }
        }
    }
}
