using ACM.MainDatabase;
using BoB.EFDbContext;
using BoB.EFDbContext.Enums;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.AppAccountListEntities
{
    public class AppAccountList:IBaseEntity<Guid>
    {
        [Key]
        public Guid ID { get; set; }

        public DataStatus Status { get; set; }

        public Guid UserID { get; set; }

        public int AppID { get; set; }

        public string NickName { get; set; }

        public string AppUserID { get; set; }

        public string Cookie { get; set; }

        public Point Location { get; set; }

        public string Address { get; set; }

        public bool OnLine { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户最后访问的URL
        /// </summary>
        public string LastURL { get; set; }

    }

    public class AppAccountListCreator : IMaindbModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<AppAccountList>().ToTable("AppAccountList");

        }
    }

}
