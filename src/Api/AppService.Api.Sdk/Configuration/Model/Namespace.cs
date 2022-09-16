namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;

/// <summary>
/// Strong typed and validated application service namespace.
/// </summary>
internal sealed class Namespace : ValueOf<NamespaceFields, Namespace>
{
    /// <summary>
    /// Tries to validate if the initialization value is a valid application service namespace. If the value is not a
    /// valid namespace, an error is thrown.
    /// </summary>
    /// <exception cref="ArgumentException">Application service namespace is invalid.</exception>
    protected override void Validate()
    {
        if (this.Value.Regex.Length == 0)
        {
            throw new ArgumentException("Application service namespace cannot be empty.", nameof(Namespace));
        }
    }

    /// <summary>
    /// Tries to validate if the initialization value is a valid application service namespace.
    /// </summary>
    /// <returns>True, if the instance is initialized with a valid namespace.</returns>
    protected override bool TryValidate()
    {
        return this.Value.Regex != string.Empty;
    }
}

/// <summary>
/// Helper struct to simplify handlyng of namespaces.
/// </summary>
public struct NamespaceFields
{
    /// <summary>
    /// <b>Required</b>: A true or false value stating whether this application service has exclusive access to events
    /// within this namespace.
    /// </summary>
    public bool Exclusive { get; set; }

    /// <summary>
    /// <b>Required</b>: A regular expression defining which values this namespace includes.
    /// </summary>
    public string Regex { get; set; }
}