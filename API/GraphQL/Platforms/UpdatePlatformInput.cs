namespace API.GraphQL.Platforms
{
    public record UpdatePlatformInput
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}