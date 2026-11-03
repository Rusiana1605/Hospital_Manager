using Data_Hospital_Manager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Hospital_Manager.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        { 
            builder.HasData(
                new Doctor
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Carter",
                    Specialty = "Surgery",
                    Email = "john.T.Carter@hospital.com",
                    PhoneNumber = "1234567892",
                    HospitalId = 2
                },
                new Doctor
                {
                    Id = 2,
                    FirstName = "Michael",
                    LastName = "Robinovitch",
                    Specialty = "ER",
                    Email = "robbyrobinovitch@hospital.com",
                    PhoneNumber = "1234567893",
                    HospitalId = 1
                }
            );
        }
    }
}
