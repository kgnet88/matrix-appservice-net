namespace KgNet88.Matrix.AppService.Api.Example;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1102:Make class static.", Justification = "Reflection")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "Reflection")]
public class ExampleAppService
{
    public static void Main(string[] args)
    {
        var registration = new Registration
        {
            Id = AppId.From("ExampleService"),
            Url = Url.From("example.com"),
            AccessToken = Token.From((TokenType.Access, RegistrationService.GenerateToken())),
            HomeserverToken = Token.From((TokenType.Homeserver, RegistrationService.GenerateToken())),
            Localpart = SenderLocalpart.From("motu"),
            Aliases = new List<Namespace>(),
            Rooms = new List<Namespace>(),
            Users = new List<Namespace>(),
            RateLimited = false
        };

        if (registration is null)
        {
            Console.WriteLine("Registration is not valid!");
            Environment.Exit(1);
        }

        var options = new AppServiceEndpointOptions
        {
            HomeserverToken = registration!.HomeserverToken,
            OnPushEventsQueryAsync = (req, _, _) =>
            {
                Console.WriteLine($"Event count: {req.ClientEvents.Count}");
                return Task.CompletedTask;
            },
            OnExistUserQueryAsync = (req, _) =>
            {
                Console.WriteLine($"User Id: {req.UserId}");
                return Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.OK, null));
            },
            OnExistRoomAliasQueryAsync = (req, _) =>
            {
                Console.WriteLine($"Room alias: {req.RoomAlias}");
                return Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.OK, null));
            }
        };

        var builder = WebApplication.CreateBuilder(args);

        _ = builder.Services
            .AddAppServiceRegistration(registration)
            .AddAppServiceEndpoints(options);

        var app = builder.Build();

        _ = app.UseAppServiceEndpoints();

        app.Run();
    }
}
