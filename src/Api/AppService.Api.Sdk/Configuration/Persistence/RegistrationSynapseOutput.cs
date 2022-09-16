namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Persistence;

/// <summary>
/// Configuration object for persisting the registration.
/// </summary>
internal sealed class RegistrationSynapseOutput
{
    /// <summary>
    /// Section name to load via configuration interface.
    /// </summary>
    public const string Section = "SynapseOutput";

    /// <summary>
    /// If true, the application services registration will be persisted.
    /// </summary>
    public bool DoPersist { get; set; } = default!;

    /// <summary>
    /// The path where the registration should be persisted.
    /// </summary>
    public string Path { get; set; } = default!;
}
