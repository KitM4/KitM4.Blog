using KitM4.Blog.Domain.Communication.Requests;

using FluentValidation;

namespace KitM4.Blog.Core.Validators.Blog;

public class AddArticleRequestValidator : AbstractValidator<BlogRequests.PublishArticle>
{
}