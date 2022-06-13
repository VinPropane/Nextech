using Microsoft.AspNetCore.Mvc;
using NexTech.AngularApp.Objects;
using NexTech.AngularApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexTech.AngularApp.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class StoryController : IStoryController {
    internal static List<Story> _storyCache;
    private static IApiService _apiService;

    public StoryController(IApiService apiService) {
      _apiService = apiService;
    }

    [HttpGet]
    [Route("GetNewStoriesAsync")]
    public async Task<StoryResults> GetNewStoriesAsync() {
      _storyCache = new List<Story>();
      var newStoryIds = await _apiService.GetNewStoryIds();

      foreach (var id in newStoryIds) {
        _storyCache.Add(await _apiService.GetStory(id));
      }
      var storyCount = _storyCache.Count < 50 ? _storyCache.Count : 50;
      return new StoryResults(_storyCache.GetRange(0, storyCount), 0, storyCount, _storyCache.Count);
    }

    [HttpGet]
    [Route("GetStories/{pageIndex}/{pageSize}")]
    public async Task<StoryResults> GetStories(int pageIndex, int pageSize, [FromQuery] string searchFilter) {
      var filteredStories = new List<Story>();

      if (!string.IsNullOrEmpty(searchFilter)) {
        filteredStories.AddRange(_storyCache.Where(s => s.Title.ToLower().Contains(searchFilter)));
      } else {
        filteredStories = _storyCache;
      }
      if (pageSize > filteredStories.Count) pageSize = filteredStories.Count;
      var storiesToFetch = pageSize;
      if (pageSize * pageIndex + pageSize > filteredStories.Count) {
        storiesToFetch = filteredStories.Count % pageSize;
      }
      return new StoryResults(new List<Story>(filteredStories.GetRange(pageIndex * pageSize, storiesToFetch)), pageIndex, pageSize, filteredStories.Count);
    }
  }
}
