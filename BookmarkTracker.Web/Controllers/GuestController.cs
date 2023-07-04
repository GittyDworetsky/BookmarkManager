using BookmarkTracker.Data;
using BookmarkTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookmarkTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly string _connectionString;

        public GuestController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("signup")]
        public void Signup(SignupViewModel user)
        {
            var repo = new GuestRepo(_connectionString);
            repo.AddUser(user, user.Password);
        }

        [HttpPost]
        [Route("login")]
        public User Login(LoginViewModel loginViewModel)
        {
            var repo = new GuestRepo(_connectionString);
            var user = repo.Login(loginViewModel.Email, loginViewModel.Password);
            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim("user", loginViewModel.Email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return user;
        }


        [HttpGet]
        [Route("gettopfivebookmarks")]
        public List<BookmarkCount> GetTopFiveBookmarks()
        {
            var repo = new GuestRepo(_connectionString);
            return repo.GetTopFiveBookmarks();
        }


        [HttpGet]
        [Route("getcurrentuser")]
        public User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var repo = new GuestRepo(_connectionString);
            return repo.GetByEmail(User.Identity.Name);
        }

    }
}

