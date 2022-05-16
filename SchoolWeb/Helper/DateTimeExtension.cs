namespace SchoolWeb.Helper
{
    public static class DateTimeExtension
    {

        /// <summary>
        /// Calculates the elapsed years from the date to the end date parameter
        /// </summary>
        /// <param name="fromDate">Starting date for calculation</param>
        /// <param name="toDate">End date for calculation, if it is null it will be assumed Today</param>
        /// <returns>Elapsed years</returns>
        public static int? ElapsedYearsUntil(this DateTime? fromDate, DateTime? toDate = null)
        {
            if (fromDate == null) return null;

            if (toDate == null) toDate = DateTime.Today;
            
            int elapsedYears = toDate.Value.Year - fromDate.Value.Year;

            if (toDate.Value.Month < fromDate.Value.Month 
                || (toDate.Value.Month == fromDate.Value.Month && toDate.Value.Day < fromDate.Value.Day)) 
                elapsedYears--;

            return elapsedYears;
        }

        /// <summary>
        /// Rounding UP a DateTime to the nearest "d" minutes
        /// </summary>
        /// <param name="dt">DateTime value</param>
        /// <param name="d">Rounded minutes. It must less or equal than 60.</param>
        /// <returns>Rounded DateTime to "d" nearest minutes, up</returns>
        public static DateTime RoundUp(this DateTime dt, TimeSpan d)
        {
            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }

        /// <summary>
        /// Rounding DOWN a DateTime to the nearest "d" minutes
        /// </summary>
        /// <param name="dt">DateTime value</param>
        /// <param name="d">Rounded minutes. It must less or equal than 60.</param>
        /// <returns>Rounded DateTime to "d" nearest minutes, down</returns>
        public static DateTime RoundDown(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }

        /// <summary>
        /// Rounding UP o DOWN a DateTime to the nearest "d" minutes
        /// </summary>
        /// <param name="dt">DateTime value</param>
        /// <param name="d">Rounded minutes. It must less or equal than 60.</param>
        /// <returns>Rounded DateTime to nearest "d" minutes, up or down</returns>
        public static DateTime RoundToNearest(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            bool roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;

            return new DateTime(dt.Ticks + offset - delta, dt.Kind);
        }
    }
}
