//-----------------------------------------------------------------------------
// <copyright file="CommunicationService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Communication Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class CommunicationService : ICommunicationService
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Receive a communication message.
        /// </summary>
        /// -------------------------------------------------------------------
        void ICommunicationService.Receive(
            String application,
            String category,
            String subcategory,
            String name,
            String emailAddress,
            String comments)
        {
            try
            {
                Validation.ValidateParameter<String>("application" , application );
                Validation.ValidateParameter<String>("category"    , category    );
                Validation.ValidateParameter<String>("emailAddress", emailAddress);

                subcategory = Validation.ValidateDefault<String>("subcategory", subcategory, String.Empty);
                name        = Validation.ValidateDefault<String>("name"       , name       , String.Empty);
                comments    = Validation.ValidateDefault<String>("comments"   , comments   , String.Empty);

                using (this.UnitOfWork.Begin())
                {
                    this.Receive(application, category, subcategory, name, emailAddress, comments);

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Retrieve all the unprocessed communications.
        /// </summary>
        /// -------------------------------------------------------------------
        List<Message> ICommunicationService.GetUnprocessed()
        {
            try
            {
                List<Message> communications = new List<Message>();

                using (this.UnitOfWork.Begin())
                {
                    communications = this.GetUnprocessed();

                    this.UnitOfWork.Commit();
                }

                return communications;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get all the supported communication types (Email, Sms, ...)
        /// </summary>
        /// -------------------------------------------------------------------
        List<String> ICommunicationService.GetCommunicationTypes()
        {
            try
            {
                List<String> types = new List<String>();

                using (this.UnitOfWork.Begin())
                {
                    types = this.GetCommunicationTypes();

                    this.UnitOfWork.Commit();
                }

                return types;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Mark the communication as processed.
        /// </summary>
        /// -------------------------------------------------------------------
        void ICommunicationService.SetProcessed(
            Message communication)
        {
            try
            {
                Validation.ValidateParameter<Message>("communication", communication);

                using (this.UnitOfWork.Begin())
                {
                    this.SetProcessed(communication);

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Send email message.
        /// </summary>
        /// -------------------------------------------------------------------
        void ICommunicationService.Send(
            SmtpMessage emailMessage)
        {
            try
            {
                Validation.ValidateParameter<SmtpMessage>("emailMessage", emailMessage);

                using (this.UnitOfWork.Begin())
                {
                    this.Send(emailMessage);

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Send sms message.
        /// </summary>
        /// -------------------------------------------------------------------
        void ICommunicationService.Send(
            SmsMessage smsMessage)
        {
            try
            {
                Validation.ValidateParameter<SmsMessage>("smsMessage", smsMessage);

                using (this.UnitOfWork.Begin())
                {
                    this.Send(smsMessage);

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Retrieve email messages from server that have bounced.
        /// </summary>
        /// -------------------------------------------------------------------
        List<PopMessage> ICommunicationService.GetBouncedMessages(
            Boolean deleteAfterFetch,
            String  senderAddressFilter)
        {
            try
            {
                Validation.ValidateParameter<String>("senderAddressFilter", senderAddressFilter);

                List<PopMessage> messages = new List<PopMessage>();

                using (this.UnitOfWork.Begin())
                {
                    messages = this.GetBouncedMessages(deleteAfterFetch, senderAddressFilter);

                    this.UnitOfWork.Commit();
                }

                return messages;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
