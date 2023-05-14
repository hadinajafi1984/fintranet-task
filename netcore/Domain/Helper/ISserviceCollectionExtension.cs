using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using congestion.calculator.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace congestion.calculator.Domain.Helper
{
    public static class ISserviceCollectionExtension
    {
        public static IServiceCollection configureservice(this IServiceCollection service, IConfiguration Configuration)
        {
            //access the appsetting json file in your WebApplication File
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = buildDir + @"\appsettings.json";
           // string filePath = @"D:\Freinds Movie\Backend Technical Test\netcore\appsettings.json";

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(filePath))
                .AddJsonFile("appSettings.json")
                .Build();

            service.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("xxx")));
            return service;
        }
    }
}
