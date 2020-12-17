using ACM.MainDatabase;
using BoB.EFDbContext;
using BoB.EFDbContext.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.AppListEntities
{
    public class AppList:IBaseEntity<int>
    {
        [Key]
        public int ID { get; set; }


        public string AppName { get; set; }

        public string WebDomain { get; set; }

        public DataStatus Status { get; set; }
    }


    public class AppListCreator : IMaindbModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<AppList>().ToTable("AppList");

        }
    }
}
