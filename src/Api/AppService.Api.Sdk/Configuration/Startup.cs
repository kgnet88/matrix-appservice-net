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
    /// Injects services to use the application services endpoints.
    /// </summary>
    /// <param name="services">The application services existing service collection.</param>
    /// <param name="options">The application services endpoint options.</param>
    public static IServiceCollection AddAppServiceEndpoints(this IServiceCollection services, AppServiceEndpointOptions options)
    {
        _ = services.AddSingleton(options);
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

        _ = app.UseFastEndpoints(c =>
        {
            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            c.Endpoints.ShortNames = true;
            c.Endpoints.Configurator = ep => ep.PreProcessors(
                new AuthPreProcessor(app.Services.GetRequiredService<AppServiceEndpointOptions>()));
        });

        _ = app.UseOpenApi();
        _ = app.UseSwaggerUi3(s => s.ConfigureDefaults());

        return app;
    }
}