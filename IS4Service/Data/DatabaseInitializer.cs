using IS4Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS4Service.Data
{
    public class DatabaseInitializer
    {
        public async static Task SeedUsersAsync(ApplicationDbContext context)
        {
            if (context.Users.Any())
                return;
            var userStore = new UserStore<ApplicationUser>(context);

            var user1 = new ApplicationUser { UserName = "user1", Email = "user1", NormalizedEmail = "user1", NormalizedUserName = "user1" };
            var user2 = new ApplicationUser { UserName = "user2", Email = "user2", NormalizedEmail = "user2", NormalizedUserName = "user2" };
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user1.PasswordHash = passwordHasher.HashPassword(user1, "123");
            user2.PasswordHash = passwordHasher.HashPassword(user2, "123");
            await userStore.CreateAsync(user1);
            await userStore.CreateAsync(user2);
            await context.SaveChangesAsync();
        }
    }
}
