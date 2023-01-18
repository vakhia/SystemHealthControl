using CA.Infrastructure.Data;
using MediatR;

namespace CA.Application.Diseases.Commands.DeleteDisease;

public class DeleteDiseaseCommand
{
    public class Command : IRequest
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DatabaseContext _context;

        public Handler(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var disease = await _context.Diseases.FindAsync(request.Id);
            _context.Remove(disease);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}