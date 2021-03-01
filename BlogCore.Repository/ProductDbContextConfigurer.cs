using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace BlogCore.Repository
{
    public static class ProductDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DefaultContext> builder, string connectionString)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EFLoggerProvider());
            builder.UseMySQL(connectionString)
                .UseLoggerFactory(loggerFactory);
        }

        public static void Configure(DbContextOptionsBuilder<DefaultContext> builder, DbConnection connection)
        {
            var loggerFactory = new LoggerFactory();
            builder.UseMySQL(connection)
                .UseLoggerFactory(loggerFactory);

        }
    }
}
