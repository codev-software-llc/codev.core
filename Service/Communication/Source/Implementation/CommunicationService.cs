// ----------------------------------------------------------------------------
// <copyright file="CommunicationService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
// ----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;
    using Codev.Core.Service.Common;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ICommunicationService for inbound and outbound
    /// communications.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class CommunicationService : BaseService, ICommunicationService
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the email client.
        /// </summary>
        ///--------------------------------------------------------------------
        public CommunicationService(
            ICoreDataSource          dataSource,
            ICommunicationRepository communicationRepository,
            IEmailClientProvider     emailClientProvider,
            ISmsClientProvider       smsClientProvider,
            IEmailServerProvider     emailServerProvider,
            IClockService            clockService) : base(dataSource)
        {
            Validation.ValidateParameter<ICommunicationRepository>("communicationRepository", communicationRepository);
            Validation.ValidateParameter<ISmsClientProvider>      ("smsClientProvider"      , smsClientProvider      );
            Validation.ValidateParameter<IEmailClientProvider>    ("emailClientProvider"    , emailClientProvider    );
            Validation.ValidateParameter<IEmailServerProvider>    ("emailServerProvider"    , emailServerProvider    );
            Validation.ValidateParameter<IClockService>           ("clockService"           , clockService           );

            this.CommunicationRepository = communicationRepository;
            this.SmsClientProvider       = smsClientProvider;
            this.EmailClientProvider     = emailClientProvider;
            this.EmailServerProvider     = emailServerProvider;
            this.ClockService            = clockService;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email address for system email.
        /// </summary>
        ///--------------------------------------------------------------------
        public String SystemEmailAddress
        {
            get
            {
                return this.EmailClientProvider.SystemEmailAddress;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set  system email name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String SystemEmailName
        {
            get
            {
                return this.EmailClientProvider.SystemEmailName;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email failing threshold.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 EmailFailingThreshold
        {
            get
            {
                return this.EmailClientProvider.EmailFailingThreshold;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email client provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private IEmailClientProvider EmailClientProvider { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the sms client provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private ISmsClientProvider SmsClientProvider { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email server provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private IEmailServerProvider EmailServerProvider { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the communication repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private ICommunicationRepository CommunicationRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the clock service.
        /// </summary>
        ///---------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Receive a communication message.
        /// </summary>
        /// -------------------------------------------------------------------
        public void Receive(
            String application,
            String category,
            String subcategory,
            String name,
            String emailAddress,
            String comments)
        {
            Instant instant = this.ClockService.GetCurrentInstant();
            
             CommunicationEntity entity = new CommunicationEntity(instant)
                {
                    Flags        = CommunicationFlags.None,
                    Application  = application,
                    Category     = category,
                    Subcategory  = subcategory,
                    Name         = name,
                    EmailAddress = emailAddress,
                    Comments     = comments
                };
            
            this.CommunicationRepository.Add(entity);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Receive all unprocessed communications.
        /// </summary>
        /// -------------------------------------------------------------------
        public List<Message> GetUnprocessed()
        {
            EntityCollection<CommunicationEntity> entities = this.CommunicationRepository.GetAll();

            return entities.Where(x => (x.Flags & CommunicationFlags.Processed) == 0).Select(y => y.ToModel()).ToList();
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get all the supported communication types (Email, Sms, ...)
        /// </summary>
        /// -------------------------------------------------------------------
        public List<String> GetCommunicationTypes()
        {
            IEnumerable<String> types = Enum.GetNames<CommunicationType>().AsEnumerable<String>();

            return new List<String>(types);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// This will mark a communication as processed so that it does not 
        /// show up in the GetUnprocessed() call.
        /// </summary>
        /// -------------------------------------------------------------------
        public void SetProcessed(
            Message communication)
        {
            CommunicationEntity communicationEntity = this.CommunicationRepository.GetById(communication.Id);

            if (communicationEntity != null)
            {
                communicationEntity.Flags |= CommunicationFlags.Processed;

                communicationEntity.DateModified = this.ClockService.GetCurrentInstant();

                this.CommunicationRepository.Update(communicationEntity);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Communication does not exist");
            }
        }

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
            this.EmailClientProvider.Send(message);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Send sms message.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Send(
            SmsMessage message)
        {
            this.SmsClientProvider.Send(message);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Retrieve email messages from server.
        /// </summary>
        /// -------------------------------------------------------------------
        public List<PopMessage> GetBouncedMessages(
            Boolean deleteAfterFetch,
            String  senderAddressFilter)
        {
            IEnumerable<PopMessage> messages = this.EmailServerProvider.GetBouncedMessages(deleteAfterFetch, senderAddressFilter);

            return new List<PopMessage>(messages);
        }
        #endregion
    }
}
