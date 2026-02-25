using GemManagment.BLL.Profiles;
using GemManagment.BLL.Services.AttachmentService;
using GemManagment.BLL.Services.Implementaion;
using GemManagment.BLL.Services.Interfaces;
using GemManagment.DAL.Data.Context;
using GemManagment.DAL.Models;
using GemManagment.DAL.Repositorys.Implementaion;
using GemManagment.DAL.Repositorys.Interfaces;
using GymManagment.BLL.Services.Classes;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.DAL.Repositotys.Classes;
using GymManagment.DAL.Repositotys.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace GemManagment.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Service

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GemDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUniteOfWork, UniteOfWork>();
            builder.Services.AddScoped<ISessionRepo, SessionRepo>();
            builder.Services.AddScoped<IMemberShipRepo, MemberShipRepo>();
            builder.Services.AddScoped<ImemberSessionRepository, MemberSessionRepository>();


            builder.Services.AddScoped<IAnlyticService, AnlyticService>();
            builder.Services.AddScoped<ImemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IMemberShip, MemberShipService>();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<IMemberSessionService, MemberSessionService>();




            builder.Services.AddScoped<IAccount, AccountService>();

            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<GemDbcontext>()
                .AddDefaultTokenProviders();




            builder.Services.AddScoped<GemDbInitlizer>();

            builder.Services.AddAutoMapper(c => c.AddProfile(new AutoMapperProfile()));
            #endregion

            var app = builder.Build();


            #region Migrate - Seed

            var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<GemDbInitlizer>();
            dbInitializer.Initialize();
            dbInitializer.SeedDate();
            await dbInitializer.SeedIdentity();

            #endregion

            // Configure the HTTP request pipeline.
            #region Pipline

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            #endregion

            app.Run();
        }
    }
}
