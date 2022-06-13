using NexTech.AngularApp.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexTech.AngularApp.Services {
  public interface IApiService {
    Task<List<int>> GetNewStoryIds();
    Task<Story> GetStory(int id);
  }
}