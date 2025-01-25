//-----------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Reflection;
    using System.Text.Json;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Provides common extensions for various objects.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class ObjectExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert to a reference type.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Reference<T> AsReference<T>(
            this Int32 id)
        {
            return new Reference<T>(id);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to a reference.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Reference<T> AsReference<T>(
            this BaseEntity entity)
        {
            return new Reference<T>(entity.Id);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will deep clone an object.
        /// </summary>
        ///--------------------------------------------------------------------
        public static T Clone<T>(
            this T item)
        {
            String serialized = JsonSerializer.Serialize(item);

            return JsonSerializer.Deserialize<T>(serialized);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will copy the properties from the object to another object
        /// of the same type.  This does not clone, but sets the properties
        /// in-place.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void CopyTo(
            this Object source,
                 Object destination)
        {
            Type destinationType = destination.GetType();

            PropertyInfo[] properties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pi in properties)
            {
                PropertyInfo destinationProperty = destinationType.GetProperty(pi.Name);

                if ((destinationProperty != null) && destinationProperty.CanWrite && pi.PropertyType.IsAssignableFrom(pi.PropertyType))
                {
                    destinationProperty.SetValue(destination, pi.GetValue(source));
                }
            }
        }
        #endregion
    }
}
