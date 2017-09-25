using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Api
{
    using Todo.Domain.Interfaces;
    using Todo.Domain.Services;
    using Todo.Data.Interfaces;
    using Todo.Data.Context.MemoryContext.Interfaces;
    using Todo.Data.Context.MemoryContext;
    using Todo.Data.Context.MemoryContext.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    /// <summary>
    /// setup dependency injection for the api
    /// </summary>
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            services.AddTransient<IContextConfiguration, ApiContextConfiguration>();
            //for this in-memory example we us singleton, but a perisistent store should be per request, implement unit of work, etc
            services.AddSingleton<IMemoryContext, MemoryContext>();
            services.AddTransient<IListItemRepository,ListItemRepository>();
            services.AddTransient<IListRepository,ListRepository>();
            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<IUserServices,UserServices>();
            services.AddTransient<IListServices,ListServices>();
            services.AddTransient<IListItemServices,ListItemServices>();
        }
    }
}