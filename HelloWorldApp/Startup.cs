using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace HelloWorldApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                    await context.Response.WriteAsync("Hello from .NET 9 Traditional Startup!"));

                endpoints.MapGet("/health", async context =>
                    await context.Response.WriteAsync("Healthy"));

                endpoints.MapGet("/ready", async context =>
                    await context.Response.WriteAsync("Ready"));

                endpoints.MapMethods("/upload", new[] { "POST" }, async context =>
                {
                    if (!context.Request.HasFormContentType)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid form data");
                        return;
                    }

                    var form = await context.Request.ReadFormAsync();
                    var file = form.Files["file"];
                    if (file == null || file.Length == 0)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("No file uploaded");
                        return;
                    }

                    var uploadsPath = "/app/analyzed-jdts";
                    Directory.CreateDirectory(uploadsPath);

                    var filePath = Path.Combine(uploadsPath, Path.GetFileName(file.FileName));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    await context.Response.WriteAsync($"You uploaded and saved {file.FileName}");
                });
            });
        }
    }
}
