using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Extensions.ServiceExtensions;

public static class FileProvider_Extension
{
    public static void Configure_FileProvider(this IServiceCollection services)
    {
        services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(
            Directory.GetCurrentDirectory(), @"D:/IT/My_Projects/RentShop/RentShop_UI/Stuff/Images"
        )));
    }

    public static void UseCustomStaticFiles(this IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"D:/IT/My_Projects/RentShop/RentShop_UI/Stuff/Images")),
            RequestPath = @"/Images"
        });
    }
}