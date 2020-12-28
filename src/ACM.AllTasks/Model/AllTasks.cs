using ACM.BaseAutoAction;
using ACM.MainDatabase;
using BoB.EFDbContext;
using BoB.EFDbContext.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.AllTasksEntities
{
    public class AllTasks:IBaseEntity<Guid>
    {
        /// <summary>
        /// Key
        /// </summary>
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// 条目数据状态
        /// </summary>
        public DataStatus Status { get; set; }

        /// <summary>
        /// 任务所对应的APPID
        /// </summary>
        public int AppID { get; set; }

        /// <summary>
        /// 任务所对应的用户编号
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 任务执行的状态
        /// </summary>
        public TaskExecuteStatusEnum TaskEcecuteStatus { get; set; }

        /// <summary>
        /// 任务的等级
        /// </summary>
        public ACMTaskLevelEnum TaskLevel { get; set; }

        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 任务开始执行的时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 任务完成执行的时间
        /// </summary>
        public DateTime DoneTime { get; set; }

        /// <summary>
        /// 任务JSON序列化参数
        /// </summary>
        public string ParamObj { get; set; }

    }

    public class AllTasksCreator : IMaindbModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<AllTasks>().ToTable("AllTasks");

        }
    }

}
