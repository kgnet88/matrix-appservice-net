namespace KgNet88.Matrix.AppService.Api.Example;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1102:Make class static.", Justification = "Reflection")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "Reflection")]
public class ExampleAppService
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        _ = builder.Services
            .AddAppServiceRegistration(builder.Configuration)
            .AddAppServiceEndpoints(settings =>
            {
                settings.OnPushEventsQueryAsync = (req, _, _) =>
                {
                    Console.WriteLine($"Event count: {req.ClientEvents.Count}");
                    return Task.CompletedTask;
                };

                settings.OnExistUserQueryAsync = (req, _) =>
                {
                    Console.WriteLine($"User Id: {req.UserId}");
                    return Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.OK, null));
                };

                settings.OnExistRoomAliasQueryAsync = (req, _) =>
                {
                    Console.WriteLine($"Room alias: {req.RoomAlias}");
                    return Task.FromResult<(HttpStatusCode, string?)>((HttpStatusCode.OK, null));
                };
            });

        var app = builder.Build();

        _ = app.UseAppServiceEndpoints();

        app.Run();
    }
}
