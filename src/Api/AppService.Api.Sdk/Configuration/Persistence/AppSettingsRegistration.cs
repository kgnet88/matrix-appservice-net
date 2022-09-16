namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Persistence;

/// <summary>
/// Configuration object for the registration.
/// </summary>
internal sealed record AppSettingsRegistration
{
    /// <summary>
    /// Section name to load via configuration interface.
    /// </summary>
    public const string Section = "Registration";

    /// <summary>
    /// <b>Required</b>: A unique, user-defined ID of the application service which will never change.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: The URL for the application service. May include a path after the domain name.
    /// </summary>
    public string Url { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: A unique token for application services to use to authenticate requests to Homeservers.
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: A unique token for Homeservers to use to authenticate requests to application services.
    /// </summary>
    public string HomeserverToken { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: The localpart of the user associated with the application service.
    /// </summary>
    public string Localpart { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: A list of users, aliases and rooms namespaces that the application service controls.
    /// </summary>
    public AppSettingNamespaces Namespaces { get; set; } = default!;

    /// <summary>
    /// Whether requests from masqueraded users are rate-limited. The sender is excluded.
    /// </summary>
    public bool RateLimited { get; set; } = default!;

    /// <summary>
    /// The external protocols which the application service provides.
    /// </summary>
    public List<string> Protocols { get; set; } = new();
}

/// <summary>
/// Represents all namespaces inside a registration.
/// </summary>
internal sealed class AppSettingNamespaces
{
    /// <summary>
    /// <b>Required</b>: A list of user namespaces with events which are sent from certain users.
    /// </summary>
    public List<AppSettingsNamespace> Users { get; set; } = new();

    /// <summary>
    /// <b>Required</b>: A list of alias namespaces with events which are sent in rooms with certain room aliases.
    /// </summary>
    public List<AppSettingsNamespace> Aliases { get; set; } = new();

    /// <summary>
    /// <b>Required</b>: A list of room namespaces with events which are sent in rooms with certain room IDs.
    /// </summary>
    public List<AppSettingsNamespace> Rooms { get; set; } = new();
}

/// <summary>
/// Represents a namespace inside a registration.
/// </summary>
internal sealed record AppSettingsNamespace
{
    /// <summary>
    /// <b>Required</b>: A true or false value stating whether this application service has exclusive access to events
    /// within this namespace.
    /// </summary>
    public bool Exclusive { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: A regular expression defining which values this namespace includes.
    /// </summary>
    public string Regex { get; set; } = default!;
}