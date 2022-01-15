using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace JobListingApp.AppCores.Implementations.Tests
{
    [TestClass()]
    public class AuthServiceTests
    {
        private IAuthService _authService { get; }
        private const string email = "israeloluwafemi44@gmail.com";
        private const string password = "P@ssw0rd";
        private const bool rememberMe = false;

        public AuthServiceTests()
        {
            AppUser user = new AppUser
            {
                Id = "asdfghjkl",
                LastName = "Adigun",
                FirstName = "Allen",
                Email = "israeloluwafemi44@gmail.com",
                PasswordHash = "P@ssw0rd"
            };

            var mockupJwt = new Mock<IJwtService>();
            var mockUserManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);//(mockUserManager.Object);
            mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            mockUserManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { });
            mockUserManager.Setup(x => x.CheckPasswordAsync(user, password)).ReturnsAsync(true);

            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserPrincipal = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
            var mockSignIn = new Mock<SignInManager<AppUser>>(mockUserManager.Object, mockContextAccessor.Object, mockUserPrincipal.Object, null, null, null, null);
            mockSignIn.Setup(x => x.PasswordSignInAsync(user, password, rememberMe, false)).ReturnsAsync(SignInResult.Success);
            mockupJwt.Setup(x => x.GenerateToken(user, It.IsAny<List<string>>())).Returns("bdey1234");


            _authService = new AuthService(mockUserManager.Object, mockSignIn.Object, mockupJwt.Object);


        }
        [TestMethod]

        public async Task LoginTestTrue()
        {


            LoginCredDto res = new LoginCredDto { Id = "asdfghjkl", token = "bdey1234", status = true };
            var result = await _authService.Login(email, password, rememberMe);
            //Assert.AreEqual(res,result);
            Assert.IsTrue(result.status);
        }

        [TestMethod]

        [DataRow("no-reply@yahoo.com", password, rememberMe)]
        [DataRow(email, "", rememberMe)]
        [DataRow("email", "asffjj", rememberMe)]
        public async Task LoginTestFalse(string email, string password, bool rememberMe)
        {


            // LoginCredDto res = new LoginCredDto { Id = "asdfghjkl", token = "bdey1234", status = true };
            var result = await _authService.Login(email, password, rememberMe);
            //Assert.AreEqual(res,result);
            Assert.IsFalse(result.status);
        }
    }
}