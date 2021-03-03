using BlogCore.Extended;
using BlogCore.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Repository
{
    public class RepositoryFactory : IDesignTimeDbContextFactory<DefaultContext>
    {
        public DefaultContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DefaultContext>();
            ProductDbContextConfigurer.Configure(builder, ConfigManagerConf.GetValue($"ConnectionStrings:Default"));
            return new DefaultContext(builder.Options);
        }
    }
}
