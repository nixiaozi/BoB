using AutoMapper;
using BoB.EFDbContext.BaseEntities;
using BoB.MainDataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.Work
{
    [AutoMap(typeof(SchoolDto))]
    public class School
    {
        [Key]
        public int Key { get; set; }

        public int SchoolID { get; set; }


        public string SchoolName { get; set; }


        public string Location { get; set; }

        [IgnoreMap]
        [Timestamp]
        public byte[] Version { get; set; } //对于时间戳这种数据库自动生成的数据，需要显式添加Timestamp标记
    }

    /// <summary>
    /// 这里进行了Context初始化，必须添加
    /// </summary>
    public class SchoolCreator : IMainModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<School>().ToTable("School")
                .HasIndex(u => u.SchoolID).IsUnique();
        }
    }
}
