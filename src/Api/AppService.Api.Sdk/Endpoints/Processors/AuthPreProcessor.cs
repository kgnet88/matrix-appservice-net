namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Processors;

/// <summary>
/// Preprocessor to authorize valid homeserver requests.
/// </summary>
internal sealed class AuthPreProcessor : IGlobalPreProcessor
{
    /// <summary>
    /// Userdefined and application service specific endpoint options.
    /// </summary>
    private readonly Registration _registration;

    /// <summary>
    /// Injects registration to make them available.
    /// </summary>
    /// <param name="registration">Injected endpoint options.</param>
    public AuthPreProcessor(Registration registration)
    {
        this._registration = registration;
    }

    /// <summary>
    /// The preprocessor validates the requests query parameter <c>access_token</c> to authorize it.
    /// </summary>
    /// <param name="request">The current request.</param>
    /// <param name="ctx">The current http context.</param>
    /// <param name="failures">A list of all current validation failures.</param>
    /// <param name="ct">The current cancellation token.</param>
    public Task PreProcessAsync(object request, HttpContext ctx, List<ValidationFailure> failures, CancellationToken ct)
    {
        if (!ctx.Request.Query.ContainsKey("access_token"))
        {
            throw HttpErrors.NotAuthorized();
        }

        string token = ctx.Request.Query["access_token"].ToString();

        return token != this._registration.HomeserverToken.Value.Token
            ? throw HttpErrors.Forbidden()
            : Task.CompletedTask;
    }
}