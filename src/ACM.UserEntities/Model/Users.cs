using ACM.MainDatabase;
using BoB.EFDbContext.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.UserEntities
{
    public class Users
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        public Guid ID { get; set; } = Guid.Empty;

        public string Phone { get; set; }

        public DateTime Brithday { get; set; }

        public DateTime CreateTime { get; set; }

        public DataStatus Status { get; set; }
    }

    public class UsersCreator : IMaindbModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<Users>().ToTable("User");

        }
    }






}
