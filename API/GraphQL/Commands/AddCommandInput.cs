namespace API.GraphQL.Commands
{
    public record AddCommandInput
    {
        public string HowTo { get; init; }
        public string CommandLine { get; init; }
        public int PlatformId { get; init; }
    }
}