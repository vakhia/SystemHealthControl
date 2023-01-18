using AutoMapper;
using AutoMapper.QueryableExtensions;
using CA.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CA.Application.Diseases.Queries.GetDiseaseById;

public class GetDiseaseByIdQuery
{
    public class Query : IRequest<DiseaseResponse>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, DiseaseResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public Handler(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DiseaseResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Diseases
                .ProjectTo<DiseaseResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}