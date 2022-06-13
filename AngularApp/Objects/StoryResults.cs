using System;
using System.Collections.Generic;
using System.Linq;

namespace NexTech.AngularApp.Objects {
  public class StoryResults {
    public StoryResults() { }
    public StoryResults(List<Story> stories, int pageIndex, int pageSize, int storyCount) {
      Stories = stories;
      PageIndex = pageIndex;
      PageSize = pageSize;
      StoryCount = storyCount;
    }

    public List<Story> Stories { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int StoryCount { get; set; }

    public override bool Equals(Object o) {
      if (o == null || GetType() != o.GetType()) return false;
      StoryResults s = (StoryResults)o;
      return s.Stories.All(Stories.Contains) &&
             s.Stories.Count == Stories.Count &&
             s.PageIndex == PageIndex &&
             s.PageSize == PageSize &&
             s.StoryCount == StoryCount;
    }
  }
}
