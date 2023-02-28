using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DEmo.Models
{
    public partial class blogdatabase : DbContext
    {
        public blogdatabase()
            : base("name=blogdatabase1")
        {
        }

        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<catalog> catalogs { get; set; }
        public virtual DbSet<like> likes { get; set; }
        public virtual DbSet<like_news> like_news { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<comment_news> comment_news { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<catalog>()
                .HasMany(e => e.news)
                .WithOptional(e => e.catalog)
                .HasForeignKey(e => e.cat_id);

            modelBuilder.Entity<catalog>()
                .HasMany(e => e.users)
                .WithOptional(e => e.catalog)
                .HasForeignKey(e => e.catalog_id);

            modelBuilder.Entity<news>()
                .HasMany(e => e.like_news)
                .WithRequired(e => e.news)
                .HasForeignKey(e => e.news_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<news>()
                .HasMany(e => e.comment_news)
                .WithRequired(e => e.news)
                .HasForeignKey(e => e.news_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.likes)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.likes1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.id_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.like_news)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.id_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.news)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.comment_news)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.id_user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.comments)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.comments1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.id_user)
                .WillCascadeOnDelete(false);
        }
    }
}
