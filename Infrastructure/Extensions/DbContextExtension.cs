//using EmployeeManangement.Infrastructure.DataSeeding;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;

//namespace Web.Infrastructure.Extensions
//{
//    public static class DbContextExtensions
//    {
//        public static async Task<IApplicationBuilder> ConfigureDataContext(this IApplicationBuilder app)
//        {
//            try
//            {
//                using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
//                var services = serviceScope.ServiceProvider;
//                using (var scope = services.CreateScope())
//                {
//                    using (var context = scope.ServiceProvider.GetRequiredService<WebDbContext>())
//                    {
//                        try
//                        {
//                            await DbInitalizer.Seed(context);
//                        }
//                        catch (Exception)
//                        {

//                        }
//                    }
//                }
//                return app;
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }
//    }
//}
