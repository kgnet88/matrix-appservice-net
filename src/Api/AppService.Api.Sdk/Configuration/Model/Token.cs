namespace KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;

/// <summary>
/// Strong typed and validated access or homeserver token.
/// </summary>
internal sealed class Token : ValueOf<(TokenType Type, string Token), Token>
{
    /// <summary>
    /// Tries to validate if the initialization value is a valid homeserver or access token. If the value is not a
    /// valid token, an error is thrown.
    /// </summary>
    /// <exception cref="ArgumentException">Given token is invalid.</exception>
    protected override void Validate()
    {
        if (this.Value.Token.Length == 0)
        {
            string tokenType = this.Value.Type == TokenType.Access ? "Access" : "Homeserver";
            throw new ArgumentException($"{tokenType} token cannot be empty.", nameof(Token));
        }
    }

    /// <summary>
    /// Tries to validate if the initialization value is a valid homeserver or access token.
    /// </summary>
    /// <returns>True, if the instance is initialized with a valid token.</returns>
    protected override bool TryValidate()
    {
        return this.Value.Token != string.Empty;
    }
}
