using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeClone.Core.Entities;

namespace YoutubeClone.Persistence.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
           UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        FirstName = "Bob",
                        LastName = "Doe",
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com",
                        EmailConfirmed = true
                    },
                    new AppUser
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com",
                        EmailConfirmed = true
                    },
                    new AppUser
                    {
                        FirstName = "Tom",
                        LastName = "Doe",
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.Posts.Any())
            {
                var activities = new List<Post>
                {
                    new Post
                    {
                        Title = "Post with random title",
                        Created = DateTime.Now.AddMonths(-2),
                        Author = null
                    },
                    new Post
                    {
                        Title = "Post with another random title",
                        Created = DateTime.Now.AddMonths(-2),
                        Author = null
                    }

                };

                await context.Posts.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}
