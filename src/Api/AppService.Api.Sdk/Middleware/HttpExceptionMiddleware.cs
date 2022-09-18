namespace KgNet88.Matrix.AppService.Api.Sdk.Middleware;

/// <summary>
/// Middleware to process
/// <see href="https://spec.matrix.org/v1.3/client-server-api/#standard-error-response">standard error Exceptions</see>.
/// If one is cought, a standard error response is generated.
/// </summary>
internal sealed class HttpExceptionMiddleware
{
    /// <summary>
    /// Delegate for the current request.
    /// </summary>
    private readonly RequestDelegate _request;

    /// <summary>
    /// Application services registration.
    /// </summary>
    private readonly Registration _registration;

    /// <summary>
    /// Injects a delegate to process the current request.
    /// </summary>
    /// <param name="request">Delegate for the current request.</param>
    /// <param name="registration">Application services registration.</param>
    public HttpExceptionMiddleware(RequestDelegate request, Registration registration)
    {
        this._request = request;
        this._registration = registration;
    }

    /// <summary>
    /// Catches <see cref="HttpException" /> and generates a conform
    /// <see href="https://spec.matrix.org/v1.3/client-server-api/#standard-error-response">standard error response</see>.
    /// </summary>
    /// <param name="context">The current requests http context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this._request(context);
        }
        catch (HttpException exception)
        {
            context.Response.StatusCode = (int)exception.StatusCode;

            string appNamespace = this._registration.Url.Value
                .Split(":")[1]
                .Replace("//", string.Empty)
                .Replace('.', '_')
                .ToUpper();

            string errorCode = exception.ErrorCode.Replace("APP_SERVICE", appNamespace);

            await context.Response.WriteAsJsonAsync(new ErrorResponse { ErrorCode = errorCode, ErrorMessage = exception.Error });
        }
    }
}