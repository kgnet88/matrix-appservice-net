namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;

/// <summary>
/// Strong typed and validated application service url.
/// </summary>
public sealed class Url : ValueOf<string, Url>
{
    /// <summary>
    /// Tries to validate if the initialization value is a valid application service url. If the value is not a valid
    /// url, an error is thrown.
    /// </summary>
    /// <exception cref="ArgumentException">Application service url is invalid.</exception>
    protected override void Validate()
    {
        if (this.Value?.Length == 0)
        {
            throw new ArgumentException("Application service url cannot be empty.", nameof(Url));
        }
    }

    /// <summary>
    /// Tries to validate if the initialization value is a valid application service url.
    /// </summary>
    /// <returns>True, if the instance is initialized with a valid url.</returns>
    protected override bool TryValidate()
    {
        return this.Value != string.Empty;
    }
}
