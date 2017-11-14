using EventManagement.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models
{
    public class CreateAjaxPostViewModel
    {
        [StringLength(6000)]
        public string PostContent { get; set; }
        public int Topic { get; set; }
        public bool DisablePosting { get; set; }
    }

    public class ShowMorePostsViewModel
    {
        public List<PostViewModel> Posts { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
        public Topic Topic { get; set; }
    }

    //public class PostViewModel
    //{
    //    public Post Post { get; set; }
    //    public string PermaLink { get; set; }
    //    public Topic ParentTopic { get; set; }
    //    public bool AllowedToVote { get; set; }
    //    public bool MemberHasFavourited { get; set; }
    //}

    public class ReportPostViewModel
    {
        public Guid PostId { get; set; }
        public string PostCreatorUsername { get; set; }
        
        [Required]
        public string Reason { get; set; }
    }
}