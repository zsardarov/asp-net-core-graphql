namespace API.GraphQL.Commands
{
    public record UpdateCommandInput
    {
        public int Id { get; init; }
        public string HowTo { get; init; }
        public string CommandLine { get; init; }
        public int PlatformId { get; init; }
    }
}