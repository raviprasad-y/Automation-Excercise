using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Reqnroll.Configuration;
using AutomationExcercise.Drivers;
using AutomationExcercise.Pages;
using OpenQA.Selenium;
using Reqnroll.Infrastructure;
using Reqnroll.BoDi;
using System.Reflection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using AutomationExercise.DriverManagers;

namespace AutomationExcercise.Utilities
{
    public class DependencyInjection
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            //Registering appropriate IDriverManager
            Type driverType = DriverManagerFactory.GetDriverManagerType();
            services.AddScoped(typeof(IDriverManager), driverType);
            services.AddScoped<IWebDriver>(sp => sp.GetRequiredService<IDriverManager>().GetDriver());

            //Register Page objects
            services.AddTransient<LoginPage>();
            services.AddTransient<ProductPage>();

            return services;
        }
    }
}
