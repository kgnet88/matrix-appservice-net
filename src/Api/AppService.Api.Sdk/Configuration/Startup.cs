namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration;

/// <summary>
/// Extension methods for injected services and app modifications during startup.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Injects the registration into the application service
    /// </summary>
    /// <param name="services">The application services existing service collection.</param>
    /// <param name="registration">The application services registration.</param>
    public static IServiceCollection AddAppServiceRegistration(this IServiceCollection services, Registration registration)
    {
        _ = services.AddSingleton(registration);

        return services;
    }

    /// <summary>
    /// Injects the registration into the application service. Loads the registration from file beforehand.
    /// </summary>
    /// <param name="services">The application services existing service collection.</param>
    /// <param name="registrationPath">The application services registration file.</param>
    public static IServiceCollection AddAppServiceRegistration(this IServiceCollection services, string registrationPath)
    {
        var registration = RegistrationService.LoadRegistrationFromFile(registrationPath);

        if (registration is null)
        {
            Console.WriteLine("Registration is not valid!");
            Environment.Exit(1);
        }

        _ = services.AddSingleton(registration);

        return services;
    }

    /// <summary>
    /// Injects the registration into the application service. Loads the registration from configuration beforehand.
    /// </summary>
    /// <param name="services">The application services existing service collection.</param>
    /// <param name="configuration">The application services configuration.</param>
    public static IServiceCollection AddAppServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var registration = RegistrationService.LoadRegistrationFromConfiguration(configuration);

        if (registration is null)
        {
            Console.WriteLine("Registration is not valid!");
            Environment.Exit(1);
        }

        _ = services.AddSingleton(registration);

        return services;
    }

    /// <summary>
    /// Injects services to use the application services endpoints.
    /// </summary>
    /// <param name="services">The application services existing service collection.</param>
    /// <param name="settings">The application services endpoint options.</param>
    public static IServiceCollection AddAppServiceEndpoints(this IServiceCollection services, Action<AppServiceEndpointSettings> settings)
    {
        var endpointSettings = new AppServiceEndpointSettings();
        settings(endpointSettings);

        _ = services.AddSingleton(endpointSettings);
        _ = services.AddFastEndpoints();

        _ = services.AddSwaggerDoc(settings =>
            {
                settings.Title = "Application Service API";
                settings.Version = "v1";
            },
            serializer =>
            {
                serializer.PropertyNamingPolicy = null;
                serializer.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            },
            shortSchemaNames: true,
            addJWTBearerAuth: false);

        return services;
    }

    /// <summary>
    /// Modifies the application to use endpoints, swagger and customized error handling middleware.
    /// </summary>
    /// <param name="app">The web application object.</param>
    public static WebApplication UseAppServiceEndpoints(this WebApplication app)
    {
        _ = app.UseMiddleware<HttpExceptionMiddleware>();

        var registration = app.Services.GetRequiredService<Registration>();

        _ = app.UseFastEndpoints(c =>
        {
            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            c.Endpoints.ShortNames = true;
            c.Endpoints.Configurator = ep => ep.PreProcessors(new AuthPreProcessor(registration));
        });

        _ = app.UseOpenApi();
        _ = app.UseSwaggerUi3(s => s.ConfigureDefaults());

        return app;
    }
}