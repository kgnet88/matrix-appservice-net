namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Requests;

/// <summary>
/// dd
/// </summary>
public abstract record BaseRequest
{
    /// <summary>
    /// ddd
    /// </summary>
    [QueryParam, BindFrom("access_token")]
    public string? AccessToken { get; set; }
}
