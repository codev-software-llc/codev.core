//-----------------------------------------------------------------------------
// <copyright file="ReportSize.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains the size of reports.
    /// </summary>
    ///------------------------------------------------------------------------
    [Flags]
    public enum ReportSize : int
    {
        Large  = 0,
        Medium = 1,
        Small  = 2
    }
}
