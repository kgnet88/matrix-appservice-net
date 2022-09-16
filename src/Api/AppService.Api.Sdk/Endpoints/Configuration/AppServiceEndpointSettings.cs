namespace KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Configuration;

/// <summary>
/// Options to proper initialize all endpoints.
/// </summary>
public sealed class AppServiceEndpointSettings
{
    /// <summary>
    /// Delegate to implement your own handler for the application services query for
    /// <see href="https://spec.matrix.org/v1.3/application-service-api/#get_matrixappv1roomsroomalias">room aliases</see>.
    /// <list type="bullet">
    /// <item>
    /// The first parameter is the <see cref="ExistRoomAliasRequest" />.
    /// </item>
    /// <item>
    /// The second parameter is the <see cref="CancellationToken" />.
    /// </item>
    /// </list>
    /// </summary>
    public Func<ExistRoomAliasRequest, CancellationToken, Task<(HttpStatusCode, string?)>> OnExistRoomAliasQueryAsync { get; set; }
        = (_, _) => throw HttpErrors.NotImplemented("ExistRoomAlias");

    /// <summary>
    /// Delegate to implement your own handler for the application services query for
    /// <see href="https://spec.matrix.org/v1.3/application-service-api/#get_matrixappv1usersuserid">user IDs</see>.
    /// <list type="bullet">
    /// <item>
    /// The first parameter is the <see cref="ExistUserRequest" />.
    /// </item>
    /// <item>
    /// The second parameter is the <see cref="CancellationToken" />.
    /// </item>
    /// </list>
    /// </summary>
    public Func<ExistUserRequest, CancellationToken, Task<(HttpStatusCode, string?)>> OnExistUserQueryAsync { get; set; }
        = (_, _) => throw HttpErrors.NotImplemented("ExistUser");

    /// <summary>
    /// Delegate to implement your own handler for the application services
    /// <see href="https://spec.matrix.org/v1.3/application-service-api/#pushing-events">transaction API</see>.
    /// <list type="bullet">
    /// <item>
    /// The first parameter is the <see cref="PushEventsRequest" />.
    /// </item>
    /// <item>
    /// The second parameter is the transaction ID <c>txnID</c>.
    /// </item>
    /// <item>
    /// The last parameter is the <see cref="CancellationToken" />.
    /// </item>
    /// </list>
    /// <example>
    /// Example implementation:
    /// <code>
    /// (request, txnId, _) =>
    /// {
    ///     Console.WriteLine($"Transaction ID: {txnId}");
    ///     Console.WriteLine($"Number of events: {request.ClientEvents.Count}");
    ///     return Task.CompletedTask;
    /// };
    /// </code>
    /// </example>
    /// </summary>
    public Func<PushEventsRequest, long, CancellationToken, Task> OnPushEventsQueryAsync { get; set; }
        = (_, _, _) => throw HttpErrors.NotImplemented("PushEvents");
}
