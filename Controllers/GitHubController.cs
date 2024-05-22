using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class GitHubController(GitHubService gitHubService) : Controller
    {
        private readonly GitHubService _gitHubService = gitHubService;

        public async Task<IActionResult> Index(string? username = "csdev-aOSE")
        {
            if (ModelState.IsValid == false) return BadRequest();

            try
            {
                var gitHubUser = await _gitHubService.GetGitHubUserAsync(username);

                if (gitHubUser == null) return NotFound();

                return View(gitHubUser);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
