//using EventManagement.service.Forum;
//using EventManagement.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace EventManagement.Models
//{
//    public class PriorityListViewComponent : ViewComponent
//    {
//        //private readonly ToDoContext db;

//        //public PriorityListViewComponent(ToDoContext context)
//        //{
//        //    db = context;
//        //}

//        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
//        {
//            CategoryService_old categoryService = new CategoryService_old();
//            TopicService_old topicService = new TopicService_old();
//            var allowedCategories = categoryService.GetAllowedCategories();

//            // Get the topics
//            var topics = topicService.GetRecentTopics(allowedCategories);

//            // Get the Topic View Models
//            var topicViewModels = ViewModelMapping.CreateTopicViewModels(topics, allowedCategories);

//            // create the view model
//            var viewModel = new ActiveTopicsViewModel
//            {
//                Topics = topicViewModels
//            };
//            //var items = await GetItemsAsync(maxPriority, isDone);
//            return View(viewModel);
//        }
//        //private Task<List<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
//        //{
//        //    return db.ToDo.Where(x => x.IsDone == isDone &&
//        //                         x.Priority <= maxPriority).ToListAsync();
//        //}
//    }
//}
