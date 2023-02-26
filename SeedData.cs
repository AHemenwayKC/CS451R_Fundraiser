﻿using Microsoft.EntityFrameworkCore;
using CS451R_Fundraiser.Models;
using CS451R_Fundraiser.Data;


namespace CS451R_Fundraiser
{
    public static class SeedData
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CS451R_FundraiserContext>();
            context.Database.EnsureCreated();
            AddFundraisers(context);
        }

        private static void AddFundraisers(CS451R_FundraiserContext context)
        {
            //var fundraiser = context.Fundraiser.FirstOrDefault();
            //if (fundraiser != null) return;

            context.Fundraiser.Add(new Fundraiser
            {
                Title = "Help little Timmy get new legs",
                PostDate = DateTime.Parse("2023-2-12"),
                Category = "Prosthetics",
                Goal = 100000
            });

            context.Fundraiser.Add(new Fundraiser
            {
                Title = "Help me buy a new BMW",
                PostDate = DateTime.Parse("2020-3-13"),
                Category = "Transportation",
                Goal = 85000
            });

            context.Fundraiser.Add(new Fundraiser
            {
                Title = "My mom needs help paying for college",
                PostDate = DateTime.Parse("2021-5-23"),
                Category = "Education",
                Goal = 25000
            });

            context.SaveChanges();
        }
    }
}





//    public static class SeedData
//    {
//        public static void Initialize(IServiceProvider serviceProvider)
//        {
//            using (var context = new CS451R_FundraiserContext(
//                serviceProvider.GetRequiredService<
//                    DbContextOptions<CS451R_FundraiserContext>>()))
//            {
//                // Look for any movies.
//                if (context.Fundraiser.Any())
//                {
//                    return;   // DB has been seeded
//                }
//                context.Fundraiser.AddRange(
//                    new Fundraiser
//                    {
//                        Id = 1234,
//                        Title = "Help little Timmy get new legs",
//                        PostDate = DateTime.Parse("2023-2-12"),
//                        Category = "Prosthetics",
//                        Goal = 100000
//                    },
//                    new Fundraiser
//                    {
//                        Id = 1235,
//                        Title = "Help me buy a new BMW",
//                        PostDate = DateTime.Parse("2020-3-13"),
//                        Category = "Transportation",
//                        Goal = 85000
//                    },
//                    new Fundraiser
//                    {
//                        Id = 1236,
//                        Title = "My mom needs help paying for college",
//                        PostDate = DateTime.Parse("2021-5-23"),
//                        Category = "Education",
//                        Goal = 25000
//                    },
//                    new Fundraiser
//                    {
//                        Id = 1237,
//                        Title = "Support cancer patients",
//                        PostDate = DateTime.Parse("2019-4-15"),
//                        Category = "Medical",
//                        Goal = 250000
//                    }
//                );
//                context.SaveChanges();
//            }
//        }
//    }
//}