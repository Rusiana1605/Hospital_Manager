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
            builder.Property(h => h.FirstName)
               .IsRequired()
               .HasMaxLength(30);
            builder.Property(h => h.LastName)
               .IsRequired()
               .HasMaxLength(30);
            builder.Property(h => h.Specialty)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(h => h.Email)
                .IsRequired();
            builder.Property(h => h.PhoneNumber)
                .IsRequired();
            builder.HasOne(d => d.Hospital)
                .WithMany(h => h.Doctors)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);

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
