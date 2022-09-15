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
    /// Injects a delegate to process the current request.
    /// </summary>
    /// <param name="request">Delegate for the current request.</param>
    public HttpExceptionMiddleware(RequestDelegate request)
    {
        this._request = request;
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
            await context.Response.WriteAsJsonAsync(new ErrorResponse { ErrorCode = exception.ErrorCode, ErrorMessage = exception.Error });
        }
    }
}