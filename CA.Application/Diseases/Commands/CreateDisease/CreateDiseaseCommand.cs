using AutoMapper;
using CA.Domain.Diseases;
using CA.Infrastructure.Data;
using MediatR;

namespace CA.Application.Diseases.Commands.CreateDisease;

public class CreateDiseaseCommand
{
    public class Command : IRequest
    {
        public CreateDiseaseRequest CreateDiseaseRequest { get; set; }
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
            var disease = _mapper.Map<CreateDiseaseRequest, Disease>(request.CreateDiseaseRequest);
            _context.Diseases.Add(disease);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}