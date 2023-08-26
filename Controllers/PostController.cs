using Microsoft.AspNetCore.Mvc;

namespace Onker.Controllers {
	public class PostController : Controller {
		[Route("/Post/{id}")]
		public IActionResult Index() {
			return View();
		}
	}
}
