using BoB.EFDbContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace Sunlit.MainDatabase
{
    public class SunlitMainContext:DbContext
    {
        public SunlitMainContext() { }

        public SunlitMainContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(BoBConfiguration.ConnectionStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BaseDbContext.Init(modelBuilder, typeof(ISunlitModelCreator)); //初始化模型

        }



    }
}
