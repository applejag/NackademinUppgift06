using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NackademinUppgift06.Models;

namespace NackademinUppgift06
{
    public class Startup
    {

		public IConfiguration Configuration { get; }

	    public Startup(IConfiguration config)
	    {
		    Configuration = config;
	    }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddMvc();
	        services.AddSingleton(Configuration);

			services.AddDbContext<BlogContext>(options =>
		        options.UseSqlServer(Configuration.GetConnectionString("Blog"))
	        );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

	        app.UseStaticFiles();

	        app.UseMvc(route =>
	        {
		        route.MapRoute("default", "{action=Index}", new
		        {
			        controller = "Blog",
		        });

		        route.MapRoute("posts", "post/{id}", new
		        {
			        controller = "Blog",
			        action = "Post",
		        });

		        route.MapRoute("category", "Category/{action=Create}", new
		        {
			        controller = "Category",
		        });
	        });
			
        }
    }
}
