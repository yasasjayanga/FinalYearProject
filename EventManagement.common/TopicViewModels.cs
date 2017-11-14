using EventManagement.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models
{
    public class CreateTopicButtonViewModel
    {
        public AspNetUser LoggedOnUser { get; set; }
        public bool UserCanPostTopics { get; set; }
    }

    public class TopicViewModel
    {
        public Topic Topic { get; set; }
        
        // Post Stuff
        public PostViewModel StarterPost { get; set; }
        public List<PostViewModel> Posts { get; set; }
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

    public class ActiveTopicsViewModel
    {
        public List<TopicViewModel> Topics { get; set; }
    }

    public class PostedInViewModel
    {
        public List<TopicViewModel> Topics { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
    }

    public class HotTopicsViewModel
    {
        public List<TopicViewModel> Topics { get; set; }
    }

    public class TagTopicsViewModel
    {
        public List<TopicViewModel> Topics { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
        public string Tag { get; set; }
    }

    public class CheckCreateTopicPermissions
    {
        public bool CanUploadFiles { get; set; }
        public bool CanStickyTopic { get; set; }
        public bool CanLockTopic { get; set; }
    }

    public class CreateEditTopicViewModel
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

        //public List<SelectListItem> Categories { get; set; }

        public bool SubscribeToTopic { get; set; }

        //public HttpPostedFileBase[] Files { get; set; }

        // Permissions stuff
        
        public int Id { get; set; }
        public int TopicId { get; set; }
        public bool IsTopicStarter { get; set; }
    }

    public class GetMorePostsViewModel
    {
        public Guid TopicId { get; set; }
        public int PageIndex { get; set; }
        public string Order { get; set; }
    }

    public class PollViewModel
    {
        public bool UserHasAlreadyVoted { get; set; }
        public int TotalVotesInPoll { get; set; }
        public bool UserAllowedToVote { get; set; }
    }

    public class UpdatePollViewModel
    {
        public Guid PollId { get; set; }
        public Guid AnswerId { get; set; }
    }

    public class MoveTopicViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class NotifyNewTopicViewModel
    {
        public Guid CategoryId { get; set; }
    }
}