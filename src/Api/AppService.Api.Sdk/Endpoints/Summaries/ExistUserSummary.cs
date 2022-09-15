namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Summaries;

/// <summary>
/// Swagger documentation summary for the user id query endpoint.
/// </summary>
public sealed class ExistUserSummary : Summary<ExistUserEndpoint, ExistUserRequest>
{
    /// <summary>
    /// Initializes a full summary for the endpoint.
    /// </summary>
    public ExistUserSummary()
    {
        this.Summary = "Queries the existence of a given user ID.";

        this.Description = "This endpoint is invoked by the homeserver on an application service to query the " +
            "existence of a given user ID. The homeserver will only query user IDs inside the application service’s " +
            "`users` namespace. The homeserver will send this request when it receives an event for an unknown user " +
            "ID in the application service’s namespace, such as a room invite.";

        this.ExampleRequest = new ExistUserRequest() { UserId = "@user:example.com" };

        this.Response(200, "The application service indicates that this user exists.");
        this.Response(401, "The homeserver has not supplied credentials to the application service.");
        this.Response(403, "The credentials supplied by the homeserver were rejected.");
        this.Response(404, "The application service indicates that this user does not exist.");
    }
}
