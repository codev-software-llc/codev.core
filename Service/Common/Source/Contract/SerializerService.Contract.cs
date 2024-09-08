//-----------------------------------------------------------------------------
// <copyright file="SerializerService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Json serializer.  This common 
    /// service makes sure our serializer works for NodaTime or any other
    /// common Codev dependency.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class SerializerService : ISerializerService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Serialize object into a string.
        /// </summary>
        ///--------------------------------------------------------------------
        String ISerializerService.Serialize<T>(
            T item)
        {
            try
            {
                return this.Serialize<T>(item);
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

        ///--------------------------------------------------------------------
        /// <summary>
        /// Deserialize the string into an object.
        /// </summary>
        ///--------------------------------------------------------------------
        T ISerializerService.Deserialize<T>(
            String serializedObject)
        {
            try
            {
                return this.Deserialize<T>(serializedObject);
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
