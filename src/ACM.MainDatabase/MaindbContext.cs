﻿using BoB.EFDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.MainDatabase
{
    public class MaindbContext:DbContext
    {
        public MaindbContext() { }

        public MaindbContext(DbContextOptions<DbContext> options):base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(BoBConfiguration.ConnectionStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BaseDbContext.Init(modelBuilder, typeof(IMaindbModelCreator)); //初始化模型

        }

    }
}