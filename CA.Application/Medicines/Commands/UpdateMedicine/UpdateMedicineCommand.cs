using AutoMapper;
using CA.Domain.Medicines;
using CA.Domain.Shared;
using CA.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CA.Application.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommand
{
    public class Command : IRequest
    {
        public UpdateMedicineRequest UpdateMedicineRequest { get; set; }
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
            var medicine = await _context.Medicines.FindAsync(request.UpdateMedicineRequest.Id);
            var diseasesList = await _context.MedicineDiseases
                .Where(md => md.MedicineId == medicine.Id)
                .ToListAsync();
            _context.RemoveRange(diseasesList);
            var supplier = await _context.Suppliers.FindAsync(request.UpdateMedicineRequest.SupplierId);
            var updatedMedicine = _mapper.Map(request.UpdateMedicineRequest, medicine);
            updatedMedicine.Supplier = supplier;
            if (request.UpdateMedicineRequest.DiseasesIds.Count > 0)
            {
                foreach (var guid in request.UpdateMedicineRequest.DiseasesIds)
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

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}