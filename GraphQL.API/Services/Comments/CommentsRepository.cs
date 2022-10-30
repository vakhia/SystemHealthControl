using GraphQL.API.Data;
using GraphQL.API.Models;

namespace GraphQL.API.Services.Comments;

public class CommentsRepository
{
    private readonly DatabaseContext context;

    public CommentsRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task<Comment> Create(Comment comment)
    {
        context.Add(comment);
        await context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment> Update(Comment comment)
    {
        context.Update(comment);
        await context.SaveChangesAsync();

        return comment;
    }

    public async Task<bool> Delete(int id)
    {
        Comment comment = new Comment()
        {
            Id = id
        };
        context.Remove(comment);
        return await context.SaveChangesAsync() > 0;
    }
}