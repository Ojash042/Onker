using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onker.Models;
using Onker.Services;
using System.Diagnostics;

namespace Onker.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;
		private readonly DataServices _dataServices;
		public HomeController(ILogger<HomeController> logger, DataServices dataServices) {
			_logger = logger;
			_dataServices = dataServices;
		}
		[Route("/")]
		public IActionResult Index() {
			return View();
		}

		public IActionResult Privacy() {
			return View();
		}

		[Route("/CreateTag")]
		[HttpGet]
		public IActionResult CreateTag() {
			return View();
		}

		[Authorize]
		[Route("/CreateTagPost")]
		[HttpPost]
		public IActionResult CreateTagPost(Tag tag) {
			_dataServices.CreateTagObject(tag);	
			return RedirectToAction("CreateTag");
		}

		[Authorize]
		[Route("/CreatePost")]
		[HttpGet]
		public IActionResult CreatePost() {
			return View();
		}

		[Authorize]
		[Route("/CreatePostPost")]
		[HttpPost]
		public IActionResult CreatePostPost(Post post) {
			_dataServices.SavePost(post);
			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}