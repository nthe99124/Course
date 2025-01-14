﻿using Amazon.S3;
using Blazorise;
using CourseProject.Common.Cache;
using CourseProject.Common.MiddleWare;
using CourseProject.Model.ViewModel;
using CourseProject.Services;
using CourseProject.Services.Base;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Http.Features;
using BlazorBootstrap;
using Blazorise.Bootstrap;

#region Config service
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Khởi tạo IConfiguration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

services.AddSingleton(configuration);

#region Razor page config
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddHttpClient();
services.AddWMBSC();
services.AddBlazorise().AddBlazorBootstrap().AddBootstrapProviders();
services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

#endregion

#region Localize

services.AddSingleton<IHtmlLocalizerFactory, HtmlLocalizerFactory>();
services.AddLocalization(options => options.ResourcesPath = "Resources");

// config thêm ngôn ngữ mới
var supportedCultures = new[]
{
    new CultureInfo("vi-VN"),
    new CultureInfo("en-US"),
};
services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("vi-VN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
    };
});
#endregion

#region Config AutoMapper
//services.AddAutoMapper(typeof(Startup));
#endregion

#region Config http requets
// cấu hình phiên 
services.AddHttpContextAccessor();
#endregion

#region Config Service
services.AddScoped(typeof(IResponseOutput<>), typeof(ResponseOutput<>));
services.AddScoped<IBaseService, BaseService>();
services.AddScoped<IAccountService, AccountService>();
services.AddScoped<ICourseService, CourseService>();
services.AddScoped<ITagService, TagService>();
services.AddScoped<ILessionService, LessionService>();
services.AddScoped<IChapterService, ChapterService>();
services.AddScoped<IFileService, FileService>();
#endregion  



#region Config cache
//TODO: hiện tại đang làm cache distributed => sau có cần cache khác thì custom thêm
services.AddDistributedMemoryCache();
services.AddSingleton<IDistributedCacheCustom, DistributedCacheCustom>();
#endregion

#endregion

#region Run service pipleline
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapWhen(ctx => !ctx.Request.Path.StartsWithSegments("/api"), client =>
{
    client.UseStaticFiles();
    client.UseRouting();
    client.UseEndpoints(endpoints =>
    {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
    });
});


#region Localize
app.UseRequestLocalization();
app.UseMiddleware<LanguageMiddleware>();
#endregion

app.Run();
#endregion