//-----------------------------------------------------------------------------
// <copyright file="EnumTypeExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the EnumType entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class EnumTypeExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static EnumType ToModel(
            this EnumTypeEntity entity)
        {
            return new EnumType()
                {
                    Id          = entity.Id,
                    Application = entity.Application,
                    IsBig       = entity.IsBig,
                    IsFlag      = entity.IsFlag,
                    EnumKey     = entity.EnumKey,
                    Name        = entity.Name,
                    Value       = entity.Value,
                    Comment     = entity.Comment
                };
        }
        #endregion
    }
}
