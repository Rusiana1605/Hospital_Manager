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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(h => h.FirstName)
               .IsRequired()
               .HasMaxLength(30);
            builder.Property(h => h.LastName)
               .IsRequired()
               .HasMaxLength(30);
            builder.Property(h => h.Email)
                .IsRequired();
            builder.Property(h => h.PhoneNumber)
                .IsRequired();
            builder.HasData(
                new Patient
                {
                    Id = 1,
                    FirstName = "Alice",
                    LastName = "Truman",
                    Email = "alicaT@gmail.com",
                    PhoneNumber = "1234568892",
                },
                new Patient
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Email = "bobby@gmail.com",
                    PhoneNumber = "1224568892",
                }
            );
        }
    }
}
