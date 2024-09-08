//-----------------------------------------------------------------------------
// <copyright file="ExportService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Export
{
    using System;
    using System.Data;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains the explicit interface hooks that we will use to perform
    /// call validation on the parameters.  Each of these methods will in-turn
    /// call the actual implementations.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ExportService : IExportService
    {
        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Convert the table into the format specified.
        /// </summary>
        ///---------------------------------------------------------------
        Byte[] IExportService.Convert(
            DataTable source,
            String    format)
        {
            try
            {
                Validation.ValidateParameter<DataTable>("source", source);
                Validation.ValidateParameter<String>   ("format", format);

                Byte[] value = new Byte[] { };

                using (this.UnitOfWork.Begin())
                {
                    value = this.Convert(source, format);

                    this.UnitOfWork.Commit();
                }

                return value;
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

        ///---------------------------------------------------------------
        /// <summary>
        /// Convert the table into the format specified using a
        /// stylesheet.
        /// </summary>
        ///---------------------------------------------------------------
        Byte[] IExportService.Convert(
            DataTable source,
            String    format,
            String    styleSheet)
        {
            try
            {
                Validation.ValidateParameter<DataTable>("source"    , source    );
                Validation.ValidateParameter<String>   ("format"    , format    );
                Validation.ValidateParameter<String>   ("styleSheet", styleSheet);

                Byte[] value = new Byte[] { };

                using (this.UnitOfWork.Begin())
                {
                    value = this.Convert(source, format, styleSheet);

                    this.UnitOfWork.Commit();
                }

                return value;
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
