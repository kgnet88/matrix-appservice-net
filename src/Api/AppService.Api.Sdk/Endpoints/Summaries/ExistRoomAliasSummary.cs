namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Summaries;

/// <summary>
/// Swagger documentation summary for the room alias query endpoint.
/// </summary>
public sealed class ExistRoomAliasSummary : Summary<ExistRoomAliasEndpoint, ExistRoomAliasRequest>
{
    /// <summary>
    /// Initializes a full summary for the endpoint.
    /// </summary>
    public ExistRoomAliasSummary()
    {
        this.Summary = "Queries the existence of a given room alias.";

        this.Description = "This endpoint is invoked by the homeserver on an application service to query the " +
            "existence of a given room alias. The homeserver will only query room `aliases` inside the " +
            "application service’s aliases namespace. The homeserver will send this request when it receives a " +
            "request to join a room alias within the application service’s namespace.";

        this.RequestParam(x => x.AccessToken, "Access token containing the <c>hs_token</c> from " +
            "the application service’s registration");

        this.RequestParam(x => x.RoomAlias, "<b>Required:</b> The room alias being queried.");

        this.ExampleRequest = new ExistRoomAliasRequest() { RoomAlias = "alias" };

        this.Response(200, "The application service indicates that this room alias exists.");
        this.Response(401, "The homeserver has not supplied credentials to the application service.");
        this.Response(403, "The credentials supplied by the homeserver were rejected.");
        this.Response(404, "The application service indicates that this room alias does not exist.");
    }
}
