using BoB.EFDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.MainDataBase
{
    public class MainDbContext: DbContext
    {
        public MainDbContext() { }

        public MainDbContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(BoBConfiguration.ConnectionStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BaseDbContext.Init(modelBuilder, typeof(IMainModelCreator)); //初始化模型

        }

    }
}
