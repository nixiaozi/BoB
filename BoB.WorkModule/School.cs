using BoB.EFDbContext.BaseEntities;
using BoB.MainDataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.Work
{
    public class School
    {
        [Key]
        public int Key { get; set; }

        public int SchoolID { get; set; }


        public string SchoolName { get; set; }


        public string Location { get; set; }


        public byte[] Version { get;  } //对于时间戳这种数据库自动生成的数据，不能设置set;访问器；防止出现错误
    }

    public class SchoolCreator : IMainModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<School>().ToTable("School")
                .HasIndex(u => u.SchoolID).IsUnique();
        }
    }
}
