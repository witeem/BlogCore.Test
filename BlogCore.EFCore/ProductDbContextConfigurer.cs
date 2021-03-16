using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace BlogCore.EFCore
{
    public static class ProductDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DefaultContext> builder, string connectionString)
        {
            builder.UseMySQL(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DefaultContext> builder, DbConnection connection)
        {
            builder.UseMySQL(connection);
        }
    }
}
