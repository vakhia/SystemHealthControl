using AutoMapper;
using CA.Infrastructure.Data;
using MediatR;

namespace CA.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommand
{
    public class Command : IRequest
    {
        public UpdateSupplierRequest UpdateSupplierRequest { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public Handler(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FindAsync(request.UpdateSupplierRequest.Id);
            _mapper.Map(request.UpdateSupplierRequest, supplier);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}