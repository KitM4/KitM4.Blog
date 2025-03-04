namespace KitM4.Blog.Domain.Communication.Requests;

public class BlogRequests
{
    public record PublishArticle(string Title, string Category, string MarkdownContent, string[] Tags);
}