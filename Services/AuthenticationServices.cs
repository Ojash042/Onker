using Microsoft.AspNetCore.Identity;
using Onker.Models;
using System.Security.Claims;

namespace Onker.Services {
	public class AuthenticationServices {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;
		private readonly IConfiguration _configuration;

		public AuthenticationServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration) {
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
		}

		public async Task<(int, string)> Registration(RegistrationModel registration, string role) {
			var userExists = await _userManager.FindByNameAsync(registration.UserName);
			if (userExists != null) 
				return (0, "User Already Exists");

			ApplicationUser user = new ApplicationUser() {
				Email = registration.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = registration.UserName,
				UserKarma = 0,
				DateJoined = DateTime.UtcNow,
			};

			var createUserResult = await _userManager.CreateAsync(user, registration.Password);

			if (!createUserResult.Succeeded)
				return (0, "User Creation failed, Try again");

			if (!await _roleManager.RoleExistsAsync(role))
				await _roleManager.CreateAsync(new IdentityRole<Guid>(role));

			if (!await _roleManager.RoleExistsAsync(role))
				await _userManager.AddToRoleAsync(user, role);

			return (1, "User Created Successfully");
		}

		public async Task<(int, string)> Login(LoginModel login) {
			var user = await _userManager.FindByNameAsync(login.Username);

			if (user == null)
				return (0, "User Not Found!");

			if (!await _userManager.CheckPasswordAsync(user, login.Password))
				return (1, "Invalid Credentials");

			var userRoles = await _userManager.GetRolesAsync(user);
			var authClaims = new List<Claim>() {
				new Claim(ClaimTypes.Name, user.UserName)
			};

			foreach (var userRole in userRoles) {
				authClaims.Add(new Claim(ClaimTypes.Role, userRole));
			}
			return (1, "Logged In Successfully");
		}
	}
}
