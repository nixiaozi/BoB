using BoB.EFDbContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace BoB.RazorDataBase
{
    public class RazorDbContext:DbContext
    {
        public RazorDbContext() { }

        public RazorDbContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(BoBConfiguration.ConnectionStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BaseDbContext.Init(modelBuilder, typeof(IRazorModelCreator)); //初始化模型

        }


    }
}
