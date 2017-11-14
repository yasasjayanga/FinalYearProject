using EventManagement.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models
{
    public class TopicDetailView_Model
    {
        public TopicDetail_Model Topic { get; set; }

        // Post Stuff
        public PostDetailViewModel StarterPost { get; set; }
        public List<PostDetailViewModel> Posts { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
        public string LastPostPermaLink { get; set; }

        // Permissions
        public bool DisablePosting { get; set; }

        // Subscription
        public bool IsSubscribed { get; set; }

        // Votes
        public int VotesUp { get; set; }
        public int VotesDown { get; set; }

        // Quote
        public string QuotedPost { get; set; }

        // Stats
        public int Answers { get; set; }
        public int Views { get; set; }
    }

    public class PostDetailViewModel
    {
        public PostDetail_Model Post { get; set; }
        public string PermaLink { get; set; }
        public TopicDetail_Model ParentTopic { get; set; }
        public bool AllowedToVote { get; set; }
        public bool MemberHasFavourited { get; set; }
    }
    public class TopicDetail_Model
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

        public virtual AspNetUserView_Model AspNetUser { get; set; }
        public virtual ICollection<PostDetail_Model> Posts { get; set; }
        public virtual PostDetail_Model Post { get; set; }
    }

    public class PostDetail_Model
    {
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
        public virtual TopicDetail_Model Topic { get; set; }
        public virtual ICollection<TopicDetail_Model> Topics { get; set; }
    }
}

