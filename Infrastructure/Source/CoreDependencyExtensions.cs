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
            services.AddSingleton<ICoreUnitOfWork, CoreUnitOfWork>();

            // Add the repositories.
            //
            services.AddSingleton<IBlobRepository              , BlobRepository>();
            services.AddSingleton<IBlobContentRepository       , BlobContentRepository>();
            services.AddSingleton<ICommunicationRepository     , CommunicationRepository>();
            services.AddSingleton<IDestinationRepository       , DestinationRepository>();
            services.AddSingleton<IEnumTypeRepository          , EnumTypeRepository>();
            services.AddSingleton<IErrorLogRepository          , ErrorLogRepository>();
            services.AddSingleton<IIdentityRepository          , IdentityRepository>();
            services.AddSingleton<ILicenseRepository           , LicenseRepository>();
            services.AddSingleton<IPaymentMethodRepository     , PaymentMethodRepository>();
            services.AddSingleton<IPaymentRepository           , PaymentRepository>();
            services.AddSingleton<IPaymentTransactionRepository, PaymentTransactionRepository>();
            services.AddSingleton<IScheduledTaskRepository     , ScheduledTaskRepository>();
            services.AddSingleton<IServiceLinkRepository       , ServiceLinkRepository>();
            services.AddSingleton<ISessionRepository           , SessionRepository>();
            services.AddSingleton<ISettingRepository           , SettingRepository>();
            services.AddSingleton<ISubscriptionRepository      , SubscriptionRepository>();

            // Add default providers.  These can be overridden if the consumer
            // application adds the same provider interface.
            //
            services.AddSingleton<ICacheProvider               , NoCacheProvider>();
            services.AddSingleton<ICipherProvider              , DesCipherProvider>();
            services.AddSingleton<IAuthenticationProvider      , CodevAuthProvider>();
            services.AddSingleton<IEmailClientProvider         , NoSmtpProvider>();
            services.AddSingleton<IEmailServerProvider         , NoEmailServerProvider>();
            services.AddSingleton<ISmsClientProvider           , NoSmsProvider>();
            services.AddSingleton<IExportProvider              , ExportXmlProvider>();
            services.AddSingleton<ISecretProvider              , CodevSecretProvider>();
            services.AddSingleton<ISubscriptionPlanProvider    , MemorySubscriptionPlanProvider>();
            services.AddSingleton<IPaymentProvider             , MemoryPaymentProvider>();
            services.AddSingleton<ISubscriptionCustomerProvider, MemorySubscriptionCustomerProvider>();

            // Add the services.
            //
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAssetService         , AssetService>();
            services.AddSingleton<ICacheService         , CacheService>();
            services.AddSingleton<ICipherService        , CipherService>();
            services.AddSingleton<IClockService         , ClockService>();
            services.AddSingleton<ICommunicationService , CommunicationService>();
            services.AddSingleton<IConfigurationService , ConfigurationService>();
            services.AddSingleton<IDiagnosticService    , DiagnosticService>();
            services.AddSingleton<IExportService        , ExportService>();
            services.AddSingleton<ILicenseService       , LicenseService>();
            services.AddSingleton<INotifyService        , NotifyService>();
            services.AddSingleton<IPaymentService       , PaymentService>();
            services.AddSingleton<IScheduledTaskService , ScheduledTaskService>();
            services.AddSingleton<ISerializerService    , SerializerService>();
            services.AddSingleton<IServiceLinkService   , ServiceLinkService>();
            services.AddSingleton<ITokenService         , TokenService>();

            return services;
        }
        #endregion
    }
}
