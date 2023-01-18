using AutoMapper;
using CA.Domain.Medicines;
using CA.Domain.Shared;
using CA.Infrastructure.Data;
using MediatR;

namespace CA.Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommand
{
    public class Command : IRequest
    {
        public CreateMedicineRequest CreateMedicineRequest { get; set; }
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
            var medicine = _mapper.Map<CreateMedicineRequest, Medicine>(request.CreateMedicineRequest);
            if (request.CreateMedicineRequest.DiseasesIds.Count > 0)
            {
                foreach (var guid in request.CreateMedicineRequest.DiseasesIds)
                {
                    var disease = await _context.Diseases.FindAsync(guid);
                    var diseasesMedicines = new MedicineDisease()
                    {
                        Medicine = medicine,
                        Disease = disease,
                    };

                    medicine.Diseases.Add(diseasesMedicines);
                }
            }
            var supplier = await _context.Suppliers.FindAsync(request.CreateMedicineRequest.SupplierId);
            medicine.Supplier = supplier;
            _context.Medicines.Add(medicine);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}