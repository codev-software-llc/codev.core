//-----------------------------------------------------------------------------
// <copyright file="VinoDependencyExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Infrastructure
{
    using Codev.Core.Common.Interface;
    using Codev.Core.Provider;
    using Codev.Core.Provider.Banking.Memory;
    using Codev.Core.Repository.Ado;
    using Codev.Core.Service.Asset;
    using Codev.Core.Service.Banking;
    using Codev.Core.Service.Cache;
    using Codev.Core.Service.Cipher;
    using Codev.Core.Service.Common;
    using Codev.Core.Service.Communication;
    using Codev.Core.Service.Export;
    using Codev.Core.Service.Licensing;
    using Codev.Core.Service.Task;
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
            services.AddSingleton<IBlobService          , BlobService>();
            services.AddSingleton<ICacheService         , CacheService>();
            services.AddSingleton<ICipherService        , CipherService>();
            services.AddSingleton<IClockService         , ClockService>();
            services.AddSingleton<ICommunicationService , CommunicationService>();
            services.AddSingleton<IDiagnosticService    , DiagnosticService>();
            services.AddSingleton<IExportService        , ExportService>();
            services.AddSingleton<IConfigurationService , ConfigurationService>();
            services.AddSingleton<IGoogleService        , GoogleService>();
            services.AddSingleton<ILicenseService       , LicenseService>();
            services.AddSingleton<IPaymentService       , PaymentService>();
            services.AddSingleton<IScheduledTaskService , ScheduledTaskService>();
            services.AddSingleton<ISerializerService    , SerializerService>();
            services.AddSingleton<IServiceLinkService   , ServiceLinkService>();

            return services;
        }
        #endregion
    }
}
