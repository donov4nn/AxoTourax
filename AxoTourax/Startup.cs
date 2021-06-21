using AxoTourax.Configuration;
using AxoTourax.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using AxoTourax.Models;

namespace AxoTourax
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddDbContext<AxoTouraxContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true ,
                    IssuerSigningKey = new SymmetricSecurityKey(key) ,
                    ValidateIssuer = false ,
                    ValidateAudience = false ,
                    ValidateLifetime = true ,
                    RequireExpirationTime = false
                };
            });

            services.AddDefaultIdentity<IdentityUser>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<AxoTouraxContext>();

            services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1" , new OpenApiInfo 
                { 
                    Title = "Tourax" , 
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "donov4nn",
                        Email = "dono.m51@gmail.com"
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app , IWebHostEnvironment env, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                { 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json" , "Tourax v1");
                    c.RoutePrefix = string.Empty;
                });
        }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Task.Run(() =>
            {
                string adminEmail = "admin@admin.admin";
                CreateRolesAsync(roleManager).Wait();
                CreateAdminUserAsync(adminEmail, userManager).Wait();
                AddAdminRolesAsync(adminEmail, userManager).Wait();
            }).Wait();
        }

        private async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = Role.GetAll();

            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
        }

        private async Task CreateAdminUserAsync(string email, UserManager<IdentityUser> userManager)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser != null) return;
            var newUser = new IdentityUser { Email = email , UserName = email };
            var isCreated = await userManager.CreateAsync(newUser , "Azerty1*");
        }

        private async Task AddAdminRolesAsync(string adminEmail, UserManager<IdentityUser> userManager)
        {
            var currentUser = await userManager.FindByEmailAsync(adminEmail);

            if (currentUser != null)
                await userManager.AddToRolesAsync(currentUser , Role.GetAll());
        }
    }
}
