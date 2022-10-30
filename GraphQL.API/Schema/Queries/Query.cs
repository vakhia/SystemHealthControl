using GraphQL.API.Data;
using GraphQL.API.Models;

namespace GraphQL.API.Schema.Queries;

public class Query
{
    public IQueryable<Comment> GetComments([Service] DatabaseContext context)
    {
        return context.Comments;
    }
}