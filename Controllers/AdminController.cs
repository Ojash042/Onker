using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onker.Models;
using Onker.Services;

namespace Onker.Controllers {
	public class AdminController : Controller {
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AdminServices _adminServices;

		public AdminController(RoleManager<IdentityRole<Guid>> roleManager, UserManager<ApplicationUser> userManager,AdminServices adminServices) {
			_roleManager = roleManager;
			_userManager = userManager;
			_adminServices = adminServices;
		}
		
		public IActionResult Index() {
			return Ok(200);
		}

		[HttpGet]
		public IActionResult CreateRole() {
			var roles = _adminServices.GetAllUserRoles().Result;
			List<UserRoles> userRoles = new List<UserRoles>() { };
			foreach(var item in roles) {
				UserRoles userRole = new UserRoles();
				userRole.UserRoleGuid = item.Id;
				userRole.UserRoleName = item.Name;
				userRoles.Add(userRole);
			}
			return View(userRoles);
		}

		[HttpPost]
		public async Task<IActionResult> CreateRolePost() {
			string? roleName = Request.Form["roleName"];
			await _adminServices.AddUserRoles(roleName);
			return RedirectToAction("CreateRole", "Admin");
		}

		[HttpGet]
		public IActionResult CreatePost() {
			return View();
		}

		[HttpPost]
		public IActionResult CreatePostPost(Post post) {
			return RedirectToAction("CreatePost");
		}

		public IActionResult CreateTag() {
			var tags = _adminServices.GetAllTags().Result;
			return View(tags);
		}

		[HttpPost]
		public IActionResult CreateTagPost() {
			string? tagName = Request.Form["tagName"];
			Tag tag = new Tag() {
				TagName = tagName
			};
			_adminServices.CreateATag(tag);
			return RedirectToAction("CreateTag");
		}
		public IActionResult UserList() {
			var users =  _userManager.Users.ToListAsync().Result;
			return View(users);
		}

		[Route("/Admin/User/{id}")]
		public IActionResult SpecificUser(string id){
			var user = _userManager.FindByIdAsync(id).Result;
			return View(user);
		}

		[Route("/Admin/ChangeUserRole/{id}")]
		public IActionResult ChangeUserRoles(string id) {
			UserView userView = new UserView();
			var roles = _roleManager.Roles.ToList();
			var user = _userManager.FindByIdAsync(id).Result;
			userView.Roles = roles;
			userView.user = user;
			return View(userView);
		}

		[Route("/Admin/ChangeUserRolePost/{id}")]
		public async Task<IActionResult> ChangeUserRolePost(string id) {
			var userId = Guid.Parse(id);
			var userRole = Request.Form["roleName"];
			var user = _userManager.FindByIdAsync(id).Result;
			await _userManager.AddToRoleAsync(user, userRole);
			return RedirectToRoute($"Admin/User/{id}");
		}
	}
}
