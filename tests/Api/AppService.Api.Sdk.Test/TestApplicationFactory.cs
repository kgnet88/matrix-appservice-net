namespace KgNet88.Matrix.AppService.Api.Sdk.Test;

public sealed class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configuration = InitConfiguration();

        _ = builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(Registration));

            _ = services.Remove(descriptor!);

            _ = services.AddAppServiceRegistration(configuration);

            descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(AppServiceEndpointSettings));

            _ = services.Remove(descriptor!);

            var options = GetOptions();

            _ = services.AddSingleton(options);
        });
    }

    private static AppServiceEndpointSettings GetOptions()
    {
        return new AppServiceEndpointSettings
        {
            OnPushEventsQueryAsync = (_, _, _) => Task.CompletedTask,
            OnExistUserQueryAsync = (req, _) => req.UserId == "user"
                    ? Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.OK, null))
                    : Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.NotFound, null)),
            OnExistRoomAliasQueryAsync = (req, _) => req.RoomAlias == "room"
                    ? Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.OK, null))
                    : Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.NotFound, "room alias does not exist, try again"))
        };
    }

    private static IConfiguration InitConfiguration()
    {
        return new ConfigurationBuilder()
           .AddJsonFile(Path.Combine(ContentRootPath(), "testconfig.json"))
           .Build();
    }

    private static string ContentRootPath()
    {
        string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        if (rootDirectory.Contains("bin"))
        {
            rootDirectory = rootDirectory[..rootDirectory.IndexOf("bin")];
        }
        return rootDirectory;
    }
}