namespace KgNet88.Matrix.AppService.Api.Sdk.Test.Endpoints;

[Collection("Sequential")]
public sealed class PushEventsEndpointTest : AbstractEndpointTestTemplate
{
    public PushEventsEndpointTest(TestApplicationFactory<ExampleAppService> application) : base(application)
    {
    }

    // happy path test (status: 200)
    [Fact]
    public async Task PUT_events_OK_200_Async()
    {
        var client = this.CreateClient();

        var response = await client.PutAsJsonAsync(
            $"_matrix/app/v1/transactions/122?access_token={AccessToken}",
            new PushEventsRequest
            {
                ClientEvents = new List<ClientEvent>()
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        response = await client.PutAsJsonAsync(
            $"transactions/122?access_token={AccessToken}",
            new PushEventsRequest
            {
                ClientEvents = new List<ClientEvent>()
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // unauthorized failure test (status: 401)
    [Fact]
    public async Task PUT_events_Unauthorized_401_Async()
    {
        var client = this.CreateClient();
        var response = await client.PutAsJsonAsync(
            "_matrix/app/v1/transactions/122",
            new PushEventsRequest
            {
                ClientEvents = new List<ClientEvent>()
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_NOT_AUTHORIZED.FromCode());
        _ = result!.ErrorMessage.Should().Be("Missing access token.");

        response = await client.PutAsJsonAsync(
            "transactions/122",
            new PushEventsRequest
            {
                ClientEvents = new List<ClientEvent>()
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_NOT_AUTHORIZED.FromCode());
        _ = result!.ErrorMessage.Should().Be("Missing access token.");
    }

    // invalid credentials failure test (status: 403)
    [Fact]
    public async Task PUT_events_Forbidden_403_Async()
    {
        var client = this.CreateClient();
        var response = await client.PutAsJsonAsync(
            $"_matrix/app/v1/transactions/122?access_token={AccessToken + 2}",
            new PushEventsRequest
            {
                ClientEvents = new List<ClientEvent>()
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_FORBIDDEN.FromCode());
        _ = result!.ErrorMessage.Should().Be("Invalid access token.");

        response = await client.PutAsJsonAsync(
            $"transactions/122?access_token={AccessToken + 2}",
            new PushEventsRequest
            {
                ClientEvents = new List<ClientEvent>()
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

        result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_FORBIDDEN.FromCode());
        _ = result!.ErrorMessage.Should().Be("Invalid access token.");
    }
}
