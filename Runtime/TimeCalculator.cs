using UnityEngine;

namespace Utilities.Runtime
{
    /// <summary>
    /// Provides utility methods for timing and calculating durations.
    /// </summary>
    public static class TimeCalculator
    {
        #region Constants
        public const int DayAsSecond = 86400;
        public const int HourAsSecond = 3600;
        public const int HourAsDay = 24;
        public const int SecondAsDay = 60;
        #endregion
        
        #region Fields
        private static float _time;
        #endregion

        #region Executes
        /// <summary>
        /// Starts a timer using the current real-time.
        /// </summary>
        public static void StartTimer() => _time = Time.realtimeSinceStartup;
        
        /// <summary>
        /// Stops the timer and returns the time difference.
        /// </summary>
        /// <param name="title">An optional title to display along with the time difference.</param>
        /// <returns>The time difference in seconds.</returns>
        public static float StopTimer(string title = "")
        {
            float diff = Time.realtimeSinceStartup - _time;
            diff = diff < 0 ? 0 : diff;
            Debug.Log(title + "TIME::" + diff);
            return diff;
        }
        #endregion
    }
}