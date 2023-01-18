using AutoMapper;
using AutoMapper.QueryableExtensions;
using CA.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CA.Application.Medicines.Queries.GetMedicines;

public class GetMedicinesQuery
{
    public class Query : IRequest<List<MedicinesResponse>> {}

    public class Handler : IRequestHandler<Query, List<MedicinesResponse>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public Handler(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MedicinesResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Medicines
                .ProjectTo<MedicinesResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}