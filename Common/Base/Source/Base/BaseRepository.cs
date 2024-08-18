//-----------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Base
{
    using System;
    using System.Data;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is a base class for storing common information across repositories.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BaseRepository
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the access.
        /// </summary>
        ///--------------------------------------------------------------------
        protected BaseRepository(
            IDataSource dataSource)
        {
            this.DataSource = dataSource;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the data source for the repository.
        /// </summary>
        ///--------------------------------------------------------------------
        public IDataSource DataSource { get; private set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Reset the entity version.
        /// </summary>
        ///--------------------------------------------------------------------
        protected void UpdateEntityRow(
            IDataReader reader,
            BaseEntity  entity)
        {
            Int32  rowId      = reader.GetIdentifier<Int32>("RowId");
            Byte[] rowVersion = reader.GetValue<Byte[]>    ("RowVersion");

            entity.ResetEntityState(rowId, rowVersion);
        }
        #endregion
    }
}
