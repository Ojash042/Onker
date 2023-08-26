using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onker.Models;
using Onker.Services;
using OpenGraphNet;
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
		public async Task<IActionResult> Index() {
			List<PostTagUser> postTagUsers = _dataServices.GetAllPosts().Result;
			
			foreach (var item in postTagUsers) {
				item.HumanizedDate = item.Post.PostedDateTime.Humanize();

				if (item.Post.UrlLink.Length > 0) {
					OpenGraph graph = await OpenGraph.ParseUrlAsync(item.Post.UrlLink);
					string ogImage="";
					string twitterImage="";
					try {
						ogImage = graph.Metadata["og:image"].First().Value;
					}
					catch {

					}
					try {
						twitterImage = graph.Metadata["twitter:image"].First().Value;
					}
					catch {

					}
					item.UrlMetadataImage = ogImage.Length > 0 ? ogImage : (twitterImage.Length > 0 ? twitterImage : "");
				}
				_logger.LogDebug(item.UrlMetadataImage);
			}
			return View(postTagUsers);
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