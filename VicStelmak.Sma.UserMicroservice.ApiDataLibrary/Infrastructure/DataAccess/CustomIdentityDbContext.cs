using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Extensions;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Infrastructure.DataAccess
{
    internal class CustomIdentityDbContext : IdentityDbContext<UserModel>
    {
        public CustomIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserModel>(entity =>
            {
                entity.Property(user => user.FirstName).IsRequired(false);
                entity.Property(user => user.LastName).IsRequired(false);
            });

            builder.SeedInitialData();
            builder.SeedRoles();
        }
    }
}
