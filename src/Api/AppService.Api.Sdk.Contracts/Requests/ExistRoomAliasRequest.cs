namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Requests;

/// <summary>
/// This request is used by the homeserver on an application service to query the existence of a given room alias.
/// </summary>
/// <seealso href="https://spec.matrix.org/v1.3/application-service-api/#get_matrixappv1roomsroomalias">
/// Querying API for room aliases
/// </seealso>
public sealed record ExistRoomAliasRequest : IRequest
{
    /// <summary>
    /// Homeservers MUST include a query parameter named <c>access_token</c> containing the <c>hs_token</c> from the
    /// application service’s registration when making requests to the application service.
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// <b>Required:</b> The room alias being queried.
    /// </summary>
    [BindFrom("roomAlias")]
    public string RoomAlias { get; init; } = default!;
}
