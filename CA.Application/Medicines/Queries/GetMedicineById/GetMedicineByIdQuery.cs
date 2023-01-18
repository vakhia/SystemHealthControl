using AutoMapper;
using AutoMapper.QueryableExtensions;
using CA.Domain.Medicines;
using CA.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CA.Application.Medicines.Queries.GetMedicineById;

public class GetMedicineByIdQuery
{
    public class Query : IRequest<MedicineResponse>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, MedicineResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public Handler(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MedicineResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Medicines
                .ProjectTo<MedicineResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}