namespace KgNet88.Matrix.AppService.Api.Sdk.Test;

public sealed class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private const string _accessToken = "Nc-T87ejp0mFtmFeQg2RQAEYUsFy11ekKH4znqQ7OAzw";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _ = builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(Registration));

            _ = services.Remove(descriptor!);

            _ = services.AddAppServiceRegistration(this.GetRegistration());

            descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(AppServiceEndpointOptions));

            _ = services.Remove(descriptor!);

            var options = this.GetOptions();

            _ = services.AddSingleton(options);
        });
    }

    public Registration GetRegistration()
    {
        return new Registration
        {
            Id = AppId.From("ExampleService"),
            Url = Url.From("example.com"),
            AccessToken = Token.From((TokenType.Access, _accessToken)),
            HomeserverToken = Token.From((TokenType.Homeserver, _accessToken)),
            Localpart = SenderLocalpart.From("motu"),
            Aliases = new List<Namespace>(),
            Rooms = new List<Namespace>(),
            Users = new List<Namespace>(),
            RateLimited = false
        };
    }

    public AppServiceEndpointOptions GetOptions()
    {
        return new AppServiceEndpointOptions
        {
            HomeserverToken = Token.From((TokenType.Homeserver, _accessToken)),
            OnPushEventsQueryAsync = (_, _, _) => Task.CompletedTask,
            OnExistUserQueryAsync = (req, _) => req.UserId == "user"
                    ? Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.OK, null))
                    : Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.NotFound, null)),
            OnExistRoomAliasQueryAsync = (req, _) => req.RoomAlias == "room"
                    ? Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.OK, null))
                    : Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.NotFound, "room alias does not exist, try again"))
        };
    }
}