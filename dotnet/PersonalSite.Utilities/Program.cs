// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using PersonalSite.Utilities.Contentful;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.local.json", optional: false, reloadOnChange: true);

var configuration = builder.Build();


await ContentfulCodeFirstMigrationUtility.RunMigration(configuration);