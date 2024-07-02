using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void SeedInitialData(this ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<UserModel>();

            builder.Entity<UserModel>().HasData(new UserModel
            {
                Id = "7f335875-a73d-3773-a7c7-937a53fd7330",
                Email = "funky@email.com",
                EmailConfirmed = true,
                FirstName = "Funky",
                LastName = "Funkterson",
                LockoutEnabled = false,
                NormalizedEmail = "FUNKY@EMAIL.COM",
                NormalizedUserName = "FUNKY@EMAIL.COM",
                PasswordHash = passwordHasher.HashPassword(null, "A@12345b"),
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "funky@email.com"
            });
        }

        internal static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            });
        }
    }
}
