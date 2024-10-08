﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskManager.Abstractions.Email;
using TaskManager.Abstractions.Services;
using TaskManager.Infrastructure.Email;

namespace TaskManager.Infrastructure.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IEmailSender, EmailSender>();
        return services;
    }
}