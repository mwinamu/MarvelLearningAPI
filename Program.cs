using System.Text.Json;
using Marten;
using MarvelLearningAPI.Extensions;
using MarvelLearningAPI.Infrastructure;
using MarvelLearningAPI.Infrastructure.Interface;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.BuilderServices();

var app = builder.Build();
app.WebApplicationContainer();



