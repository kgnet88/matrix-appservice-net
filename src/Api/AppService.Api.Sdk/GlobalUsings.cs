global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Net;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Threading.Tasks;

global using FastEndpoints;
global using FastEndpoints.Swagger;

global using KgNet88.Matrix.AppService.Api.Sdk.Configuration.Model;
global using KgNet88.Matrix.AppService.Api.Sdk.Contracts.Errors;
global using KgNet88.Matrix.AppService.Api.Sdk.Contracts.Requests;
global using KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Configuration;
global using KgNet88.Matrix.AppService.Api.Sdk.Middleware;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;

global using ValueOf;

global using YamlDotNet.Serialization;

global using ErrorResponse = KgNet88.Matrix.AppService.Api.Sdk.Contracts.Responses.ErrorResponse;
global using FluentValidation.Results;
global using KgNet88.Matrix.AppService.Api.Sdk.Endpoints.Processors;
