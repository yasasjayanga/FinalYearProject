//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventManagement.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public System.DateTime Dob { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Country { get; set; }
        public string ProfilePhoto { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
