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
using AutomationExcercise.Utilities;

public class DependencyInjection
{
    // This method is required by Reqnroll's DI integration
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IWebDriver>(sp => DriverFactory.GetDriver());
        services.AddTransient<WaitHelper>();
        services.AddTransient<LoginPage>();
        services.AddTransient<ProductPage>(); // <-- Add this line

        // Register more pages/services as needed  
        return services;
    }
}
