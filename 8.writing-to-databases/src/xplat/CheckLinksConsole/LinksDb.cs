namespace CheckLinksConsole
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.IO;
    using static CheckLinksConsole.LinkChecker;

    public class LinksDb
        : DbContext
    {
        public DbSet<LinkCheckResult> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //var connection = @"Server=localhost;Database=Links;User Id=sa;Password=Whatever12!";
            //optionsBuilder.UseSqlServer(connection);

            var dbLocation = Path.Combine(Directory.GetCurrentDirectory(), "links.db");
            optionsBuilder.UseSqlite($"Filename={dbLocation}");
        }
    }
}