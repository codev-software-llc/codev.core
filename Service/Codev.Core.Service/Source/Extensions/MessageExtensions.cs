//-----------------------------------------------------------------------------
// <copyright file="MessageExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the Communication entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class MessageExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Message ToModel(
            this CommunicationEntity entity)
        {
            return new Message()
                {
                    Id           = entity.Id,
                    Category     = entity.Category,
                    Subcategory  = entity.Subcategory,
                    Name         = entity.Name,
                    EmailAddress = entity.EmailAddress,
                    Comments     = entity.Comments
                };
        }
        #endregion
    }
}
