//-----------------------------------------------------------------------------
// <copyright file="VinoDependencyExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Infrastructure
{
    using Codev.Core.Interface;
    using Codev.Core.Provider.Authentication;
    using Codev.Core.Provider.Banking.Memory;
    using Codev.Core.Provider.Cache;
    using Codev.Core.Provider.Cipher;
    using Codev.Core.Provider.Communication;
    using Codev.Core.Provider.Export;
    using Codev.Core.Repository.Ado;
    using Codev.Core.Service.Asset;
    using Codev.Core.Service.Authentication;
    using Codev.Core.Service.Banking;
    using Codev.Core.Service.Cache;
    using Codev.Core.Service.Cipher;
    using Codev.Core.Service.Clock;
    using Codev.Core.Service.Communication;
    using Codev.Core.Service.Configuration;
    using Codev.Core.Service.Diagnostic;
    using Codev.Core.Service.Export;
    using Codev.Core.Service.Licensing;
    using Codev.Core.Service.Notify;
    using Codev.Core.Service.ScheduledTask;
    using Codev.Core.Service.Serializer;
    using Codev.Core.Service.ServiceLink;
    using Codev.Core.Service.Token;
    using Microsoft.Extensions.DependencyInjection;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This configures the common application IoC dependencies.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class CoreDependencyExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the dependencies. These are the non-configurable
        /// implementations.  The detils of what providers to load is up to 
        /// the running application.
        /// </summary>
        ///--------------------------------------------------------------------
        public static IServiceCollection AddCoreInfrastructure(
            this IServiceCollection services)
        {
            // Add the memory caching.
            //
            services.AddMemoryCache();

            // Add the unit of work for db transactions.
            //
            services.AddScoped<ICoreUnitOfWork, CoreUnitOfWork>();

            // Add the repositories.
            //
            services.AddScoped<IBlobRepository              , BlobRepository>();
            services.AddScoped<IBlobContentRepository       , BlobContentRepository>();
            services.AddScoped<ICommunicationRepository     , CommunicationRepository>();
            services.AddScoped<IDestinationRepository       , DestinationRepository>();
            services.AddScoped<IEnumTypeRepository          , EnumTypeRepository>();
            services.AddScoped<IErrorLogRepository          , ErrorLogRepository>();
            services.AddScoped<IIdentityRepository          , IdentityRepository>();
            services.AddScoped<ILicenseRepository           , LicenseRepository>();
            services.AddScoped<IPaymentMethodRepository     , PaymentMethodRepository>();
            services.AddScoped<IPaymentRepository           , PaymentRepository>();
            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
            services.AddScoped<IScheduledTaskRepository     , ScheduledTaskRepository>();
            services.AddScoped<IServiceLinkRepository       , ServiceLinkRepository>();
            services.AddScoped<ISessionRepository           , SessionRepository>();
            services.AddScoped<ISettingRepository           , SettingRepository>();
            services.AddScoped<ISubscriptionRepository      , SubscriptionRepository>();

            // Add default providers.  These can be overridden if the consumer
            // application adds the same provider interface.
            //
            services.AddScoped<ICacheProvider               , NoCacheProvider>();
            services.AddScoped<ICipherProvider              , DesCipherProvider>();
            services.AddScoped<IAuthenticationProvider      , CodevAuthProvider>();
            services.AddScoped<IEmailClientProvider         , NoSmtpProvider>();
            services.AddScoped<IEmailServerProvider         , NoEmailServerProvider>();
            services.AddScoped<ISmsClientProvider           , NoSmsProvider>();
            services.AddScoped<IExportProvider              , ExportXmlProvider>();
            services.AddScoped<ISecretProvider              , CodevSecretProvider>();
            services.AddScoped<ISubscriptionPlanProvider    , MemorySubscriptionPlanProvider>();
            services.AddScoped<IPaymentProvider             , MemoryPaymentProvider>();
            services.AddScoped<ISubscriptionCustomerProvider, MemorySubscriptionCustomerProvider>();

            // Add the services.
            //
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAssetService         , AssetService>();
            services.AddScoped<ICacheService         , CacheService>();
            services.AddScoped<ICipherService        , CipherService>();
            services.AddScoped<IClockService         , ClockService>();
            services.AddScoped<ICommunicationService , CommunicationService>();
            services.AddScoped<IConfigurationService , ConfigurationService>();
            services.AddScoped<IDiagnosticService    , DiagnosticService>();
            services.AddScoped<IExportService        , ExportService>();
            services.AddScoped<ILicenseService       , LicenseService>();
            services.AddScoped<INotifyService        , NotifyService>();
            services.AddScoped<IPaymentService       , PaymentService>();
            services.AddScoped<IScheduledTaskService , ScheduledTaskService>();
            services.AddScoped<ISerializerService    , SerializerService>();
            services.AddScoped<IServiceLinkService   , ServiceLinkService>();
            services.AddScoped<ITokenService         , TokenService>();

            return services;
        }
        #endregion
    }
}
