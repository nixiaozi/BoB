using ACM.MainDatabase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.AppListEntities
{
    public class AppList
    {
        [Key]
        public int ID { get; set; }


        public string AppName { get; set; }

        public string WebDomain { get; set; }

    }


    public class AppListCreator : IMaindbModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<AppList>().ToTable("AppList");

        }
    }
}
