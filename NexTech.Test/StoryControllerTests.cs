using NexTech.AngularApp.Controllers;
using NexTech.AngularApp.Objects;
using NexTech.AngularApp.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;

namespace NexTech.Test {
  public class Tests {
    private static Mock<IApiService> _apiService = new Mock<IApiService>();
    private StoryController _storyController = new StoryController(_apiService.Object);
    private StoryResults _results; 

    [SetUp]
    public void Setup() {
      
    }

    public void SetupFakeData() {
      _results = new StoryResults(new List<Story> {
        new Story(123, "Cool Title", "http://nerve.com"),
        new Story(456, "Very Cool Title", "http://hair.com"),
        new Story(789, "Somewhat Cool Title", "http://decorative.com"),
        new Story(444, "Not Really Cool Title", "http://cause.com"),
        new Story(911, "Bad Title", "http://property.com"),
        new Story(654, "Very Bad Title", "http://mess.com"),
        new Story(111, "Okay Title", "http://resort.com"),
        new Story(109, "Terrible Title", "http://manage.com"),
        new Story(787, "Excellent Title", "http://satellite.com"),
        new Story(274, "A Title", "http://literature.com"),
        new Story(658, "The Title", "http://mist.com"),
        new Story(117, "Title 2: Title Harder", "http://nextech.com")
      }, 0, 12, 12);

      _apiService.Setup(x => x.GetStory(123)).Returns(GetStory(123));
      _apiService.Setup(x => x.GetStory(456)).Returns(GetStory(456));
      _apiService.Setup(x => x.GetStory(789)).Returns(GetStory(789));
      _apiService.Setup(x => x.GetStory(444)).Returns(GetStory(444));
      _apiService.Setup(x => x.GetStory(911)).Returns(GetStory(911));
      _apiService.Setup(x => x.GetStory(654)).Returns(GetStory(654));
      _apiService.Setup(x => x.GetStory(111)).Returns(GetStory(111));
      _apiService.Setup(x => x.GetStory(109)).Returns(GetStory(109));
      _apiService.Setup(x => x.GetStory(787)).Returns(GetStory(787));
      _apiService.Setup(x => x.GetStory(274)).Returns(GetStory(274));
      _apiService.Setup(x => x.GetStory(658)).Returns(GetStory(658));
      _apiService.Setup(x => x.GetStory(117)).Returns(GetStory(117));
    }
    private static async Task<List<int>> GetTwelveIds() {
      return await Task.FromResult(new List<int> { 123, 456, 789, 444, 911, 654, 111, 109, 787, 274, 658, 117 });
    }

    private static async Task<List<int>> GetNoIds() {
      return await Task.FromResult(new List<int>());
    }

    private async Task<Story> GetStory(int id) {
      return id switch {
        123 => await Task.FromResult(new Story(123, "Cool Title", "http://nerve.com")),
        456 => await Task.FromResult(new Story(456, "Very Cool Title", "http://hair.com")),
        789 => await Task.FromResult(new Story(789, "Somewhat Cool Title", "http://decorative.com")),
        444 => await Task.FromResult(new Story(444, "Not Really Cool Title", "http://cause.com")),
        911 => await Task.FromResult(new Story(911, "Bad Title", "http://property.com")),
        654 => await Task.FromResult(new Story(654, "Very Bad Title", "http://mess.com")),
        111 => await Task.FromResult(new Story(111, "Okay Title", "http://resort.com")),
        109 => await Task.FromResult(new Story(109, "Terrible Title", "http://manage.com")),
        787 => await Task.FromResult(new Story(787, "Excellent Title", "http://satellite.com")),
        274 => await Task.FromResult(new Story(274, "A Title", "http://literature.com")),
        658 => await Task.FromResult(new Story(658, "The Title", "http://mist.com")),
        117 => await Task.FromResult(new Story(117, "Title 2: Title Harder", "http://nextech.com")),
        _ => null,
      };
    }

    [Test]
    public async Task ApiReturnsNoStories() {
      _apiService.Setup(x => x.GetNewStoryIds()).Returns(GetNoIds());
      var newStories = await _storyController.GetNewStoriesAsync();
      var noStories = new StoryResults(new List<Story>(), 0, 0, 0);
      Assert.IsTrue(newStories.Equals(noStories));
    }

    [Test]
    public async Task ApiReturnsAllStories() {
      _apiService.Setup(x => x.GetNewStoryIds()).Returns(GetTwelveIds());
      SetupFakeData();
      var newStories = await _storyController.GetNewStoriesAsync();
      Assert.IsTrue(newStories.Equals(_results));
    }

    [Test]
    public async Task ShowFirstAndSecondOfTwelveStories() {
      SetupFakeData();
      _apiService.Setup(x => x.GetNewStoryIds()).Returns(GetTwelveIds());
      await _storyController.GetNewStoriesAsync();
      var results = await _storyController.GetStories(0, 2, string.Empty);
      var expectedResults = new StoryResults(new List<Story>(_results.Stories.GetRange(0, 2)), 0, 2, 12);
      Assert.IsTrue(expectedResults.Equals(results));
    }

    [Test]
    public async Task ShowLastTwoStoriesWhenPageSizeIsFive() {
      SetupFakeData();
      _apiService.Setup(x => x.GetNewStoryIds()).Returns(GetTwelveIds());
      await _storyController.GetNewStoriesAsync();
      var results = await _storyController.GetStories(2, 5, string.Empty);
      var expectedResults = new StoryResults(new List<Story>(_results.Stories.GetRange(10, 2)), 2, 5, 12);
      Assert.IsTrue(expectedResults.Equals(results));
    }
  }
}