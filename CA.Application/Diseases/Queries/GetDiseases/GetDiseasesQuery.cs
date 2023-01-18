using AutoMapper;
using AutoMapper.QueryableExtensions;
using CA.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CA.Application.Diseases.Queries.GetDiseases;

public class GetDiseasesQuery
{
    public class Query : IRequest<List<DiseasesResponse>> {}

    public class Handler : IRequestHandler<Query, List<DiseasesResponse>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public Handler(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DiseasesResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Diseases
                .ProjectTo<DiseasesResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}