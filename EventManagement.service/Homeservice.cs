using EventManagement.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.service
{
    public class HomeService
    {
        public void ContactUs(Contact_Us model)
        {
            using (var context = new EventManagementEntities())
            {
                context.Contact_Us.Add(model);
                context.SaveChanges();
            }
        }
    }
}
