namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;

/// <summary>
/// Strong typed and validated application service protocol.
/// </summary>
internal sealed class Protocol : ValueOf<string, Protocol>
{
    /// <summary>
    /// Tries to validate if the initialization value is a valid application service protocol. If the value is not a
    /// valid protocol, an error is thrown.
    /// </summary>
    /// <exception cref="ArgumentException">Application service protocol is invalid.</exception>
    protected override void Validate()
    {
        if (this.Value?.Length == 0)
        {
            throw new ArgumentException("Application service protocol cannot be empty.", nameof(Protocol));
        }
    }

    /// <summary>
    /// Tries to validate if the initialization value is a valid application service protocol.
    /// </summary>
    /// <returns>True, if the instance is initialized with a valid protocol.</returns>
    protected override bool TryValidate()
    {
        return this.Value != string.Empty;
    }
}