using BlogCore.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Repository
{
    public class DefaultContext: DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options):base(options)
        {
        }

        public DbSet<Advertisement> Advertisement { get; set; }

        /// <summary>
        /// TODO:当数据库创建完成后， EF 创建一系列数据表，表名默认和 DbSet 属性名相同。 集合属性的名称一般使用复数形式，但不同的开发人员的命名习惯可能不一样，
        /// 开发人员根据自己的情况确定是否使用复数形式。 在定义 DbSet 属性的代码之后，添加下面代码，对DbContext指定单数的表名来覆盖默认的表名。
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>().ToTable("t_advertisement");
            base.OnModelCreating(modelBuilder);
        }
    }
}
