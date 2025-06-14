﻿using InforceUrlShortener.Domain.Constants;
using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InforceUrlShortener.Infrastructure.Seeders
{
    public class UrlShortenerSeeder(
        InforceUrlShortenerDbContext dbContext) : IUrlShortenerSeeder
    {
        public async Task SeedAsync()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(GetRoles());
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.AlgorithmDescription.Any())
                {
                    dbContext.AlgorithmDescription.Add(new AlgorithmDescription
                    {
                        Id = AlgorithmDescription.SingletonId,
                        Description = "init desc of algorithm",
                        LastUpdated = DateTime.Now,
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<IdentityRole> GetRoles()
        {
            var roles = new List<IdentityRole>()
            {
                new(UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            };
            return roles;
        }
    }
}
