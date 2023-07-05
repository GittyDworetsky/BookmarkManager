using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookmarkTracker.Data
{
    public class GuestRepo
    {

        private readonly string _connectionString;

        public GuestRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<BookmarkCount> GetTopFiveBookmarks()
        {
            using var context = new BookmarkTrackerDataContext(_connectionString);
            if (context.Bookmarks != null)
            {
                var bookmarks = context.Bookmarks.FromSqlRaw("SELECT TOP 5 Url, COUNT(*) as bookmarkCount FROM bookmarks " +
                                                 "GROUP BY Url " +
                                                 "ORDER BY bookmarkCount DESC" 
                                                 )
                                     
                .Select(b => new BookmarkCount
                {
                    Url = b.Url,
                    Count = (int)b.BookmarkCount
                }).ToList();

                return bookmarks;

            }
            else
            {
                return new List<BookmarkCount>();
            }
        }


        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var context = new BookmarkTrackerDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                return null;
            }

            return user;

        }

        public User GetByEmail(string email)
        {
            using var ctx = new BookmarkTrackerDataContext(_connectionString);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }


    }

}
