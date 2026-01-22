// ----------------------------------------------------------------------------
// <copyright file="CommunicationService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
// ----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using Codev.Core.Service.Clock;
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
            IClockService            clockService,
            ICommunicationRepository communicationRepository) : base(dataSource, clockService)
        {
            Validation.ValidateParameter<ICommunicationRepository>("communicationRepository", communicationRepository);

            this.CommunicationRepository = communicationRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the communication repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private ICommunicationRepository CommunicationRepository { get; set; }
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
            CommunicationEntity communicationEntity)
        {
            communicationEntity.Flags |= CommunicationFlags.Processed;

            communicationEntity.DateModified = this.ClockService.GetCurrentInstant();

            this.CommunicationRepository.Update(communicationEntity);
        }
        #endregion
    }
}
