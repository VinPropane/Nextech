namespace NexTech.AngularApp.Objects {
  public class Story {
    public Story(int id, string title, string url) {
      Id = id;
      Title = title;
      Url = url;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }

    public override bool Equals(object o) {
      if (o == null || GetType() != o.GetType()) return false;
      Story s = (Story)o;
      return Id == s.Id && Title == s.Title && Url == s.Url;

    }
  }
}
