using Microsoft.AspNetCore.Identity;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.UserMicroservice.Tests
{
    internal static class UserFixture
    {
        internal static List<UserModel> CreateListOfUserModels()
        {
            var passwordHasher = new PasswordHasher<UserModel>();

            return new List<UserModel>
            {
                new UserModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = "test@email.com",
                    EmailConfirmed = false,
                    FirstName = "Testor",
                    LastName = "Testorson",
                    LockoutEnabled = false,
                    NormalizedEmail = "TEST@EMAIL.COM",
                    NormalizedUserName = "TEST@EMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "A@12345b"),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "test@email.com"
                },
                new UserModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = "test2@email.com",
                    EmailConfirmed = false,
                    FirstName = "Testors",
                    LastName = "Testorsons",
                    LockoutEnabled = false,
                    NormalizedEmail = "TEST2@EMAIL.COM",
                    NormalizedUserName = "TEST2@EMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "A@12345b"),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "test2@email.com"
                },
                new UserModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = "test3@email.com",
                    EmailConfirmed = false,
                    FirstName = "Testorsen",
                    LastName = "Testorsen",
                    LockoutEnabled = false,
                    NormalizedEmail = "TEST3@EMAIL.COM",
                    NormalizedUserName = "TEST3@EMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "A@12345b"),
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "test3@email.com"
                }
            };
        }

        internal static List<string> CreateListOfUserRoles()
        {
            return new List<string>
            {
                "Administrator",
                "Customer",
                "User"
            };
        }

        internal static UpdateUserRequest CreateUpdateUserRequest()
        {
            return new UpdateUserRequest("test@email.com", "Testor", "Testorson");
        }

        internal static UserModel CreateUserModel(string userId)
        {
            var passwordHasher = new PasswordHasher<UserModel>();

            return new UserModel
            {
                Id = userId,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Email = "test@email.com",
                EmailConfirmed = false,
                FirstName = "Testor",
                LastName = "Testorson",
                LockoutEnabled = false,
                NormalizedEmail = "TEST@EMAIL.COM",
                NormalizedUserName = "TEST@EMAIL.COM",
                PasswordHash = passwordHasher.HashPassword(null, "A@12345b"),
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "test@email.com"
            };
        }

        internal static CreateUserRequest MakeCreateUserRequest()
        {
            return new CreateUserRequest(
                "test@email.com", 
                "Testor", 
                "Testorson", 
                "A@12345b", 
                "A@12345b");
        }
    }
}
