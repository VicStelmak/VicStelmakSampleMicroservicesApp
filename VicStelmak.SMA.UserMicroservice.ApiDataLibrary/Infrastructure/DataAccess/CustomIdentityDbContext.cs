using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Extensions;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Infrastructure.DataAccess
{
    internal class CustomIdentityDbContext : IdentityDbContext<UserModel>
    {
        public CustomIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SeedInitialData();
            builder.SeedRoles();
        }
    }
}
