using System;

namespace Utilities.Runtime
{
    /// <summary>
    /// Provides utility methods for formatting numbers and time durations.
    /// </summary>
    public static class TextFormatter
    {
        #region Constants
        private const string Days = "{0:00}:";
        private const string Hours = "{1:00}:";
        private const string Minutes = "{2:00}:";
        private const string Seconds = "{3:00}:";
        private const string Milliseconds = "{4:00}";
        #endregion

        #region Executes
        /// <summary>
        /// Formats a number into a more readable format (e.g., 1M for 1,000,000).
        /// </summary>
        /// <param name="num">The number to format.</param>
        /// <returns>A string representing the formatted number.</returns>
        public static string FormatNumber(int num) =>
            num switch
            {
                >= 1_000_000_000 => (num / 1_000_000_000D).ToString("0.#B"),
                >= 100_000_000 => (num / 1_000_000D).ToString("0.#M"),
                >= 1_000_000 => (num / 1_000_000D).ToString("0.##M"),
                >= 100_000 => (num / 1_000D).ToString("0.#k"),
                >= 10_000 => (num / 1_000D).ToString("0.##k"),
                >= 1_000 => (num / 1_000D).ToString("0.#k"),
                _ => num.ToString()
            };
        
        /// <summary>
        /// Formats a time duration in seconds into a human-readable string.
        /// </summary>
        /// <param name="totalSecond">The total time in seconds.</param>
        /// <param name="timeFormattingType">The formatting style (days, hours, minutes, seconds).</param>
        /// <param name="withMilliSeconds">Indicates whether to include milliseconds.</param>
        /// <returns>A string representing the formatted time duration.</returns>
        public static string FormatTime(double totalSecond,
            TimeFormattingTypes timeFormattingType = TimeFormattingTypes.DaysHoursMinutesSeconds,
            bool withMilliSeconds = false)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSecond);
            int days = time.Days;
            int hours = time.Hours;
            int minutes = time.Minutes;
            int seconds = time.Seconds;
            int milliSeconds = (int)(totalSecond * 100);
            milliSeconds %= 100;

            bool withDays = timeFormattingType == TimeFormattingTypes.DaysHoursMinutesSeconds;
            bool withHours = timeFormattingType is TimeFormattingTypes.DaysHoursMinutesSeconds
                or TimeFormattingTypes.HoursMinutesSeconds;
            bool withMinutes = timeFormattingType is TimeFormattingTypes.DaysHoursMinutesSeconds
                or TimeFormattingTypes.HoursMinutesSeconds or TimeFormattingTypes.MinutesSeconds;

            string d = withDays ? Days : null;
            string h = withHours ? Hours : null;
            string m = withMinutes ? Minutes : null;
            string ms = withMilliSeconds ? Milliseconds : null;

            string result = string.Format(d + h + m + Seconds + ms, days, hours, minutes, seconds, milliSeconds);
            result = result.TrimEnd(':');
            return result;
        }
        #endregion
    }
}