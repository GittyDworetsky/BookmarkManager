using BookmarkTracker.Data;
using BookmarkTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookmarkTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly string _connectionString;

        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("getbookmarks")]
        public List<Bookmark> GetBookmarks()
        {
            var user = GetCurrentUser();
            var UserRepo = new UserRepo(_connectionString);
            return UserRepo.GetBookmarksById(user.Id);
        }

        [HttpPost]
        [Route("addbookmarkforuser")]
        public void AddBookmarkForUser(Bookmark bookmark)
        {
            var user = GetCurrentUser();
            bookmark.UserId = user.Id;
            var UserRepo = new UserRepo(_connectionString);
            UserRepo.AddBookmarkForUser(bookmark);

        }

        [HttpPost]
        [Route("updatebookmarktitle")]
        public void UpdateBookmarkTitle(UpdateBookmarkViewModel vm)
        {
            var UserRepo = new UserRepo(_connectionString);
            UserRepo.UpdateBookmarkTitle(vm.Title, vm.Id);

        }

        [HttpPost]
        [Route("deletebookmark")]
        public void DeleteBookmark(DeleteViewModel vm)
        {
            var UserRepo = new UserRepo(_connectionString);
            UserRepo.DeleteBookmark(vm.Id);
        }

        [HttpGet]
        [Route("logout")]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }

        private User GetCurrentUser()
        {
            var guestRepo = new GuestRepo(_connectionString);
            var user = guestRepo.GetByEmail(User.Identity.Name);
            return user;
        }



    }
}
