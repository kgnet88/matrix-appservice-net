namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Errors;

/// <summary>
/// List of possible
/// <see href="https://spec.matrix.org/v1.3/client-server-api/#standard-error-response">error codes</see>, produced by
/// the application services endpoints.
/// </summary>
public enum HttpErrorCodes
{
    /// <summary>
    /// The endpoint is not implemented by the application service.
    /// </summary>
    APP_SERVICE_SDK_NOT_IMPLEMENTED = 0,

    /// <summary>
    /// The homeserver has not supplied credentials to the application service. Optional error information can be
    /// included in the body of this response.
    /// </summary>
    APP_SERVICE_NOT_AUTHORIZED = 1,

    /// <summary>
    /// The credentials supplied by the homeserver were rejected.
    /// </summary>
    APP_SERVICE_FORBIDDEN = 2,

    /// <summary>
    /// The application service indicates that this user does not exist. Optional error information can be included in
    /// the body of this response.
    /// </summary>
    APP_SERVICE_ROOM_ALIAS_NOT_FOUND = 3,

    /// <summary>
    /// The application service indicates that this room alias does not exist. Optional error information can be
    /// included in the body of this response.
    /// </summary>
    APP_SERVICE_USER_NOT_FOUND = 4
}

/// <summary>
/// Extensions for <see cref="HttpErrorCodes" /> to convert enum values to
/// <see href="https://spec.matrix.org/v1.3/client-server-api/#standard-error-response">error codes</see>.
/// </summary>
public static class HttpErrorCodesExtensions
{
    /// <summary>
    /// Converts the predefined error codes in matrix protocol valid
    /// <see href="https://spec.matrix.org/v1.3/client-server-api/#standard-error-response">error code strings</see>.
    /// </summary>
    /// <param name="code">The given error code.</param>
    /// <returns>The error code string for given code.</returns>
    public static string FromCode(this HttpErrorCodes code)
    {
        return code switch
        {
            HttpErrorCodes.APP_SERVICE_SDK_NOT_IMPLEMENTED => "APP_SERVICE_SDK_NOT_IMPLEMENTED",
            HttpErrorCodes.APP_SERVICE_ROOM_ALIAS_NOT_FOUND => "APP_SERVICE_ROOM_ALIAS_NOT_FOUND",
            HttpErrorCodes.APP_SERVICE_USER_NOT_FOUND => "APP_SERVICE_USER_NOT_FOUND",
            HttpErrorCodes.APP_SERVICE_NOT_AUTHORIZED => "APP_SERVICE_NOT_AUTHORIZED",
            HttpErrorCodes.APP_SERVICE_FORBIDDEN => "M_FORBIDDEN",
            _ => throw new NotImplementedException()
        };
    }
}
