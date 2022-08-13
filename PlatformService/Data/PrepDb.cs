using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data");

                context.Platforms.AddRange(
                    new Platform { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform { Name = "Azure", Publisher = "Microsoft", Cost = "Free" },
                    new Platform { Name = "Visio", Publisher = "Microsoft", Cost = "Free" },
                    new Platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" },
                    new Platform { Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have data");
            }
        }
    }
}