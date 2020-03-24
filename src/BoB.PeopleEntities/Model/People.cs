using BoB.EFDbContext.Enums;
using BoB.MainDatabase;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BoB.PeopleEntities
{
    public class People
    {
        [Key]
        public int Key { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }


        public  DataStatus DataStatus { get; set; }

    }

    public class PeopleCreator : IMainModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<People>();
            
        }
    }
}
