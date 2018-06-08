using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.API.Models;

namespace User.API.Data
{
    public class UserContextSeed
    {
        public static async Task SeedAsync(IApplicationBuilder app, ILoggerFactory logger, int? retry = 0)
        {
            var retryForAvaiability = retry.Value;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                try
                {

                    var userContext = (UserContext)scope.ServiceProvider.GetServices(typeof(UserContext));
                    var log = (ILogger<UserContextSeed>)scope.ServiceProvider.GetServices(typeof(ILogger<UserContext>));
                    log.LogDebug("Begin USerContextSeed SeedAs");

                    userContext.Database.Migrate();
                    if (!userContext.Users.Any())
                    {
                        userContext.Users.Add(new Models.AppUser { Name = "jgsy" });
                        userContext.SaveChanges();
                    }

                }
                catch (Exception e)
                {
                    if (retryForAvaiability < 10)
                    {
                        retryForAvaiability++;
                        var loggers = logger.CreateLogger(typeof(UserContextSeed));
                        loggers.LogError(e.Message);
                        await SeedAsync(app, logger, retryForAvaiability);

                    }
                }
            }
        }
    }
}
