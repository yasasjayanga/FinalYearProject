using EventManagement.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models
{
    public class ForumView_Model
    {
        public List<TopicView_Model> Topics { get; set; }
        public List<PostView_Model> Posts { get; set; }
    }

    public class TopicView_Model
    {
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

        public PostView_Model StarterPost { get; set; }
        public bool DisablePosting { get; set; }

        // Subscription
        public bool IsSubscribed { get; set; }

        public virtual AspNetUserView_Model AspNetUser { get; set; }
        public virtual ICollection<PostView_Model> Posts { get; set; }
        public virtual PostView_Model Post { get; set; }
    }

    public class PostView_Model
    {
        public PostView_Model Post { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PostContent { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int Topic_Id { get; set; }
        public Nullable<System.DateTime> DateEdited { get; set; }
        public bool IsSolution { get; set; }
        public Nullable<bool> IsTopicStarter { get; set; }
        public Nullable<bool> Pending { get; set; }
        public virtual AspNetUserView_Model AspNetUser { get; set; }
        public virtual TopicView_Model Topic { get; set; }
        public virtual ICollection<TopicView_Model> Topics { get; set; }
    }

    public class AspNetUserView_Model
    {
        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public Nullable<System.DateTimeOffset> LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<PostView_Model> Posts { get; set; }
        public virtual ICollection<TopicView_Model> Topics { get; set; }
    }

    public class CreateEditTopicView_Model
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(6000)]
        public string Content { get; set; }

        public bool IsSticky { get; set; }

        public bool IsLocked { get; set; }

        [Required]
        public Guid Category { get; set; }

        public string Tags { get; set; }
        
        public bool SubscribeToTopic { get; set; }
        
        // Permissions stuff
        //public CheckCreateTopicPermissions OptionalPermissions { get; set; }
        
        public int Id { get; set; }
        public int TopicId { get; set; }
        public bool IsTopicStarter { get; set; }
    }
}


//using EventManagement.data;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace EventManagement.Models
//{
//    public class ForumView_Model
//    {
//        public List<TopicView_Model> Topics { get; set; }
//        public List<PostView_Model> Posts { get; set; }
//    }

//    public class TopicView_Model
//    {
//        public TopicView_Model Topic { get; set; }
//        public PostView_Model StarterPost { get; set; }

//        public int Id { get; set; }
//        public string Name { get; set; }
//        public System.DateTime CreateDate { get; set; }
//        public string UserId { get; set; }
//        public bool Solved { get; set; }
//        public string Slug { get; set; }
//        public Nullable<int> Views { get; set; }
//        public bool IsSticky { get; set; }
//        public bool IsLocked { get; set; }
//        public Nullable<bool> Pending { get; set; }
//        public Nullable<int> PostId { get; set; }

//        public virtual AspNetUserView_Model AspNetUser { get; set; }
//        public virtual ICollection<PostView_Model> Posts { get; set; }
//        public virtual PostView_Model Post { get; set; }
//    }

//    public class PostView_Model
//    {
//        public PostView_Model Post { get; set; }
//        public string PermaLink { get; set; }
//        public TopicView_Model ParentTopic { get; set; }
//        public bool AllowedToVote { get; set; }
//        public bool MemberHasFavourited { get; set; }

//        public int Id { get; set; }
//        public string UserId { get; set; }
//        public string PostContent { get; set; }
//        public System.DateTime DateCreated { get; set; }
//        public int Topic_Id { get; set; }
//        public Nullable<System.DateTime> DateEdited { get; set; }
//        public bool IsSolution { get; set; }
//        public Nullable<bool> IsTopicStarter { get; set; }
//        public Nullable<bool> Pending { get; set; }
//        public virtual AspNetUserView_Model AspNetUser { get; set; }
//        public virtual TopicView_Model Topic { get; set; }
//        public virtual ICollection<TopicView_Model> Topics { get; set; }
//    }

//    public class AspNetUserView_Model
//    {
//        public string Id { get; set; }
//        public int AccessFailedCount { get; set; }
//        public string ConcurrencyStamp { get; set; }
//        public string Email { get; set; }
//        public bool EmailConfirmed { get; set; }
//        public bool LockoutEnabled { get; set; }
//        public Nullable<System.DateTimeOffset> LockoutEnd { get; set; }
//        public string NormalizedEmail { get; set; }
//        public string NormalizedUserName { get; set; }
//        public string PasswordHash { get; set; }
//        public string PhoneNumber { get; set; }
//        public bool PhoneNumberConfirmed { get; set; }
//        public string SecurityStamp { get; set; }
//        public bool TwoFactorEnabled { get; set; }
//        public string UserName { get; set; }
//        public virtual ICollection<PostView_Model> Posts { get; set; }
//        public virtual ICollection<TopicView_Model> Topics { get; set; }
//    }
//}