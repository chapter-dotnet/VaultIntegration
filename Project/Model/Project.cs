namespace VaultAPI.Models
{
  public class Project
  {
    public string Id { get; set; }
    public int YearOfFirstCommit { get; set; }
    public string GitHubLink { get; set; }
  }
}