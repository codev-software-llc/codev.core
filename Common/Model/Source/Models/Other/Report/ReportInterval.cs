//-----------------------------------------------------------------------------
// <copyright file="ReportInterval.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    ///------------------------------------------------------------------------
    /// <summary>
    /// This represents the report granularity rounding
    /// </summary>
    ///------------------------------------------------------------------------
    public enum ReportInterval : int
    {
        None           = 0,
        FifteenMinutes = 15,
        ThirtyMinutes  = 30,
        Hour           = 60
    }
}
