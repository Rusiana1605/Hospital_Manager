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
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasData(
                new Hospital
                {
                    Id = 1,
                    Name = "The Ptt",
                    Address = "Pitt Strees 12",
                    Email = "pittsburg@gmail.com",
                    PhoneNumber = "1234567890",
                    BedsCount = 100,
                    CreatedOn = new DateTime(2025,1,1),
                    IsDeleted = false
                },
                 new Hospital
                 {
                     Id = 2,
                     Name = "County Hospital",
                     Address = "Dock Street 23",
                     Email = "ChicagoHospitals@gmail.com",
                     PhoneNumber = "1234567891",
                     BedsCount = 200,
                     CreatedOn = new DateTime(1994, 1, 1),
                     IsDeleted = false
                 }
                );
        }
    }
}
