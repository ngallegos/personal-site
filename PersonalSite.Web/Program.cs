using Contentful.AspNetCore;
using PersonalSite.Web.Config;

var builder = WebApplication.CreateBuilder(args);

// No need to use AWS Secrets Manager locally if we've configured the appsettings.local.json file
// with Contentful secrets
var env = builder.Environment.EnvironmentName;
if (string.IsNullOrEmpty(builder.Configuration["ContentfulOptions:DeliveryApiKey"]))
{
    builder.Host.ConfigureAppConfiguration((ctx, configurationBuilder) =>
    {
        configurationBuilder.AddAmazonSecretsManager(ctx.Configuration["AWS:Region"],
            ctx.Configuration["AWS:SecretNames:Contentful"]);
    });
}

// Add services to the container.
builder.Services.AddRazorPages();

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

app.Run();