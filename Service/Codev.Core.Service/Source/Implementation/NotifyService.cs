// ----------------------------------------------------------------------------
// <copyright file="NotifyService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
// ----------------------------------------------------------------------------
namespace Codev.Core.Service.Notify
{
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using Codev.Core.Service.Clock;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ICommunicationService for inbound and outbound
    /// communications.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class NotifyService : INotifyService
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the email client.
        /// </summary>
        ///--------------------------------------------------------------------
        public NotifyService(
            IEmailClientProvider emailClientProvider,
            ISmsClientProvider   smsClientProvider,
            IClockService        clockService)
        {
            Validation.ValidateParameter<ISmsClientProvider>  ("smsClientProvider"  , smsClientProvider  );
            Validation.ValidateParameter<IEmailClientProvider>("emailClientProvider", emailClientProvider);
            Validation.ValidateParameter<IClockService>       ("clockService"       , clockService       );

            this.SmsProvider   = smsClientProvider;
            this.EmailProvider = emailClientProvider;
            this.ClockService  = clockService;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email client provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private IEmailClientProvider EmailProvider { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the sms client provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private ISmsClientProvider SmsProvider { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the clock service.
        /// </summary>
        ///---------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Send email.  This is a synchronous operation and as such is 
        /// blocking.  It is recommended that email is asynchronously 
        /// controlled from the outside caller.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Send(
            SmtpMessage message)
        {
            this.EmailProvider.Send(message);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Send sms message.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Send(
            SmsMessage message)
        {
            this.SmsProvider.Send(message);
        }
        #endregion
    }
}
