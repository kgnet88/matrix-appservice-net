namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;

/// <summary>
/// Strong typed and validated application service localpart.
/// </summary>
internal sealed class SenderLocalpart : ValueOf<string, SenderLocalpart>
{
    /// <summary>
    /// Tries to validate if the initialization value is a valid application service localpart. If the value is not a
    /// valid localpart, an error is thrown.
    /// </summary>
    /// <exception cref="ArgumentException">Application service localpart is invalid.</exception>
    protected override void Validate()
    {
        if (this.Value?.Length == 0)
        {
            throw new ArgumentException("Application service sender localpart cannot be empty.", nameof(SenderLocalpart));
        }
    }

    /// <summary>
    /// Tries to validate if the initialization value is a valid application service localpart.
    /// </summary>
    /// <returns>True, if the instance is initialized with a valid localpart.</returns>
    protected override bool TryValidate()
    {
        return this.Value != string.Empty;
    }
}
