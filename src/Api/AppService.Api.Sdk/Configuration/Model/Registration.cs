namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;

/// <summary>
/// Configuration file to register an application service on the homeserver.
/// </summary>
internal sealed class Registration
{
    /// <summary>
    /// <b>Required</b>: A unique, user-defined ID of the application service which will never change.
    /// </summary>
    public required ApplicationId Id { get; init; }

    /// <summary>
    /// <b>Required</b>: The URL for the application service. May include a path after the domain name.
    /// </summary>
    public required Url Url { get; init; }

    /// <summary>
    /// <b>Required</b>: A unique token for application services to use to authenticate requests to Homeservers.
    /// </summary>
    public required Token AccessToken { get; init; }

    /// <summary>
    /// <b>Required</b>: A unique token for Homeservers to use to authenticate requests to application services.
    /// </summary>
    public required Token HomeserverToken { get; init; }

    /// <summary>
    /// <b>Required</b>: The localpart of the user associated with the application service.
    /// </summary>
    public required SenderLocalpart Localpart { get; init; }

    /// <summary>
    /// <b>Required</b>: A list of user namespaces with events which are sent from certain users.
    /// </summary>
    public required List<Namespace> Users { get; init; }

    /// <summary>
    /// <b>Required</b>: A list of alias namespaces with events which are sent in rooms with certain room aliases.
    /// </summary>
    public required List<Namespace> Aliases { get; init; }

    /// <summary>
    /// <b>Required</b>: A list of room namespaces with events which are sent in rooms with certain room IDs.
    /// </summary>
    public required List<Namespace> Rooms { get; init; }

    /// <summary>
    /// Whether requests from masqueraded users are rate-limited. The sender is excluded.
    /// </summary>
    public bool RateLimited { get; init; }

    /// <summary>
    /// The external protocols which the application service provides.
    /// </summary>
    public List<Protocol> Protocols { get; init; } = new();
}
