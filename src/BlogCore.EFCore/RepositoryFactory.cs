using BlogCore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogCore.EFCore
{
    public class RepositoryFactory : IDesignTimeDbContextFactory<DefaultContext>
    {
        public DefaultContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DefaultContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            //ProductDbContextConfigurer.Configure(builder, ConfigManagerConf.GetValue($"ConnectionStrings:{BlogCoreConsts.Default}"));
            ProductDbContextConfigurer.Configure(builder, configuration.GetConnectionString(BlogCoreConsts.Default));
            return new DefaultContext(builder.Options);
        }
    }
}
