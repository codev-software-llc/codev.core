//-----------------------------------------------------------------------------
// <copyright file="IImageProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
   // using System.Drawing.Imaging;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the interface for image manipulation.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IImageProvider : IProvider
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a a QR Code.
        /// </summary>
        ///--------------------------------------------------------------------
       // Byte[] GenerateQRCode(
       //     String       content,
       //     Int32       scalingFactor,
       //     ImageFormat format);
        #endregion
    }
}
