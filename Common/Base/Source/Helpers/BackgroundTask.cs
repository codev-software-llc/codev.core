//-----------------------------------------------------------------------------
// <copyright file="BackgroundTask.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.ComponentModel;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides a common means to invoke tasks in a background thread.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class BackgroundTask
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// This provides the worker code that wraps the custom Ado client
        /// implementations.  Its goal is to provide the common aspects of
        /// open and closing of connections as well as doing the common
        /// exception handling.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void Invoke(
            Action<Object>                  work,
            Action<BackgroundTaskEventArgs> complete)
        {
            using (BackgroundWorker bw = new BackgroundWorker()) 
            {
                // Setup the work to be called by the background task.
                //
                bw.DoWork += (sender, e) =>
                    {
                        work(e.Argument);                        
                    };

                // Once completed, we can signal the caller.
                //
                bw.RunWorkerCompleted += (sender, e)  =>
                    {
                        if (complete != null)
                        {                            
                            BackgroundTaskEventArgs args = new BackgroundTaskEventArgs(e.Error);

                            complete(args);
                        }
                    };

                bw.RunWorkerAsync();
            }
        }
        #endregion
    }
}
