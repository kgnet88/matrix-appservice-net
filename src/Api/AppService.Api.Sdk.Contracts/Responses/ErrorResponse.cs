namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Responses;

/// <summary>
/// Implements a
/// <see href="https://spec.matrix.org/v1.3/client-server-api/#standard-error-response">standard error response</see>.
/// </summary>
public sealed record ErrorResponse
{
    /// <summary>
    /// The errcode string will be a unique string which can be used to handle an error message.
    /// </summary>
    [JsonPropertyName("errcode")]
    public required string ErrorCode { get; set; }

    /// <summary>
    /// The error string will be a human-readable error message, usually a sentence explaining what went wrong.
    /// </summary>
    [JsonPropertyName("error")]
    public required string ErrorMessage { get; set; }
}
