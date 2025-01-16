using Microsoft.EntityFrameworkCore;
using DAS.Models;

namespace DAS.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new DASContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<DASContext>>()
            );

        if (context == null || context.Appointments == null || context.Profiles == null || context.Patients == null)
        {
            throw new NullReferenceException("Null DASContext or Appointment DbSet");
        }

        if (context.Appointments.Any())
        {
            Console.WriteLine("Found it!!!!!!!!!!!!");
            return;
        }


        //context.SaveChanges();












    }


}