namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Processors;

internal sealed class AuthPreProcessor : IGlobalPreProcessor
{
    /// <summary>
    /// Userdefined and application service specific endpoint options.
    /// </summary>
    private readonly AppServiceEndpointOptions _options;

    /// <summary>
    /// Injects endpoint options to make them available.
    /// </summary>
    /// <param name="options">Injected endpoint options.</param>
    public AuthPreProcessor(AppServiceEndpointOptions options)
    {
        this._options = options;
    }

    public Task PreProcessAsync(object req, HttpContext ctx, List<ValidationFailure> failures, CancellationToken ct)
    {
        if (req is BaseRequest request)
        {
            if (request.AccessToken is null)
            {
                throw HttpErrors.NotAuthorized();
            }

            if (request.AccessToken != this._options.HomeserverToken.Value.Token)
            {
                throw HttpErrors.Forbidden();
            }
        }

        return Task.CompletedTask;
    }
}