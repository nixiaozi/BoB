using BoB.EFDbContext;
using Microsoft.EntityFrameworkCore;
using Sunlit.MainDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunlit.BussinessAssociaterEntities.Model
{
    public class BussinessAssociater:NormalEntity<Guid>
    {
        /// <summary>
        /// 商业伙伴所在地
        /// </summary>
        public string Localtion { get; set; }

        /// <summary>
        /// 商业伙伴的电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 所属实体的ID
        /// </summary>
        public string RelatedEntities { get; set; }

        /// <summary>
        /// 联系人的老家
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// 有关于联系的人的备注信息
        /// </summary>
        public string Comment { get; set; }

    }

    public class BussinessAssociaterCreator : ISunlitModelCreator
    {
        public void CreateModel(ModelBuilder builder)
        {
            builder.Entity<BussinessAssociater>().ToTable("BussinessAssociater");

        }
    }

}
