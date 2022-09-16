namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Services;

/// <summary>
/// Configuration file to register an application service on the homeserver.
/// </summary>
internal sealed class YamlRegistration
{
    /// <summary>
    /// <b>Required</b>: A unique, user-defined ID of the application service which will never change.
    /// </summary>
    [YamlMember(Alias = "id", ApplyNamingConventions = false)]
    public string Id { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: The URL for the application service. May include a path after the domain name.
    /// </summary>
    [YamlMember(Alias = "url", ApplyNamingConventions = false)]
    public string Url { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: A unique token for application services to use to authenticate requests to Homeservers.
    /// </summary>
    [YamlMember(Alias = "as_token", ApplyNamingConventions = false)]
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: A unique token for Homeservers to use to authenticate requests to application services.
    /// </summary>
    [YamlMember(Alias = "hs_token", ApplyNamingConventions = false)]
    public string HomeserverToken { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: The localpart of the user associated with the application service.
    /// </summary>
    [YamlMember(Alias = "sender_localpart", ApplyNamingConventions = false)]
    public string Localpart { get; set; } = default!;

    /// <summary>
    /// <b>Required</b>: A list of users, aliases and rooms namespaces that the application service controls.
    /// </summary>
    [YamlMember(Alias = "namespaces", ApplyNamingConventions = false)]
    public YamlNamespaces Namespaces { get; set; } = default!;

    /// <summary>
    /// Whether requests from masqueraded users are rate-limited. The sender is excluded.
    /// </summary>
    [YamlMember(Alias = "rate_limited", ApplyNamingConventions = false)]
    public bool RateLimited { get; set; }

    /// <summary>
    /// The external protocols which the application service provides.
    /// </summary>
    [YamlMember(Alias = "protocols", ApplyNamingConventions = false)]
    public List<string> Protocols { get; set; } = default!;
}

internal sealed class YamlNamespace
{
    /// <summary>
    /// <b>Required</b>: A true or false value stating whether this application service has exclusive access to events
    /// within this namespace.
    /// </summary>
    [YamlMember(Alias = "exclusive", ApplyNamingConventions = false)]
    public bool Exclusive { get; set; }

    /// <summary>
    /// <b>Required</b>: A regular expression defining which values this namespace includes.
    /// </summary>
    [YamlMember(Alias = "regex", ApplyNamingConventions = false)]
    public string Regex { get; set; } = default!;
}

internal sealed class YamlNamespaces
{
    /// <summary>
    /// Events which are sent from certain users.
    /// </summary>
    [YamlMember(Alias = "users", ApplyNamingConventions = false)]
    public List<YamlNamespace> Users { get; set; } = default!;

    /// <summary>
    /// Events which are sent in rooms with certain room aliases.
    /// </summary>
    [YamlMember(Alias = "aliases", ApplyNamingConventions = false)]
    public List<YamlNamespace> Aliases { get; set; } = default!;

    /// <summary>
    /// Events which are sent in rooms with certain room IDs.
    /// </summary>
    [YamlMember(Alias = "rooms", ApplyNamingConventions = false)]
    public List<YamlNamespace> Rooms { get; set; } = default!;
}