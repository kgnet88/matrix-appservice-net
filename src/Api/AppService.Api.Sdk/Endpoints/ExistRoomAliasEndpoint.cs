namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints;

/// <summary>
/// This endpoint is invoked by the homeserver on an application service to query the existence of a given room alias.
/// <remarks>
/// The homeserver will only query room aliases inside the application service’s aliases <c>namespace</c>. The
/// homeserver will send this request when it receives a request to join a room alias within the application service’s
/// namespace.
/// </remarks>
/// </summary>
public sealed class ExistRoomAliasEndpoint : Endpoint<ExistRoomAliasRequest>
{
    /// <summary>
    /// Userdefined and application service specific endpoint options.
    /// </summary>
    private readonly AppServiceEndpointSettings _options;

    /// <summary>
    /// Injects endpoint options to make them available.
    /// </summary>
    /// <param name="options">Injected endpoint options.</param>
    public ExistRoomAliasEndpoint(AppServiceEndpointSettings options)
    {
        this._options = options;
    }

    /// <summary>
    /// Configures the neccessary routes for the endpoint.
    /// </summary>
    /// <seealso href="https://spec.matrix.org/v1.3/application-service-api/#get_matrixappv1roomsroomalias">
    /// Querying API for room aliases
    /// </seealso>
    /// <seealso href="https://spec.matrix.org/v1.3/application-service-api/#legacy-routes">Legacy Routes</seealso>
    public override void Configure()
    {
        this.Get("rooms/{roomAlias}", "_matrix/app/v1/rooms/{roomAlias}");
        this.AllowAnonymous();
    }

    /// <summary>
    /// the handler method for the endpoint. this method is called for each request received. It delegates the work to
    /// the user defined handler and processes its result.
    /// </summary>
    /// <param name="req">the request dto</param>
    /// <param name="ct">a cancellation token</param>
    public override async Task HandleAsync(ExistRoomAliasRequest req, CancellationToken ct)
    {
        var (Code, Error) = await this._options.OnExistRoomAliasQueryAsync(req, ct);

        switch (Code)
        {
            case HttpStatusCode.OK:
                await this.SendOkAsync(ct);
                break;
            case HttpStatusCode.NotFound:
                throw HttpErrors.RoomAliasNotFound(req.RoomAlias, Error);
            default:
                throw new NotImplementedException();
        }
    }
}
