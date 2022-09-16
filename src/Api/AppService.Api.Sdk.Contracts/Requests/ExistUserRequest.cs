namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Requests;

/// <summary>
/// This request is used by the homeserver on an application service to query the existence of a given user ID.
/// </summary>
/// <seealso href="https://spec.matrix.org/v1.3/application-service-api/#get_matrixappv1usersuserid">
/// Querying API for user IDs
/// </seealso>
public sealed record ExistUserRequest : IRequest
{
    /// <summary>
    /// Homeservers MUST include a query parameter named <c>access_token</c> containing the <c>hs_token</c> from the
    /// application service’s registration when making requests to the application service.
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// <b>Required:</b> The user ID being queried.
    /// </summary>
    [BindFrom("userId")]
    public string UserId { get; init; } = default!;
}
