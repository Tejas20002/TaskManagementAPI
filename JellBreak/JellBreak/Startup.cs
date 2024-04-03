using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace JellBreak
{

        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllersWithViews();
                var mongoConnectionString = Configuration.GetConnectionString("MongoDBConnection");
                var mongoDatabaseName = Configuration.GetConnectionString("MongoDBDatabaseName");
                services.AddSingleton(new MongoDbContext(mongoConnectionString, mongoDatabaseName));

            }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Define your routes
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Route: /b/{id}
                endpoints.MapControllerRoute(
                    name: "boardDetails",
                    pattern: "/b/{id}",
                    defaults: new { controller = "Board", action = "BoardDetails" }
                );

                // Route: /{userId}/boards
                endpoints.MapControllerRoute(
                    name: "userBoards",
                    pattern: "/{userId}/boards",
                    defaults: new { controller = "Board", action = "GetUserBoards" }
                );

                // Route: /b/
                endpoints.MapControllerRoute(
                    name: "createBoard",
                    pattern: "/b/",
                    defaults: new { controller = "Board", action = "CreateBoard" }
                );

                // Route: /b/{id}/
                endpoints.MapControllerRoute(
                    name: "updateBoard",
                    pattern: "/b/{id}/",
                    defaults: new { controller = "Board", action = "UpdateBoard" }
                );

                // Route: /b/{id}/
                endpoints.MapControllerRoute(
                    name: "deleteBoard",
                    pattern: "/b/{id}/",
                    defaults: new { controller = "Board", action = "DeleteBoard" }
                );

                // Route: /1/cards/{id}
                endpoints.MapControllerRoute(
                    name: "cardDetails",
                    pattern: "/1/cards/{id}",
                    defaults: new { controller = "Card", action = "CardDetails" }
                );

                // Route: /1/cards/
                endpoints.MapControllerRoute(
                    name: "createCard",
                    pattern: "/1/cards/",
                    defaults: new { controller = "Card", action = "CreateCard" }
                );

                // Route: /1/cards/{id}
                endpoints.MapControllerRoute(
                    name: "updateCard",
                    pattern: "/1/cards/{id}",
                    defaults: new { controller = "Card", action = "UpdateCard" }
                );

                // Route: /1/cards/{id}
                endpoints.MapControllerRoute(
                    name: "deleteCard",
                    pattern: "/1/cards/{id}",
                    defaults: new { controller = "Card", action = "DeleteCard" }
                );

                // Route: /1/cards/{id}/checklist
                endpoints.MapControllerRoute(
                    name: "createSubtask",
                    pattern: "/1/cards/{id}/checklist",
                    defaults: new { controller = "Subtask", action = "CreateSubtask" }
                );

                // Route: /1/cards/{id}/checklist/{idSubtask}
                endpoints.MapControllerRoute(
                    name: "editSubtask",
                    pattern: "/1/cards/{id}/checklist/{idSubtask}",
                    defaults: new { controller = "Subtask", action = "EditSubtask" }
                );

                // Route: /1/cards/{id}/checklist/{idSubtask}
                endpoints.MapControllerRoute(
                    name: "deleteSubtask",
                    pattern: "/1/cards/{id}/checklist/{idSubtask}",
                    defaults: new { controller = "Subtask", action = "DeleteSubtask" }
                );

                // Route: /signup
                endpoints.MapControllerRoute(
                    name: "signup",
                    pattern: "/signup",
                    defaults: new { controller = "Account", action = "CreateAccountPost" }
                );

                // Route: /login
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "/login",
                    defaults: new { controller = "Account", action = "AccountLoginPost" }
                );

                // Route: /login/guest
                endpoints.MapControllerRoute(
                    name: "loginGuest",
                    pattern: "/login/guest",
                    defaults: new { controller = "Account", action = "CreateGuestAccount" }
                );

                // Route: /user
                endpoints.MapControllerRoute(
                    name: "user",
                    pattern: "/user",
                    defaults: new { controller = "Account", action = "UserGet" }
                );

                // Route: /logout
                endpoints.MapControllerRoute(
                    name: "logout",
                    pattern: "/logout",
                    defaults: new { controller = "Account", action = "AccountLogout" }
                );

                endpoints.MapControllerRoute(
                    name: "listDetails",
                    pattern: "/1/lists/{id}",
                    defaults: new { controller = "List", action = "ListDetails" }
                );

                // Route: /1/lists/
                endpoints.MapControllerRoute(
                    name: "createList",
                    pattern: "/1/lists/",
                    defaults: new { controller = "List", action = "CreateList" }
                );

                // Route: /1/lists/{id}
                endpoints.MapControllerRoute(
                    name: "updateList",
                    pattern: "/1/lists/{id}",
                    defaults: new { controller = "List", action = "UpdateList" }
                );

                // Route: /1/lists/{id}
                endpoints.MapControllerRoute(
                    name: "deleteList",
                    pattern: "/1/lists/{id}",
                    defaults: new { controller = "List", action = "DeleteList" }
                );

                // Default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    }
