namespace GraphQL.API.Schema.Mutations;

public class MessageInputType
{
    public int AuthorId { get; set; }

    public string Message { get; set; }

    public bool IsVisible { get; set; }
}