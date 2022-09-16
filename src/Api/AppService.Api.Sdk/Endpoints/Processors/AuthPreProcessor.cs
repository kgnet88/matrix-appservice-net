namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Processors;

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

    public Task PreProcessAsync(object req, HttpContext ctx, List<ValidationFailure> failures, CancellationToken ct)
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