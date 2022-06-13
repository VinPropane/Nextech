using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NexTech.AngularApp.Objects;

namespace NexTech.AngularApp.Services {
  public class ApiService : IApiService {
    const string BASE_URL = "https://hacker-news.firebaseio.com";

    public async Task<List<int>> GetNewStoryIds() {
      var endpoint = BASE_URL + "/v0/newstories.json?print=pretty";
      var client = new HttpClient();
      var res = await client.GetAsync(endpoint);
      return JsonConvert.DeserializeObject<List<int>>(res.Content.ReadAsStringAsync().Result);
    }

    public async Task<Story> GetStory(int id) {
      var endpoint = BASE_URL + $"/v0/item/{id}.json?print=pretty";
      var client = new HttpClient();
      var res = await client.GetAsync(endpoint);
      return JsonConvert.DeserializeObject<Story>(res.Content.ReadAsStringAsync().Result);
    }
  }
}
