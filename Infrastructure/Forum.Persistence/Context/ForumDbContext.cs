using Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Context
{
    public class ForumDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MSA;database=ForumDbContext;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Identity için gerekli ayarları çağır

            // **User - Comment İlişkisi**
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Kullanıcı silinirse, UserId NULL olur.

            // **User - SubComment İlişkisi**
            modelBuilder.Entity<SubComment>()
                .HasOne(sc => sc.User)
                .WithMany()
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Kullanıcı silinirse, UserId NULL olur.

            // **Comment - SubComment İlişkisi**
            modelBuilder.Entity<SubComment>()
                .HasOne(sc => sc.Comment)
                .WithMany(c => c.SubComments)
                .HasForeignKey(sc => sc.CommentId)
                .OnDelete(DeleteBehavior.NoAction); // Yorum silinirse, bağlı SubComment'ler de silinir.

            // **Post - Comment İlişkisi**
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction); // Post silinirse, bağlı yorumlar da silinir.
        }

    }
}
