namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Persistence;

/// <summary>
/// SDKs default implementation of the registration service.
/// </summary>
internal static class RegistrationService
{
    /// <summary>
    /// The service tries to load a valid application service registration from the yaml file at the specified path.
    /// </summary>
    /// <param name="path">The application service registrations path.</param>
    /// <returns>If successful it returns the loaded application service registration.</returns>
    public static Registration? LoadRegistrationFromFile(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        string fileInput = File.ReadAllText(path);
        var input = new StringReader(fileInput);

        var deserializer = new DeserializerBuilder().Build();

        var yamlRegistration = deserializer.Deserialize<YamlRegistration>(input);

        return yamlRegistration is null
            ? null
            : new Registration
            {
                Id = ApplicationId.From(yamlRegistration.Id),
                Url = Url.From(yamlRegistration.Url),
                AccessToken = Token.From((TokenType.Access, yamlRegistration.AccessToken)),
                HomeserverToken = Token.From((TokenType.Homeserver, yamlRegistration.HomeserverToken)),
                Localpart = SenderLocalpart.From(yamlRegistration.Localpart),
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
    /// The service tries to load a valid application service registration from the configuration file.
    /// </summary>
    /// <param name="configuration">A valid application services configuration.</param>
    /// <returns>If successful it returns the loaded application service registration.</returns>
    public static Registration? LoadRegistrationFromConfiguration(IConfiguration configuration)
    {
        var appServiceRegistration = new AppSettingsRegistration();
        var section = configuration.GetSection(AppSettingsRegistration.Section);

        if (section is null)
        {
            return null;
        }

        section.Bind(appServiceRegistration);

        return new Registration
        {
            Id = ApplicationId.From(appServiceRegistration.Id),
            Url = Url.From(appServiceRegistration.Url),
            AccessToken = Token.From((TokenType.Access, appServiceRegistration.AccessToken)),
            HomeserverToken = Token.From((TokenType.Homeserver, appServiceRegistration.HomeserverToken)),
            Localpart = SenderLocalpart.From(appServiceRegistration.Localpart),
            Users = appServiceRegistration.Namespaces.Users.ConvertAll(
            x => Namespace.From(new NamespaceFields { Exclusive = x.Exclusive, Regex = x.Regex })),
            Aliases = appServiceRegistration.Namespaces.Aliases.ConvertAll(
            x => Namespace.From(new NamespaceFields { Exclusive = x.Exclusive, Regex = x.Regex })),
            Rooms = appServiceRegistration.Namespaces.Rooms.ConvertAll(
            x => Namespace.From(new NamespaceFields { Exclusive = x.Exclusive, Regex = x.Regex })),
            RateLimited = appServiceRegistration.RateLimited,
            Protocols = appServiceRegistration.Protocols.ConvertAll(Protocol.From)
        };
    }

    /// <summary>
    /// The service tries to save the given application service registration as yaml file under the given path.
    /// </summary>
    /// <param name="registration">The registration to be saved.</param>
    /// <param name="path">The application service registrations path.</param>
    public static void WriteRegistrationToFile(Registration registration, string path)
    {
        var yamlRegistration = new YamlRegistration
        {
            Id = registration.Id.Value,
            Url = registration.Url.Value,
            AccessToken = registration.AccessToken.Value.Token,
            HomeserverToken = registration.HomeserverToken.Value.Token,
            Localpart = registration.Localpart.Value,
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

        File.WriteAllText(path, yaml);
    }

    /// <summary>
    /// Generates a new random token, which can be used as access or homeserver token.
    /// </summary>
    /// <returns>A random token string.</returns>
    public static string GenerateToken()
    {
        return (Guid.NewGuid() + Guid.NewGuid().ToString()).Replace("-", string.Empty);
    }
}
