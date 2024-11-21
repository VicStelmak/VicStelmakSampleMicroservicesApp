using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Responses;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Services;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.UserMicroservice.Tests
{
    public class UserServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly UserService _testedService;
        private readonly Mock<UserManager<UserModel>> _userManagerMock; 

        public UserServiceTests()
        {
            var testJwtSettings = new Dictionary<string, string> 
            {
                { "JwtSettings:secretKey", "AstonishinglyComplicatedToGuessMegaSecretTestPrivateKey" },
                { "JwtSettings:validIssuer", "testCertificationAuthority" },
                { "JwtSettings:validAudience", "http://testhost:7211"},
                { "JwtSettings:expiryInMinutes", "5" }
            }; 

            _configuration = new ConfigurationBuilder().AddInMemoryCollection(testJwtSettings).Build();

            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object);

            _userManagerMock = new Mock<UserManager<UserModel>>(
                new Mock<IUserStore<UserModel>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<UserModel>>().Object,
                new IUserValidator<UserModel>[0],
                new IPasswordValidator<UserModel>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<UserModel>>>().Object);

            _testedService = new UserService(_configuration, _roleManagerMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task AddRoleToUserAsync_WhenUserAndRoleExist_ShouldCallIsInRoleAsyncOnce()
        {
            var roleName = "Customer";
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);
            _roleManagerMock.Setup(manager => manager.RoleExistsAsync(roleName)).ReturnsAsync(true);

            await _testedService.AddRoleToUserAsync(roleName, userId);

            _userManagerMock.Verify(manager => manager.IsInRoleAsync(It.IsAny<UserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task AddRoleToUserAsync_WhenUserAndRoleExistAndUserIsAlreadyInThisRole_ShouldReturnAddRoleToUserResponseWithSpecificContents()
        {
            var roleName = "Customer";
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            var expectedTestResult = new AddRoleToUserResponse($"User {user.UserName} already have {roleName} role.",
                false, true);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);
            _roleManagerMock.Setup(manager => manager.RoleExistsAsync(roleName)).ReturnsAsync(true);
            _userManagerMock.Setup(manager => manager.IsInRoleAsync(user, roleName)).ReturnsAsync(true);

            var actualTestResult = await _testedService.AddRoleToUserAsync(roleName, userId);

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task AddRoleToUserAsync_WhenUserAndRoleExistAndUserIsNotInRole_ShouldCallAddToRoleAsync()
        {
            var roleName = "Customer";
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);
            _roleManagerMock.Setup(manager => manager.RoleExistsAsync(roleName)).ReturnsAsync(true);
            _userManagerMock.Setup(manager => manager.IsInRoleAsync(user, roleName)).ReturnsAsync(false);

            await _testedService.AddRoleToUserAsync(roleName, userId);

            _userManagerMock.Verify(manager => manager.AddToRoleAsync(It.IsAny<UserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task AddRoleToUserAsync_WhenUserAndRoleExistAndUserIsNotInThisRole_ShouldReturnAddRoleToUserResponseWithSpecificContents()
        {
            var roleName = "Customer";
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            var expectedTestResult = new AddRoleToUserResponse($"Added {roleName} role to user {user.UserName}.", true,
                false);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);
            _roleManagerMock.Setup(manager => manager.RoleExistsAsync(roleName)).ReturnsAsync(true);
            _userManagerMock.Setup(manager => manager.IsInRoleAsync(user, roleName)).ReturnsAsync(false);

            var actualTestResult = await _testedService.AddRoleToUserAsync(roleName, userId);

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task AddRoleToUserAsync_WhenUserDoesNotExists_ShouldReturnAddRoleToUserResponseWithSpecificContents()
        {
            var expectedTestResult = new AddRoleToUserResponse("User not found.", false, false);
            var nonExistentUserId = Guid.NewGuid().ToString();
            var roleName = "Customer";
            _userManagerMock.Setup(manager => manager.FindByIdAsync(nonExistentUserId)).ReturnsAsync(() => null);

            var actualTestResult = await _testedService.AddRoleToUserAsync(roleName, nonExistentUserId);

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task AddRoleToUserAsync_WhenUserExists_ShouldCallRoleExistsAsyncOnce()
        {
            var roleName = "Customer";
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);

            await _testedService.AddRoleToUserAsync(roleName, userId);

            _roleManagerMock.Verify(manager => manager.RoleExistsAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task AddRoleToUserAsync_WhenUserExistsButRoleDoesNotExists_ShouldReturnAddRoleToUserResponseWithSpecificContents()
        {
            var roleName = "Customer";
            var expectedTestResult = new AddRoleToUserResponse($"Role {roleName} does not exist.",
                false, false);
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);
            _roleManagerMock.Setup(manager => manager.RoleExistsAsync(roleName)).ReturnsAsync(false);

            var actualTestResult = await _testedService.AddRoleToUserAsync(roleName, userId);

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task CheckIfUserExistsByEmailAsync_WhenUserDoesNotExists_ShouldReturnFalse()
        {
            var userEmail = "test@email.com";
            _userManagerMock.Setup(manager => manager.FindByEmailAsync(userEmail)).ReturnsAsync(() => null);

            var actualTestResult = await _testedService.CheckIfUserExistsByEmailAsync(userEmail);

            Assert.True(actualTestResult == false);
        }

        [Fact]
        public async Task CheckIfUserExistsByEmailAsync_WhenUserExists_ShouldReturnTrue()
        {
            var userEmail = "test@email.com";
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            _userManagerMock.Setup(manager => manager.FindByEmailAsync(userEmail)).ReturnsAsync(user);

            var actualTestResult = await _testedService.CheckIfUserExistsByEmailAsync(userEmail);

            Assert.True(actualTestResult == true);
        }

        [Fact]
        public async Task CreateUserAsync_WhenInputPayloadIsNotNull_ShouldCallCreateAsyncOnce()
        {
            var request = UserFixture.MakeCreateUserRequest();
            _userManagerMock.Setup(manager => manager.CreateAsync(It.IsAny<UserModel>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            await _testedService.CreateUserAsync(request);

            _userManagerMock.Verify(manager => manager.CreateAsync(It.IsAny<UserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateUserAsync_WhenUserCreatedSuccessfully_ShouldCallAddToRoleAsyncOnce()
        {
            var request = UserFixture.MakeCreateUserRequest();
            _userManagerMock.Setup(manager => manager.CreateAsync(It.IsAny<UserModel>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var actualTestResult = await _testedService.CreateUserAsync(request);

            _userManagerMock.Verify(manager => manager.AddToRoleAsync(It.IsAny<UserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateUserAsync_WhenUserCreatedSuccessfully_ShouldReturnCreateUserResponse()
        {
            var request = UserFixture.MakeCreateUserRequest();
            _userManagerMock.Setup(manager => manager.CreateAsync(It.IsAny<UserModel>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var actualTestResult = await _testedService.CreateUserAsync(request);

            Assert.IsType<CreateUserResponse>(actualTestResult);
        }

        [Fact]
        public async Task DeleteRolesFromUserAsync_WhenUserDoesNotExists_ShouldReturnDeleteRolesFromUserResponseWithSpecificContents()
        {
            var expectedTestResult = new DeleteRolesFromUserResponse("User not found.", false);
            var roles = UserFixture.CreateListOfUserRoles();
            var userId = Guid.NewGuid().ToString();
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(() => null);

            var actualTestResult = await _testedService.DeleteRolesFromUserAsync(userId, roles);

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task DeleteRolesFromUserAsync_WhenUserExists_ShouldCallRemoveFromRolesAsyncOnce()
        {
            var roles = UserFixture.CreateListOfUserRoles();
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);

            var actualTestResult = await _testedService.DeleteRolesFromUserAsync(userId, roles);

            _userManagerMock.Verify(manager => manager.RemoveFromRolesAsync(user, roles), Times.Once);
        }

        [Fact]
        public async Task DeleteRolesFromUserAsync_WhenUserExists_ShouldReturnDeleteRolesFromUserResponseWithSpecificContents()
        {
            var roles = UserFixture.CreateListOfUserRoles();
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            var expectedTestResult = new DeleteRolesFromUserResponse($"Roles were removed successfully from user {user.Email}.",
                true);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);

            var actualTestResult = await _testedService.DeleteRolesFromUserAsync(userId, roles);

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserExists_ShouldCallDeleteAsyncOnce()
        {
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);

            await _testedService.DeleteUserAsync(userId);

            _userManagerMock.Verify(manager => manager.DeleteAsync(It.IsAny<UserModel>()), Times.Once);
        }

        [Fact]
        public async Task GetAllUsersAsync_WhenUsersListIsEmpty_ShouldReturnEmptyList()
        {
            var emptyListOfUsers = new List<UserModel>() { };
            var expectedTestResult = emptyListOfUsers.MapToListOfGetUserResponses();
            _userManagerMock.Setup(manager => manager.Users).Returns(emptyListOfUsers.AsQueryable);

            var actualTestResult = await _testedService.GetAllUsersAsync();

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task GetAllUsersAsync_WhenUsersListIsNotEmpty_ShouldReturnListOfGetUserResponses()
        {
            var actualTestResult = await _testedService.GetAllUsersAsync();

            Assert.IsType<List<GetUserResponse>>(actualTestResult);
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserDoesNotExist_ShouldReturnNull()
        {
            var userId = Guid.NewGuid().ToString();
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(() => null);

            var actualTestResult = await _testedService.GetUserByIdAsync(userId);

            Assert.Null(await _testedService.GetUserByIdAsync(userId));
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserExists_ShouldCallGetRolesAsyncOnce()
        {
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);

            await _testedService.GetUserByIdAsync(userId);

            _userManagerMock.Verify(manager => manager.GetRolesAsync(It.IsAny<UserModel>()), Times.Once);
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserExists_ShouldReturnGetUserResponse()
        {
            var userId = Guid.NewGuid().ToString();
            var user = UserFixture.CreateUserModel(userId);
            var userRoles = UserFixture.CreateListOfUserRoles();
            _userManagerMock.Setup(manager => manager.FindByIdAsync(userId)).ReturnsAsync(user);
            _userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync((IList<string>)userRoles);

            var actualTestResult = await _testedService.GetUserByIdAsync(userId);

            Assert.IsType<GetUserResponse>(actualTestResult);
        }

        [Fact]
        public async Task LogInAsync_WhenUserDoesNotExists_ShouldReturnLogInResponseWithSpecificContents()
        {
            var expectedTestResult = new LogInResponse("User not found.", false, null);
            var userEmail = "test@email.com";
            var request = new LogInRequest(userEmail, "A@12345b");
            _userManagerMock.Setup(manager => manager.FindByNameAsync(userEmail)).ReturnsAsync(() => null);

            var actualTestResult = await _testedService.LogInAsync(request);

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task LogInAsync_WhenUserExistsAndPasswordIsValid_ShouldReturnLogInResponseWithSpecificContentsIncludingJwt()
        {
            var user = UserFixture.CreateUserModel(Guid.NewGuid().ToString());
            var userEmail = "test@email.com";
            var userPassword = "A@12345b";
            var request = new LogInRequest(userEmail, userPassword);
            _userManagerMock.Setup(manager => manager.FindByNameAsync(userEmail)).ReturnsAsync(user);
            _userManagerMock.Setup(manager => manager.CheckPasswordAsync(user, userPassword)).ReturnsAsync(true);
            _userManagerMock.Setup(manager => manager.GetRolesAsync(user)).ReturnsAsync(UserFixture.CreateListOfUserRoles());

            var actualTestResult = await _testedService.LogInAsync(request);

            Assert.Null(actualTestResult.ErrorMessage);
            Assert.True(actualTestResult.IsAuthenticationSuccessful == true);
            Assert.True(string.IsNullOrEmpty(actualTestResult.Jwt) == false);
        }

        [Fact]
        public async Task LogInAsync_WhenUserExistsButPasswordIsInvalid_ShouldReturnLogInResponseWithSpecificContents()
        {
            var expectedTestResult = new LogInResponse("Incorrect login or password.", false, null);
            var user = UserFixture.CreateUserModel(Guid.NewGuid().ToString());
            var userEmail = "test@email.com";
            var userPassword = "A@12345b";
            var request = new LogInRequest(userEmail, userPassword);
            _userManagerMock.Setup(manager => manager.FindByNameAsync(userEmail)).ReturnsAsync(user);
            _userManagerMock.Setup(manager => manager.CheckPasswordAsync(user, userPassword)).ReturnsAsync(false);

            var actualTestResult = await _testedService.LogInAsync(request);

            Assert.Equal(expectedTestResult, actualTestResult);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenInputPayloadIsNotNull_ShouldCallUpdateUserAsync()
        {
            var validUserId = Guid.NewGuid().ToString();
            var updateRequest = UserFixture.CreateUpdateUserRequest();
            var user = UserFixture.CreateUserModel(validUserId);
            _userManagerMock.Setup(manager => manager.FindByIdAsync(validUserId)).ReturnsAsync(user);

            await _testedService.UpdateUserAsync(validUserId, updateRequest);

            _userManagerMock.Verify(manager => manager.UpdateAsync(It.IsAny<UserModel>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(UpdateUserAsyncTestDataGenerator))]
        public async Task UpdateUserAsync_WhenInputUserIdIsNullOrRequestIsNull_ShouldThrowArgumentNullException(
            string userId, UpdateUserRequest request)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _testedService.UpdateUserAsync(userId, request));
        }
    }
}
