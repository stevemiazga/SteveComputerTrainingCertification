using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;

namespace Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TrainingContext(serviceProvider.GetRequiredService<DbContextOptions<TrainingContext>>()))
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {

                    command.CommandText = "ResetComputerTrainingCertificationDatabase";
                    command.CommandType = CommandType.StoredProcedure;

                    if (command.Connection.State != ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    command.ExecuteNonQuery();
                }
            }


        }
    }
}
