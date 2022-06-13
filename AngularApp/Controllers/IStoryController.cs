using Microsoft.AspNetCore.Mvc;
using NexTech.AngularApp.Objects;
using System.Threading.Tasks;

namespace NexTech.AngularApp.Controllers {
  public interface IStoryController {
    Task<StoryResults> GetNewStoriesAsync();
    Task<StoryResults> GetStories(int pageIndex, int pageSize, [FromQuery] string searchFilter);
  }
}