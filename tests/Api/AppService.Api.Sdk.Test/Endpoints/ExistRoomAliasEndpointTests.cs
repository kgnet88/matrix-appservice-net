namespace KgNet88.Matrix.AppService.Api.Sdk.Test.Endpoints;

[Collection("Sequential")]
public class ExistRoomAliasEndpointTests : AbstractEndpointTestTemplate
{
    public ExistRoomAliasEndpointTests(TestApplicationFactory<ExampleAppService> application) : base(application)
    {
    }

    // happy path test (status: 200)
    [Fact]
    public async Task GET_room_alias_OK_200_Async()
    {
        var client = this.CreateClient();
        var response = await client.GetAsync($"_matrix/app/v1/rooms/{TestRoom}?access_token={AccessToken}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        response = await client.GetAsync($"rooms/{TestRoom}?access_token={AccessToken}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // unauthorized failure test (status: 401)
    [Fact]
    public async Task GET_room_alias_Unauthorized_401_Async()
    {
        var client = this.CreateClient();
        var response = await client.GetAsync($"_matrix/app/v1/rooms/{TestRoom}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_NOT_AUTHORIZED.FromCode());
        _ = result!.ErrorMessage.Should().Be("Missing access token.");

        response = await client.GetAsync($"rooms/{TestRoom}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_NOT_AUTHORIZED.FromCode());
        _ = result!.ErrorMessage.Should().Be("Missing access token.");
    }

    // invalid credentials failure test (status: 403)
    [Fact]
    public async Task GET_room_alias_Forbidden_403_Async()
    {
        var client = this.CreateClient();
        var response = await client.GetAsync($"_matrix/app/v1/rooms/{TestRoom}?access_token={AccessToken + "2"}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_FORBIDDEN.FromCode());
        _ = result!.ErrorMessage.Should().Be("Invalid access token.");

        response = await client.GetAsync($"rooms/{TestRoom}?access_token={AccessToken + "2"}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

        result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_FORBIDDEN.FromCode());
        _ = result!.ErrorMessage.Should().Be("Invalid access token.");
    }

    // alias does not exists failure test (status: 404)
    [Fact]
    public async Task GET_room_alias_NotFound_404_Async()
    {
        var client = this.CreateClient();
        var response = await client.GetAsync($"_matrix/app/v1/rooms/{TestRoom + "2"}?access_token={AccessToken}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_ROOM_ALIAS_NOT_FOUND.FromCode());
        _ = result!.ErrorMessage.Should().Be("room alias does not exist, try again");

        response = await client.GetAsync($"rooms/{TestRoom + "2"}?access_token={AccessToken}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        result = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        _ = result!.ErrorCode.Should().Be(HttpErrorCodes.APP_SERVICE_ROOM_ALIAS_NOT_FOUND.FromCode());
        _ = result!.ErrorMessage.Should().Be("room alias does not exist, try again");
    }
}
