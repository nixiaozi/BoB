using BoB.RazorDataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BoB.RazorWork
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="值不能为空！"), StringLength(20,ErrorMessage ="字符串长度最多为20个")]
        public string Name { get; set; }

    }


    public class CustomerCreator : IRazorModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<Customer>().ToTable("Customer")
                .HasIndex(u => u.Id).IsUnique();
        }
    }

}
