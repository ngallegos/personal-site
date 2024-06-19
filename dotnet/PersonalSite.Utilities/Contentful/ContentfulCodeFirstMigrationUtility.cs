using Contentful.AspNetCore;
using Contentful.CodeFirst;
using Contentful.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CT = PersonalSite.ContentModel;

namespace PersonalSite.Utilities.Contentful;

public class ContentfulCodeFirstMigrationUtility
{
    public static async Task RunMigration(IConfiguration configuration)
    {
        var services = new ServiceCollection()
            .AddLogging(c =>
            {
                c.AddFilter("System.Net.Http.HttpClient.ContentfulClient", LogLevel.Warning);
                c.AddConsole();
            })
            .AddContentful(configuration);
        
        var serviceProvider = services.BuildServiceProvider();

        var logger = serviceProvider.GetRequiredService<ILogger<ContentfulCodeFirstMigrationUtility>>();
        
        using (var scope = serviceProvider.CreateScope())
        {
            logger.LogInformation("Setting up contentful management client");
            var contentfulManagementClient = scope.ServiceProvider.GetRequiredService<IContentfulManagementClient>();

            var cfConfig = new ContentfulCodeFirstConfiguration
            {
                ApiKey = configuration["ContentfulOptions:ManagementApiKey"],
                SpaceId = configuration["ContentfulOptions:SpaceId"],
                ForceUpdateContentTypes = true,
                PublishAutomatically = true
            };

            logger.LogInformation(
                $"Creating content types in contentful for the {configuration["ContentfulOptions:Environment"]} environment");
            
            // These are in order of dependency.  If you add a new content type, make sure it's at the end of the list.
            var contentTypes = new[]
            {
                typeof(CT.ResumeSection),
                typeof(CT.Resume),
                typeof(CT.Page),
                typeof(CT.Link),
                typeof(CT.HeadMeta),
                typeof(CT.SiteMetaData),
                typeof(CT.Blog.Tag),
                typeof(CT.Blog.Post)
            };
            var contentTypesSynchronized = 0;
            foreach (var type in contentTypes)
            {
                logger.LogInformation($"Synchronizing content type - {type.Name}");
                // Do one at a time so we don't go over contentful management api limits.
                var contentTypeInformation = ContentTypeBuilder.InitializeContentTypes(new List<Type> { type });
                var result = await ContentTypeBuilder.CreateContentTypes(contentTypeInformation, cfConfig,
                    contentfulManagementClient);
                if (result.Count > 0)
                {
                    logger.LogInformation($"Content type synchronized - {type.Name}");
                    contentTypesSynchronized += result.Count;
                }
            
                await Task.Delay(1000);
            }

            logger.LogInformation($"Done - Synchronized {contentTypesSynchronized} content types");
        }
    }
}