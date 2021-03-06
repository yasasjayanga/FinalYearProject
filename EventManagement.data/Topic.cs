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
    
    public partial class Topic
    {
        public Topic()
        {
            this.Posts = new HashSet<Post>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public bool Solved { get; set; }
        public string Slug { get; set; }
        public Nullable<int> Views { get; set; }
        public bool IsSticky { get; set; }
        public bool IsLocked { get; set; }
        public Nullable<bool> Pending { get; set; }
        public Nullable<int> PostId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual Post Post { get; set; }
    }
}
