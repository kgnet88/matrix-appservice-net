namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Requests;

/// <summary>
/// A homeservers request to push an event (or batch of events) to the application service.
/// </summary>
/// <seealso href="https://spec.matrix.org/v1.3/application-service-api/#pushing-events">
/// Transaction API for sending a list of events
/// </seealso>
public sealed record PushEventsRequest : BaseRequest
{
    /// <summary>
    /// <b>Required</b>: A list of events, formatted as per the Client-Server API.
    /// </summary>
    [JsonPropertyName("events")]
    public List<ClientEvent> ClientEvents { get; set; } = new();
}

/// <summary>
/// Details for an event, formatted as per the Client-Server API.
/// </summary>
public sealed record ClientEvent
{
    /// <summary>
    /// <b>Required</b>: The body of this event, as created by the client which sent it.
    /// </summary>
    [JsonPropertyName("content")]
    public object Content { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: The globally unique identifier for this event.
    /// </summary>
    [JsonPropertyName("event_id")]
    public string EventId { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: Timestamp (in milliseconds since the unix epoch) on originating homeserver when this event was
    /// sent.
    /// </summary>
    [JsonPropertyName("origin_server_ts")]
    public long OriginServerTs { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: The ID of the room associated with this event.
    /// </summary>
    [JsonPropertyName("room_id")]
    public string RoomId { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: Contains the fully-qualified ID of the user who sent this event.
    /// </summary>
    [JsonPropertyName("sender")]
    public string Sender { get; set; } = default!;

    /// <summary>
    /// <para>
    /// Present if, and only if, this event is a state event. The key making this piece of state unique in the room.
    /// Note that it is often an empty string.
    /// </para>
    /// <para>
    /// State keys starting with an @ are reserved for referencing user IDs, such as room members. With the exception
    /// of a few events, state events set with a given user’s ID as the state key MUST only be set by that user.
    /// </para>
    /// </summary>
    [JsonPropertyName("state_key")]
    public string StateKey { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: The type of the event.
    /// </summary>
    [JsonPropertyName("type")]
    public string EventType { get; set; } = default!;

    /// <summary>
    /// Contains optional extra information about the event.
    /// </summary>
    [JsonPropertyName("unsigned")]
    public UnsingedData Unsigned { get; set; } = default!;
}

/// <summary>
/// Contains optional extra information about the event.
/// </summary>
public sealed record UnsingedData
{
    /// <summary>
    ///  The time in milliseconds that has elapsed since the event was sent.
    /// </summary>
    /// <remarks>
    /// This field is generated by the local homeserver, and may be incorrect if the local time on at least one of
    /// the two servers is out of sync, which can cause the age to either be negative or greater than it actually is.
    /// </remarks>
    [JsonPropertyName("age")]
    public long Age { get; set; } = default!;

    /// <summary>
    /// The previous content for this event.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This field is generated by the local homeserver, and is only returned if the event is a state event, and the
    /// client has permission to see the previous content.
    /// </para>
    /// <para>
    /// <b>Changed in v1.2:</b> Previously, this field was specified at the top level of returned events rather than in
    /// unsigned (with the exception of the GET .../notifications endpoint), though in practice no known server
    /// implementations honoured this.
    /// </para>
    /// </remarks>
    [JsonPropertyName("prev_content")]
    public object PreviousContent { get; set; } = default!;

    /// <summary>
    /// The event that redacted this event, if any.
    /// </summary>
    [JsonPropertyName("redacted_because")]
    public ClientEvent RedactedBecause { get; set; } = default!;

    /// <summary>
    /// The client-supplied transaction ID, for example, provided via
    /// <c>PUT /_matrix/client/v3/rooms/{roomId}/send/{eventType}/{txnId}</c>,
    /// if the client being given the event is the same one which sent it.
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = default!;
}
