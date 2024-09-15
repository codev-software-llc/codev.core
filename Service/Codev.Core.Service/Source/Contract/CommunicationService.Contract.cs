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
        /// Get all the supported communication types (Email, Sms, ...)
        /// </summary>
        /// -------------------------------------------------------------------
        List<String> ICommunicationService.GetCommunicationTypes()
        {
            try
            {
                return this.GetCommunicationTypes();
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
                return this.GetUnprocessed();
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
            Validation.ValidateParameter<String>("application" , application );
            Validation.ValidateParameter<String>("category"    , category    );
            Validation.ValidateParameter<String>("emailAddress", emailAddress);

            subcategory = Validation.ValidateDefault<String>("subcategory", subcategory, String.Empty);
            name        = Validation.ValidateDefault<String>("name"       , name       , String.Empty);
            comments    = Validation.ValidateDefault<String>("comments"   , comments   , String.Empty);

            try
            {
                this.Receive(application, category, subcategory, name, emailAddress, comments);
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
            Message message)
        {
            Validation.ValidateParameter<Message>("communication", message);

            try
            {
                CommunicationEntity communicationEntity = this.CommunicationRepository.GetById(message.Id);

                if (communicationEntity != null)
                {
                    this.SetProcessed(communicationEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Communication entity does not exist");
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
        #endregion
    }
}
