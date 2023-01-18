using AutoMapper;
using AutoMapper.QueryableExtensions;
using CA.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CA.Application.Suppliers.Queries.GetSuppliers;

public class GetSuppliersQuery
{
    public class Query : IRequest<List<SuppliersResponse>>
    {
    }

    public class Handler : IRequestHandler<Query, List<SuppliersResponse>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public Handler(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SuppliersResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers
                .ProjectTo<SuppliersResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}