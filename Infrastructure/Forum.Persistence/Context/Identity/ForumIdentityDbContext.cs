using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Context.Identity
{
    public class ForumIdentityDbContext : IdentityDbContext<ForumIdentityUser,ForumIdentityRole,string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MSA;database=ForumDbContext;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}
