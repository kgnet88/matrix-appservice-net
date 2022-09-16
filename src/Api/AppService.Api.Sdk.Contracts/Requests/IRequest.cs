namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Requests;

/// <summary>
/// Interface for all requests to ensure the authorization process between homeserver and application service.
/// </summary>
public interface IRequest
{
    /// <summary>
    /// Homeservers MUST include a query parameter named <c>access_token</c> containing the <c>hs_token</c> from the
    /// application service’s registration when making requests to the application service.
    /// </summary>
    [QueryParam, BindFrom("access_token")]
    public string AccessToken { get; set; }
}
