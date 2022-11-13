using EntityFramework.DAL.Data;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.DAL.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DatabaseContext databaseContext;

    public AppointmentRepository(DatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public async Task<Appointment> GetAppointmentByIdAsync(int id)
    {
        return await databaseContext.Appointments.FindAsync(id);
    }

    public async Task<IReadOnlyList<Appointment>> GetAppointmentsAsync()
    {
        return await databaseContext.Appointments.ToListAsync();
    }
}