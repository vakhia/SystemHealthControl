using AutoMapper;
using CA.Infrastructure.Data;
using MediatR;

namespace CA.Application.Diseases.Commands.UpdateDisease;

public class UpdateDiseaseCommand
{
    public class Command : IRequest
    {
        public UpdateDiseaseRequest UpdateDiseaseRequest { get; set; }
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
            var disease = await _context.Diseases.FindAsync(request.UpdateDiseaseRequest.Id);
            _mapper.Map(request.UpdateDiseaseRequest, disease);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}