using AIMCPSample.Shared.Services;
using AIMCPSample.Web.Components;
using AIMCPSample.Web.Services;

namespace AIMCPSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient();

            // Add device-specific services used by the AIMCPSample.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
 
            builder.Services.AddSingleton<MenuService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(AIMCPSample.Shared._Imports).Assembly);

            app.Run();
        }
    }
}
