namespace KgNet88.Matrix.AppService.Api.Sdk.Contracts.Errors;

/// <summary>
/// This class is a conform implementation for any errors which occur at the Matrix API level and returns a
/// <see href="https://spec.matrix.org/v1.3/client-server-api/#standard-error-response">standard error Exception</see>
/// from which a valid response can be derived.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1194:Implement exception constructors.", Justification = "AllreadyDone")]
[Serializable]
public sealed class HttpException : Exception
{
    /// <summary>
    /// Gets the http status code for the response type.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the error code string will be a unique string which can be used to handle an error message.
    /// </summary>
    public string ErrorCode { get; }

    /// <summary>
    /// Gets the error string will be a human-readable error message, usually a sentence explaining what went wrong.
    /// </summary>
    public string Error => this.Message;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpException"/> class.
    /// Initializes a valid standard exception without user defined error message.
    /// </summary>
    /// <param name="errorCode">The responses error code.</param>
    /// <param name="statusCode">The responses http status code.</param>
    public HttpException(string errorCode, HttpStatusCode statusCode)
        : base(errorCode)
    {
        this.ErrorCode = errorCode;
        this.StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpException"/> class.
    /// Initializes a valid standard exception with user defined error message.
    /// </summary>
    /// <param name="errorCode">The responses error code.</param>
    /// <param name="error">User defined error message.</param>
    /// <param name="statusCode">The responses http status code.</param>
    public HttpException(string errorCode, string error, HttpStatusCode statusCode)
        : base(error)
    {
        this.ErrorCode = errorCode;
        this.StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpException"/> class.
    /// Initializes a valid standard exception with user defined error message and inner exception.
    /// </summary>
    /// <param name="errorCode">The responses error code.</param>
    /// <param name="error">User defined error message.</param>
    /// <param name="statusCode">The responses http status code.</param>
    /// <param name="innerException">Inner exception for more details.</param>
    public HttpException(
        string errorCode,
        string error,
        HttpStatusCode statusCode,
        Exception? innerException)
        : base(error, innerException)
    {
        this.ErrorCode = errorCode;
        this.StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpException"/> class.
    /// Initializes a valid standard Exception by deserialization.
    /// </summary>
    /// <param name="info">Holds the serialized exception data.</param>
    /// <param name="context">The serialization context.</param>
    private HttpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
    {
        this.StatusCode = (HttpStatusCode)info.GetValue(nameof(this.StatusCode), typeof(HttpStatusCode)) !;
        this.ErrorCode = info.GetString(nameof(this.ErrorCode)) !;
    }

    /// <summary>
    /// When overridden in a derived class, sets the <see cref="SerializationInfo" /> with information about the
    /// exception.
    /// </summary>
    /// <param name="info">
    /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
    /// </param>
    /// <exception cref="ArgumentNullException">The <paramref name="info" /> parameter is a null reference.</exception>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException(nameof(info));
        }

        base.GetObjectData(info, context);

        info.AddValue("ErrorCode", this.ErrorCode);
        info.AddValue("StatusCode", this.StatusCode);
    }
}
