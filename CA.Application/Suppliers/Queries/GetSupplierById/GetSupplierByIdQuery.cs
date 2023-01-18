using AutoMapper;
using AutoMapper.QueryableExtensions;
using CA.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CA.Application.Suppliers.Queries.GetSupplierById;

public class GetSupplierByIdQuery
{
    public class Query : IRequest<SupplierResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, SupplierResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public Handler(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SupplierResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers
                .ProjectTo<SupplierResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}