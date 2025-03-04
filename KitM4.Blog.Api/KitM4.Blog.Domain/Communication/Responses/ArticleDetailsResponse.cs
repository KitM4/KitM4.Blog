namespace KitM4.Blog.Domain.Communication.Responses;

public class ArticleDetailsResponse
{
    public required string Category { get; set; }

    public required List<string> ArticleTitles { get; set; }

    public required int RatesCount { get; set; }
}