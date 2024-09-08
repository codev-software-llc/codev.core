//-----------------------------------------------------------------------------
// <copyright file="TimedScheduler.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Threading;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements a wrapper around a scheduler manager.
    /// </summary>
    ///------------------------------------------------------------------------
    public class TimedScheduler : IDisposable
    {
        #region Private Members
        ///--------------------------------------------------------------------
        /// <summary>
        /// Contains the count of calls in an event.
        /// </summary>
        ///--------------------------------------------------------------------
        private Int32 inCallCount;
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the scheduler object that will be used to periodically
        /// notify clients of an event.
        /// </summary>
        ///--------------------------------------------------------------------
        public TimedScheduler(
            Int32 timerSeconds)
        {
            this.EllapsedTime = timerSeconds;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Called when the object is disposed.
        /// </summary>
        ///--------------------------------------------------------------------
        ~TimedScheduler()
        {
            this.DisposeWorker(false);
        }
        #endregion

        #region Delegates
        ///--------------------------------------------------------------------
        /// <summary>
        /// This event is raised when the scheduled time expires.
        /// </summary>
        ///--------------------------------------------------------------------
        public event EventHandler<EventArgs> ScheduledNotify;
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return whether the object is disposed or not.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean IsDisposed { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the (thread) timer that will be used to run our
        /// work.
        /// </summary>
        ///--------------------------------------------------------------------
        private Timer WorkTimer { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set number of minutes that the timer should trigger on.
        /// </summary>
        ///--------------------------------------------------------------------
        private Int32 EllapsedTime { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Use this count to count how many thread invocations are in 
        /// process.  We will use this count to throttle the callback to the
        /// caller so they do not have multiple callbacks overlapping.
        /// </summary>
        ///--------------------------------------------------------------------
        private Int32 CallCount { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Start our scheduler timer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Start()
        {
            Int32 interval = this.EllapsedTime * 1000;

            if (this.WorkTimer == null)
            {
                this.WorkTimer = new Timer(this.OnScheduledEvent, null, interval, interval);
            }
            else
            {
                this.WorkTimer.Change(interval, interval);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Stop the timer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Stop()
        {
            if (this.WorkTimer != null)
            {
                this.WorkTimer.Change(Int32.MaxValue, Int32.MaxValue);
            }
        }
        #endregion

        #region IDisposable
        ///--------------------------------------------------------------------
        /// <summary>
        /// This is called to release our resources.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Dispose()
        {
            this.DisposeWorker(true);

            // Suppressing the finalize will prevent our Dispose from
            // potentially being called twice.
            //
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Notifications
        ///--------------------------------------------------------------------
        /// <summary>
        /// This method (delegate) is invoked on the ThreadPool when the 
        /// elapsed time has been encountered.
        /// </summary>
        ///--------------------------------------------------------------------
        private void OnScheduledEvent(
            Object sender)
        {
            Int32 count = Interlocked.Increment(ref this.inCallCount);

            if ((count == 1) && (this.ScheduledNotify != null))
            {
                this.ScheduledNotify(this, EventArgs.Empty);
            }

            Interlocked.Decrement(ref this.inCallCount);
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will handle our disposal of our resources.
        /// </summary>
        ///--------------------------------------------------------------------
        private void DisposeWorker(
            Boolean isDisposing)
        {
            if (isDisposing && (this.IsDisposed == false))
            {
                this.ScheduledNotify = null;

                this.WorkTimer.Dispose();

                this.IsDisposed = true;
            }
        }
        #endregion
    }
}
