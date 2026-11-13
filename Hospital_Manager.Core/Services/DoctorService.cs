using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Hospital_Manager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Manager.Core.Services
{
    public class DoctorService:IDoctorService
    {
        private readonly HospitalDbContext context;

        public DoctorService(HospitalDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync(
            string? search,
            string? specialty,
            string? sortOrder)
        {

            //var query1 = context.Doctors
            //    .Include(x => x.Hospital)
            //    .ToListAsync();
            IQueryable<Doctor> query = context.Doctors
                .Include(x => x.Hospital);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.FirstName.Contains(search) || x.FirstName.Contains(search));
            }
            if (!string.IsNullOrEmpty(specialty))
            {
                query = query.Where(t => t.Specialty.Contains(search));
            }
            switch (sortOrder)
            {
                case "name_asc":
                    query = query.OrderBy(h => h.FirstName).ThenBy(d => d.LastName);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(h => h.FirstName).ThenBy(d => d.LastName);
                    break;
                case "specialty":
                    query = query.OrderBy(h => h.Specialty).ThenBy(h => h.FirstName);
                    break;
                default:
                    query = query.OrderBy(h => h.FirstName).ThenBy(d => d.LastName);
                    break;
            }
            return await query.ToListAsync();
        }
        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await context.Doctors.FindAsync(id);
        }
        public async Task<Doctor?> GetDoctorDetailAsync(int id)
        {
            return await context.Doctors
                .Include(x => x.Hospital)
                .Include(x => x.DoctorPatients)
                .ThenInclude(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddDoctorAsync(Doctor d)
        {
            context.Doctors.Add(d);
            await context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor d)
        {
            await context.SaveChangesAsync();
        }
        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await context.Doctors
                .Include(x => x.DoctorPatients)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (doctor != null)
            {
                context.DoctorPatients.RemoveRange(doctor.DoctorPatients);
                context.Doctors.Remove(doctor);
                await context.SaveChangesAsync();
            }
        }
    }
}
