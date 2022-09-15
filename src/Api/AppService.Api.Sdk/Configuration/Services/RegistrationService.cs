namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Services;

/// <summary>
/// SDKs default implementation of the registration service.
/// </summary>
public sealed class RegistrationService : IRegistrationService
{
    /// <summary>
    /// The service tries to load a valid application service registration from the yaml file at the specified path.
    /// </summary>
    /// <param name="path">The application service registrations path.</param>
    /// <returns>If successful it returns the loaded application service registration.</returns>
    public async Task<Registration?> LoadRegistrationAsync(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        string fileInput = await File.ReadAllTextAsync(path);
        var input = new StringReader(fileInput);

        var deserializer = new DeserializerBuilder().Build();

        var yamlRegistration = deserializer.Deserialize<YamlRegistration>(input);

        return yamlRegistration is null
            ? null
            : new Registration
            {
                Id = Model.ApplicationId.From(yamlRegistration.Id),
                Url = Url.From(yamlRegistration.Url),
                AccessToken = Token.From((TokenType.Access, yamlRegistration.AccessToken)),
                HomeserverToken = Token.From((TokenType.Homeserver, yamlRegistration.HomeserverToken)),
                Localpart = SenderLocalpart.From(yamlRegistration.LocalPart),
                Users = yamlRegistration.Namespaces.Users.ConvertAll(
                x => Namespace.From(new NamespaceFields { Exclusive = x.Exclusive, Regex = x.Regex })),
                Aliases = yamlRegistration.Namespaces.Aliases.ConvertAll(
                x => Namespace.From(new NamespaceFields { Exclusive = x.Exclusive, Regex = x.Regex })),
                Rooms = yamlRegistration.Namespaces.Rooms.ConvertAll(
                x => Namespace.From(new NamespaceFields { Exclusive = x.Exclusive, Regex = x.Regex })),
                RateLimited = yamlRegistration.RateLimited,
                Protocols = yamlRegistration.Protocols.ConvertAll(Protocol.From)
            };
    }

    /// <summary>
    /// The service tries to save the given application service registration as yaml file under the given path.
    /// </summary>
    /// <param name="registration">The registration to be saved.</param>
    /// <param name="path">The application service registrations path.</param>
    public async Task WriteRegistrationAsync(Registration registration, string path)
    {
        var yamlRegistration = new YamlRegistration
        {
            Id = registration.Id.Value,
            Url = registration.Url.Value,
            AccessToken = registration.AccessToken.Value.Token,
            HomeserverToken = registration.HomeserverToken.Value.Token,
            LocalPart = registration.Localpart.Value,
            Namespaces = new YamlNamespaces
            {
                Users = registration.Users.ConvertAll(x => new YamlNamespace { Exclusive = x.Value.Exclusive, Regex = x.Value.Regex }),
                Aliases = registration.Aliases.ConvertAll(x => new YamlNamespace { Exclusive = x.Value.Exclusive, Regex = x.Value.Regex }),
                Rooms = registration.Rooms.ConvertAll(x => new YamlNamespace { Exclusive = x.Value.Exclusive, Regex = x.Value.Regex }),
            },
            RateLimited = registration.RateLimited,
            Protocols = registration.Protocols.ConvertAll(x => x.Value)
        };

        var serializer = new SerializerBuilder().Build();
        string yaml = serializer.Serialize(yamlRegistration);

        await File.WriteAllTextAsync(path, yaml);
    }

    /// <summary>
    /// Generates a new random token, which can be used as access or homeserver token.
    /// </summary>
    /// <returns>A random token string.</returns>
    public static string GenerateToken()
    {
        return (Guid.NewGuid() + Guid.NewGuid().ToString()).Replace("-", "");
    }
}
