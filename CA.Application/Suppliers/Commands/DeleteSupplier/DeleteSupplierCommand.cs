using CA.Infrastructure.Data;
using MediatR;

namespace CA.Application.Suppliers.Commands.DeleteSupplier;

public class DeleteSupplierCommand
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
            var supplier = await _context.Suppliers.FindAsync(request.Id);
            _context.Remove(supplier);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}