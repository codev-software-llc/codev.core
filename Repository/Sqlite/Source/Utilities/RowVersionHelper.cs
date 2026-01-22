//-----------------------------------------------------------------------------
// <copyright file="RowVersion.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides a helper utility to increment row versions.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class RowVersionHelper
    {
        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Update the row versions to the entity.
        /// </summary>
        ///---------------------------------------------------------------
        public static void UpdateIdentifier(
            IDataSource dataSource,
            BaseEntity  entity)
        {
            SqliteAccess.CallStatement(
                "select last_insert_rowid() as 'RowId'",
                dataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity.ResetEntityState(reader.GetIdentifier<Int32>("RowId"));
                        }
                    });
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Increment row version.
        /// </summary>
        ///---------------------------------------------------------------
        public static Byte[] Increment(
            Byte[] rowVersion)
        {
            Byte v1 = rowVersion[0];
            Byte v2 = rowVersion[1];
            Byte v3 = rowVersion[2];
            Byte v4 = rowVersion[3];

            if (v4 == 255)
            {
                if (v3 == 255)
                {
                    if (v2 == 255)
                    {
                        v1 = 0;
                        v2 = 0;
                        v3 = 0;
                        v4 = 1;
                    }
                    else
                    {
                        v2++;
                    }
                }
                else
                {
                    v3++;
                }
            }
            else
            {
                v4++;
            }

            return new Byte[] { v1, v2, v3, v4 };
        }
        #endregion
    }
}
