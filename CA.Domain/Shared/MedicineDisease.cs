using CA.Domain.Diseases;
using CA.Domain.Medicines;

namespace CA.Domain.Shared;

public class MedicineDisease
{
    public Guid MedicineId { get; set; }

    public Medicine Medicine { get; set; }

    public Guid DiseaseId { get; set; }

    public Disease Disease { get; set; }
}