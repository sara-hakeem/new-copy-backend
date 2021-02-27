using Contracts.Repository;
using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace dizzlr_task.Extention
{
    public static class servicesExtentions
    {
        public static void ConfigureCORS(this IServiceCollection services) =>

       services.AddCors(Action =>
       {
           Action.AddPolicy("CorsPolicy", Policy =>
           {
               Policy.AllowAnyOrigin();
               Policy.AllowAnyMethod();
               Policy.AllowAnyHeader();
           });
       });

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<RepositoryContext>(
            //option => option.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
            //action => action.MigrationsAssembly("CompanyEmployees"))
            //);
            services.AddDbContext<RepositoryContext>(
                option => option.UseSqlite("Data Source = task.db"
            ,    action => action.MigrationsAssembly("dizzlr_task")
            ));
        }
        //public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        //    services.AddScoped<IRepositoryManager, RepositoryManager>();




        public static string MD5Hash(this string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

    }
}
