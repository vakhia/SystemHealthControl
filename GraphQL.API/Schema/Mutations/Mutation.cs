using GraphQL.API.Models;
using GraphQL.API.Services.Comments;

namespace GraphQL.API.Schema.Mutations;

public class Mutation
{
    private readonly CommentsRepository CommentsRepository;

    public Mutation(CommentsRepository commentsRepository)
    {
        CommentsRepository = commentsRepository;
    }

    public async Task<Comment> CreateMessage(MessageInputType messageInputType)
    {
        Comment comment = new Comment()
        {
            AuthorId = messageInputType.AuthorId,
            Message = messageInputType.Message,
            IsVisible = messageInputType.IsVisible,
            CreatedAt = DateTime.Now
        };

        comment = await CommentsRepository.Create(comment);
        return comment;
    }

    public async Task<Comment> UpdateMessage(int commentId, MessageInputType messageInputType)
    {
        Comment comment = new Comment()
        {
            Id = commentId,
            AuthorId = messageInputType.AuthorId,
            Message = messageInputType.Message,
            IsVisible = messageInputType.IsVisible,
            UpdatedAt = DateTime.Now
        };

        comment = await CommentsRepository.Update(comment);
        return comment;
    }

    public async Task<bool> DeleteMessage(int commentId)
    {
        return await CommentsRepository.Delete(commentId);
    }
}