namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Summaries;

/// <summary>
/// Swagger documentation summary for the user id query endpoint.
/// </summary>
public sealed class PushEventsSummary : Summary<PushEventsEndpoint, PushEventsRequest>
{
    /// <summary>
    /// Initializes a full summary for the endpoint.
    /// </summary>
    public PushEventsSummary()
    {
        this.Summary = "Push an event (or batch of events) to the application service.";

        this.Description = "This API is called by the homeserver when it wants to push an event (or batch of events) " +
            "to the application service.\r\n\r\n" +
            "Note that the application service should distinguish state events from message events via the presence " +
            "of a `state_key`, rather than via the event type.";

        this.Response(200, "The transaction was processed successfully.");
        this.Response(401, "The homeserver has not supplied credentials to the application service.");
        this.Response(403, "The credentials supplied by the homeserver were rejected.");
        this.Response(404, "The application service indicates that this endpoint not implemented is.");
    }
}
