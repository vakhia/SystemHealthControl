using CA.Infrastructure.Data;
using MediatR;

namespace CA.Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommand
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
            var medicine = await _context.Medicines.FindAsync(request.Id);
            _context.Remove(medicine);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}