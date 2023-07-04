using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkTracker.Data
{
    public class UserRepo
    {
        private readonly string _connectionString;

        public UserRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Bookmark> GetBookmarksById(int id)
        {
            using var context = new BookmarkTrackerDataContext(_connectionString);
            return context.Bookmarks.Where(b => b.UserId == id).ToList();          
        }

        public void AddBookmarkForUser(Bookmark bookmark)
        {
            using var context = new BookmarkTrackerDataContext(_connectionString);
            context.Bookmarks.Add(bookmark);
            context.SaveChanges();

        }

        public void UpdateBookmarkTitle(string title, int id)
        {
            using var context = new BookmarkTrackerDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE bookmarks SET title={title} WHERE id={id}");
            context.SaveChanges();

        }

        public void DeleteBookmark(int id)
        {
            using var context = new BookmarkTrackerDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM Bookmarks WHERE Id={id}");

        }



    }
}
