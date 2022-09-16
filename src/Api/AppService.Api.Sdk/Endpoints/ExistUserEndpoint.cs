namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints;

/// <summary>
/// This endpoint is invoked by the homeserver on an application service to query the existence of a given user ID.
/// <remarks>
/// The homeserver will only query user IDs inside the application service’s <c>users</c> namespace. The homeserver
/// will send this request when it receives an event for an unknown user ID in the application service’s namespace,
/// such as a room invite.
/// </remarks>
/// </summary>
public sealed class ExistUserEndpoint : Endpoint<ExistUserRequest>
{
    /// <summary>
    /// Userdefined and application service specific endpoint options.
    /// </summary>
    private readonly AppServiceEndpointSettings _options;

    /// <summary>
    /// Injects endpoint options to make them available.
    /// </summary>
    /// <param name="options">Injected endpoint options.</param>
    public ExistUserEndpoint(AppServiceEndpointSettings options)
    {
        this._options = options;
    }

    /// <summary>
    /// Configures the neccessary routes for the endpoint.
    /// </summary>
    /// <seealso href="https://spec.matrix.org/v1.3/application-service-api/#get_matrixappv1usersuserid">
    /// Querying API for user IDs
    /// </seealso>
    /// <seealso href="https://spec.matrix.org/v1.3/application-service-api/#legacy-routes">Legacy Routes</seealso>
    public override void Configure()
    {
        this.Get("users/{userId}", "_matrix/app/v1/users/{userId}");
        this.AllowAnonymous();
    }

    /// <summary>
    /// the handler method for the endpoint. this method is called for each request received. It delegates the work to
    /// the user defined handler and processes its result.
    /// </summary>
    /// <param name="req">the request dto.</param>
    /// <param name="ct">a cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public override async Task HandleAsync(ExistUserRequest req, CancellationToken ct)
    {
        var (code, error) = await this._options.OnExistUserQueryAsync(req, ct);

        switch (code)
        {
            case HttpStatusCode.OK:
                await this.SendOkAsync(ct);
                break;
            case HttpStatusCode.NotFound:
                throw HttpErrors.UserNotFound(req.UserId, error);
            default:
                throw new NotImplementedException();
        }
    }
}
