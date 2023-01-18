using AutoMapper;
using CA.Application.Diseases.Commands.CreateDisease;
using CA.Application.Diseases.Commands.UpdateDisease;
using CA.Application.Diseases.Queries.GetDiseaseById;
using CA.Application.Diseases.Queries.GetDiseases;
using CA.Application.Medicines.Commands.CreateMedicine;
using CA.Application.Medicines.Commands.UpdateMedicine;
using CA.Application.Medicines.Queries.GetMedicineById;
using CA.Application.Medicines.Queries.GetMedicines;
using CA.Application.Suppliers.Commands.CreateSupplier;
using CA.Application.Suppliers.Commands.UpdateSupplier;
using CA.Application.Suppliers.Queries.GetSupplierById;
using CA.Application.Suppliers.Queries.GetSuppliers;
using CA.Domain.Diseases;
using CA.Domain.Medicines;
using CA.Domain.Shared;
using CA.Domain.Suppliers;

namespace CA.Application.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Medicine, Medicine>();
        CreateMap<Medicine, MedicinesResponse>();

        CreateMap<MedicineDisease, MedicineDiseaseUnit>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Disease.Id))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Disease.Title))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Disease.Description));
        
        CreateMap<Medicine, MedicineResponse>();
        
        CreateMap<Medicine, CreateMedicineRequest>();
        CreateMap<CreateMedicineRequest, Medicine>();
        CreateMap<Medicine, UpdateMedicineRequest>();
        CreateMap<UpdateMedicineRequest, Medicine>();


        CreateMap<Disease, Disease>();
        CreateMap<Disease, DiseaseResponse>();
        CreateMap<Disease, DiseasesResponse>();
        CreateMap<CreateDiseaseRequest, Disease>();
        CreateMap<Disease, CreateDiseaseRequest>();
        CreateMap<Disease, UpdateDiseaseRequest>();
        CreateMap<UpdateDiseaseRequest, Disease>();
        
        
        CreateMap<Supplier, Supplier>();
        CreateMap<Supplier, SupplierResponse>();
        CreateMap<Supplier, SuppliersResponse>();
        CreateMap<CreateSupplierRequest, Supplier>();
        CreateMap<Supplier, CreateSupplierRequest>();
        CreateMap<Supplier, UpdateSupplierRequest>();
        CreateMap<UpdateSupplierRequest, Supplier>();


    }
}