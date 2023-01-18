using AutoMapper;
using CA.Domain.Suppliers;
using CA.Infrastructure.Data;
using MediatR;

namespace CA.Application.Suppliers.Commands.CreateSupplier;

public class CreateSupplierCommand
{
    public class Command : IRequest
    {
        public CreateSupplierRequest CreateSupplierRequest { get; set; }
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
            var supplier = _mapper.Map<CreateSupplierRequest, Supplier>(request.CreateSupplierRequest);
            _context.Suppliers.Add(supplier);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}