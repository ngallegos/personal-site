using Contentful.AspNetCore;
using PersonalSite.ContentModel;
using PersonalSite.Web.Config;

var builder = WebApplication.CreateBuilder(args);

// No need to use AWS Secrets Manager locally if we've configured the appsettings.local.json file
// with Contentful secrets
var env = builder.Environment.EnvironmentName;
if (string.IsNullOrEmpty(builder.Configuration["ContentfulOptions:DeliveryApiKey"]))
{
    builder.Configuration.AddAmazonSecretsManager(builder.Configuration["AWS:Region"],
            builder.Configuration["AWS:SecretNames:Contentful"]);
}

builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<IContentService, CachedContentService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHealthChecks();

builder.Services.AddContentful(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsEnvironment("local"))
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHealthChecks("/health");

app.Run();