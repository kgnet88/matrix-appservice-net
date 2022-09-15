namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Services;

/// <summary>
/// Interface for a service that can store and load a application services registration as a yaml file.
/// </summary>
public interface IRegistrationService
{
    /// <summary>
    /// The service tries to load a valid application service registration from the yaml file at the specified path.
    /// </summary>
    /// <param name="path">The application service registrations path.</param>
    /// <returns>If successful it returns the loaded application service registration.</returns>
    public Task<Registration?> LoadRegistrationAsync(string path);

    /// <summary>
    /// The service tries to save the given application service registration as yaml file under the given path.
    /// </summary>
    /// <param name="registration">The registration to be saved.</param>
    /// <param name="path">The application service registrations path.</param>
    public Task WriteRegistrationAsync(Registration registration, string path);

    /// <summary>
    /// Generates a new random token, which can be used as access or homeserver token.
    /// </summary>
    /// <returns>A random token string.</returns>
    public static abstract string GenerateToken();
}
