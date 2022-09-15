namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Errors;

/// <summary>
/// Static helper functions to create exceptions for standard
/// <see href="https://spec.matrix.org/v1.3/client-server-api/#standard-error-response">error codes</see>.
/// </summary>
public static class HttpErrors
{
    /// <summary>
    /// Helper to generate exception for not implemented endpoints.
    /// </summary>
    /// <param name="endpointName">The name of the not implemented endpoint.</param>
    /// <returns>Exception with right error code, status code and message.</returns>
    public static HttpException NotImplemented(string endpointName)
    {
        return new HttpException(
            HttpErrorCodes.APP_SERVICE_SDK_NOT_IMPLEMENTED.FromCode(),
            $"Endpoint {endpointName} not implemented",
            HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Helper to generate exception for a missing access token.
    /// </summary>
    /// <returns>Exception with right error code, status code and message.</returns>
    public static HttpException NotAuthorized()
    {
        return new HttpException(
            HttpErrorCodes.APP_SERVICE_NOT_AUTHORIZED.FromCode(),
            "Missing access token.",
            HttpStatusCode.Unauthorized);
    }

    /// <summary>
    /// Helper to generate exception for a invalid access token.
    /// </summary>
    /// <returns>Exception with right error code, status code and message.</returns>
    public static HttpException Forbidden()
    {
        return new HttpException(
            HttpErrorCodes.APP_SERVICE_FORBIDDEN.FromCode(),
            "Invalid access token.",
            HttpStatusCode.Forbidden);
    }

    /// <summary>
    /// Helper to generate exception for missing room alias.
    /// </summary>
    /// <param name="roomAlias">The requested room alias.</param>
    /// <param name="errorMessage">Specialized error message (optional).</param>
    /// <returns>Exception with right error code, status code and message.</returns>
    public static HttpException RoomAliasNotFound(string roomAlias, string? errorMessage = null)
    {
        return new HttpException(
            HttpErrorCodes.APP_SERVICE_ROOM_ALIAS_NOT_FOUND.FromCode(),
            errorMessage ?? $"Room alias {roomAlias} not found",
            HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Helper to generate exception for missing user ID.
    /// </summary>
    /// <param name="userId">The requested user ID.</param>
    /// <param name="errorMessage">Specialized error message (optional).</param>
    /// <returns>Exception with right error code, status code and message.</returns>
    public static HttpException UserNotFound(string userId, string? errorMessage = null)
    {
        return new HttpException(
            HttpErrorCodes.APP_SERVICE_USER_NOT_FOUND.FromCode(),
            errorMessage ?? $"User {userId} not found",
            HttpStatusCode.NotFound);
    }
}
