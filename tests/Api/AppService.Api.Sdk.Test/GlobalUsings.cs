global using System.Net;
global using System.Net.Http.Json;

global using FluentAssertions;

global using KgNet88.Matrix.AppService.Api.Example;
global using KgNet88.Matrix.AppService.Api.Sdk.Configuration;
global using KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;
global using KgNet88.Matrix.AppService.Api.Sdk.Contracts.Errors;
global using KgNet88.Matrix.AppService.Api.Sdk.Contracts.Responses;
global using KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Configuration;

global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.DependencyInjection;

global using Xunit;

global using AppId = KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model.ApplicationId;
global using KgNet88.Matrix.AppService.Api.Sdk.Contracts.Requests;
