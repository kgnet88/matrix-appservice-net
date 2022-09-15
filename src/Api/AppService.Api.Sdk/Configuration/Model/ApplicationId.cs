namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;

/// <summary>
/// Strong typed and validated application service Id.
/// </summary>
public sealed class ApplicationId : ValueOf<string, ApplicationId>
{
    /// <summary>
    /// Tries to validate if the initialization value is a valid application service id. If the value is not a valid id,
    /// an error is thrown.
    /// </summary>
    /// <exception cref="ArgumentException">Application service id is invalid.</exception>
    protected override void Validate()
    {
        if (this.Value?.Length == 0)
        {
            throw new ArgumentException("Application service id cannot be empty.", nameof(ApplicationId));
        }
    }

    /// <summary>
    /// Tries to validate if the initialization value is a valid application service id.
    /// </summary>
    /// <returns>True, if the instance is initialized with a valid id.</returns>
    protected override bool TryValidate()
    {
        return this.Value != string.Empty;
    }
}
