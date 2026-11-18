//-----------------------------------------------------------------------------
// <copyright file="NotifyService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Notify
{
    using System;
    using System.Threading.Tasks;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the notification service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class NotifyService : INotifyService
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Send email message.
        /// </summary>
        /// -------------------------------------------------------------------
        async Task INotifyService.SendAsync(
            SmtpMessage message)
        {
            Validation.ValidateParameter<SmtpMessage>("message", message);

            try
            {
                await this.SendAsync(message);
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
        void INotifyService.Send(
            SmsMessage smsMessage)
        {
            Validation.ValidateParameter<SmsMessage>("smsMessage", smsMessage);

            try
            {
                this.Send(smsMessage);
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
