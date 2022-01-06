#region Using directives
using Blazorise;
using Blazorise.AntDesign;
using Blazorise.Bootstrap;
using Blazorise.Bootstrap5;
using Blazorise.Modules;
using Dignite.Abp.BlobStoringManagement.Components.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
#endregion

namespace Dignite.Abp.BlobStoringManagement.Components
{
    public static class Config
    {

        /// <summary>
        /// Adds a ant design providers and component mappings.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDigniteAntDesignProviders(this IServiceCollection serviceCollection, Action<IClassProvider> configureClassProvider = null)
        {
            var classProvider = new AntDesignClassProvider();

            configureClassProvider?.Invoke(classProvider);
            serviceCollection.AddSingleton<IClassProvider>(classProvider);
            serviceCollection.AddSingleton<IStyleProvider, AntDesignStyleProvider>();
            serviceCollection.AddScoped<IThemeGenerator, AntDesignThemeGenerator>();
            var components = Blazorise.AntDesign.Config.ComponentMap;
            components[typeof(FileEditPlus)] = typeof(Components.Platform.AntDesign.FileEditPlus);
            foreach (var mapping in components)
            {
                serviceCollection.AddTransient(mapping.Key, mapping.Value);
            }

            serviceCollection.AddScoped<IJSModalModule, Blazorise.AntDesign.Modules.AntDesignJSModalModule>();
            serviceCollection.AddScoped<IJSTooltipModule, Blazorise.AntDesign.Modules.AntDesignJSTooltipModule>();

            return serviceCollection;
        }

        /// <summary>
        /// Adds a Bootstrap and component mappings.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDigniteBootstrapProviders(this IServiceCollection serviceCollection, Action<IClassProvider> configureClassProvider = null)
        {
            var classProvider = new Blazorise.Bootstrap.BootstrapClassProvider();

            configureClassProvider?.Invoke(classProvider);
            serviceCollection.AddSingleton<IClassProvider>(classProvider);
            serviceCollection.AddSingleton<IStyleProvider, BootstrapStyleProvider>();
            serviceCollection.AddScoped<IThemeGenerator, Blazorise.Bootstrap.BootstrapThemeGenerator>();
            var components = Blazorise.Bootstrap.Config.ComponentMap;
            components[typeof(FileEditPlus)] = typeof(Components.Platform.Bootstrap.FileEditPlus);
            foreach (var mapping in components)
            {
                serviceCollection.AddTransient(mapping.Key, mapping.Value);
            }

            serviceCollection.AddScoped<IJSModalModule, Blazorise.Bootstrap.Modules.BootstrapJSModalModule>();
            serviceCollection.AddScoped<IJSTooltipModule, Blazorise.Bootstrap.Modules.BootstrapJSTooltipModule>();

            return serviceCollection;
        }

        /// <summary>
        /// Adds a BootStrap5 providers and component mappings.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDigniteBootStrap5Providers(this IServiceCollection serviceCollection, Action<IClassProvider> configureClassProvider = null)
        {
            var classProvider = new Blazorise.Bootstrap5.BootstrapClassProvider();

            configureClassProvider?.Invoke(classProvider);
            serviceCollection.AddSingleton<IClassProvider>(classProvider);
            serviceCollection.AddSingleton<IStyleProvider, Bootstrap5StyleProvider>();
            serviceCollection.AddScoped<IThemeGenerator, Blazorise.Bootstrap5.BootstrapThemeGenerator>();
            var components = Blazorise.Bootstrap5.Config.ComponentMap;
            components[typeof(FileEditPlus)] = typeof(Platform.Bootstrap5.FileEditPlus);
            foreach (var mapping in components)
            {
                serviceCollection.AddTransient(mapping.Key, mapping.Value);
            }

            serviceCollection.AddScoped<IJSModalModule, Blazorise.Bootstrap5.Modules.BootstrapJSModalModule>();
            serviceCollection.AddScoped<IJSTooltipModule, Blazorise.Bootstrap5.Modules.BootstrapJSTooltipModule>();

            return serviceCollection;
        }

        /// <summary>
        /// Adds a Material providers and component mappings.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDigniteMaterialProviders(this IServiceCollection serviceCollection, Action<IClassProvider> configureClassProvider = null)
        {
            var classProvider = new Blazorise.Material.MaterialClassProvider();

            configureClassProvider?.Invoke(classProvider);
            serviceCollection.AddSingleton<IClassProvider>(classProvider);
            serviceCollection.AddSingleton<IStyleProvider, Blazorise.Material.MaterialStyleProvider>();
            serviceCollection.AddScoped<IThemeGenerator, Blazorise.Material.MaterialThemeGenerator>();
            var components = Blazorise.Material.Config.ComponentMap;
            components[typeof(FileEditPlus)] = typeof(Components.Platform.Material.FileEditPlus);
            foreach (var mapping in components)
            {
                serviceCollection.AddTransient(mapping.Key, mapping.Value);
            }

            serviceCollection.AddScoped<IJSModalModule, Blazorise.Material.Modules.MaterialJSModalModule>();
            serviceCollection.AddScoped<IJSTooltipModule, Blazorise.Material.Modules.MaterialJSTooltipModule>();

            return serviceCollection;
        }

    }
}
