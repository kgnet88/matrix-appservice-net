namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;

/// <summary>
/// Type of a given unique token.
/// </summary>
internal enum TokenType
{
    /// <summary>
    /// Given token is unique token for a homeserver.
    /// </summary>
    Homeserver,

    /// <summary>
    /// Given token is unique access token.
    /// </summary>
    Access
}
