using ACM.BaseAutoAction;
using ACM.MainDatabase;
using BoB.EFDbContext;
using BoB.EFDbContext.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.DoingTasksEntities
{
    public class DoingTasks : IBaseEntity<Guid>
    {
        [Key]
        public Guid ID { get; set; }

        public DataStatus Status { get; set; }

        public DoingTaskStatusEnum TaskExecingStatus { get; set; }

        public Guid TaskID { get; set; }

        public string ParamObj { get; set; }
    }

    public class DoingTasksCreator : IMaindbModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<DoingTasks>().ToTable("DoingTasks");

        }
    }
}
