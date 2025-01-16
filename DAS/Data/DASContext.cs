using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DAS.Models;

namespace DAS.Data
{
    public class DASContext : DbContext
    {
        public DASContext(DbContextOptions<DASContext> options)
            : base(options)
        {
        }

        public DbSet<DAS.Models.Appointment> Appointments { get; set; } = default!;
        public DbSet<DAS.Models.Patient> Patients { get; set; } = default!;
        public DbSet<DAS.Models.Profile> Profiles { get; set; } = default!;

        public DbSet<DAS.Models.DoctorNote> DoctorNotes { get; set; } = default!;
        public DbSet<DAS.Models.ServiceProviders> ServiceProviders { get; set; } = default!;



        // internal void SaveChanges()
        // {
        //     throw new NotImplementedException();
        // }
    }
}
