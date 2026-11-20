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
    public class DoctorPatientConfiguration : IEntityTypeConfiguration<DoctorPatient>
    {
        public void Configure(EntityTypeBuilder<DoctorPatient> builder)
        {
            builder.HasKey(dp => new { dp.DoctorId, dp.PatientId });
            builder.HasOne(dp => dp.Doctor)
                .WithMany(d => d.DoctorPatients)
                .HasForeignKey(dp => dp.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(dp => dp.Patient)
              .WithMany(d => d.DoctorPatients)
              .HasForeignKey(dp => dp.PatientId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new DoctorPatient { DoctorId = 1, PatientId = 1 },
                new DoctorPatient { DoctorId = 2, PatientId = 2 }
            );
        }
    }
}
