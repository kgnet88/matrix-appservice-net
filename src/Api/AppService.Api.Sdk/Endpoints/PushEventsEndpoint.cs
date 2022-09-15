namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints;

/// <summary>
/// This endpoint is called by the homeserver when it wants to push an event (or batch of events) to the application
/// service.
/// <remarks>
/// Note that the application service should distinguish state events from message events via the presence of a
/// <c>state_key</c>, rather than via the event type.
/// </remarks>
/// </summary>
public sealed class PushEventsEndpoint : Endpoint<PushEventsRequest>
{
    /// <summary>
    /// Userdefined and application service specific endpoint options.
    /// </summary>
    private readonly AppServiceEndpointOptions _options;

    /// <summary>
    /// Injects endpoint options to make them available.
    /// </summary>
    /// <param name="options">Injected endpoint options.</param>
    public PushEventsEndpoint(AppServiceEndpointOptions options)
    {
        this._options = options;
    }

    /// <summary>
    /// Configures the neccessary routes for the endpoint.
    /// </summary>
    /// <seealso href="https://spec.matrix.org/v1.3/application-service-api/#pushing-events">
    /// Transaction API for sending a list of events
    /// </seealso>
    public override void Configure()
    {
        this.Put("_matrix/app/v1/transactions/{txnId}");
        this.AllowAnonymous();
    }

    /// <summary>
    /// the handler method for the endpoint. this method is called for each request received. It delegates the work to
    /// the user defined handler and processes its result.
    /// </summary>
    /// <param name="req">the request dto</param>
    /// <param name="ct">a cancellation token</param>
    public override async Task HandleAsync(PushEventsRequest req, CancellationToken ct)
    {
        long txnId = this.Route<int>("txnId");

        await this._options.OnPushEventsQueryAsync(req, txnId, ct);
        await this.SendOkAsync(ct);
    }
}
