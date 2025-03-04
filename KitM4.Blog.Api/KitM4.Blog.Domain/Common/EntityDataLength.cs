namespace KitM4.Blog.Domain.Common;

public static class EntityDataLength
{
    public const int MinNameLength = 3;

    public const int MaxNameLength = 64;

    public const int MaxTitleLength = 250;

    public const int MaxBioLength = 1000;

    public const int MinPasswordLength = 8;

    public const int MaxPasswordLength = 128;

    public const int MaxHashLength = 256;

    public const int MaxUrlLength = 512;

    public const int MaxCommentLength = 1500;
}