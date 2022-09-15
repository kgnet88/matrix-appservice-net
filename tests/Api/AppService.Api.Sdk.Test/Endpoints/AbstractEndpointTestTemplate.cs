namespace KgNet88.Matrix.AppService.Api.Sdk.Test.Endpoints;

public abstract class AbstractEndpointTestTemplate : IClassFixture<TestApplicationFactory<ExampleAppService>>
{
    protected readonly TestApplicationFactory<ExampleAppService> _application;
    protected static string AccessToken => "Nc-T87ejp0mFtmFeQg2RQAEYUsFy11ekKH4znqQ7OAzw";
    protected static string TestUser => "user";
    protected static string TestRoom => "room";

    protected AbstractEndpointTestTemplate(TestApplicationFactory<ExampleAppService> application)
    {
        this._application = application;
    }

    protected HttpClient CreateClient()
    {
        return this._application.CreateClient();
    }
}
