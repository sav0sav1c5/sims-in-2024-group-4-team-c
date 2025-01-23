using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Utilities
{
    public static class DateRangeUtility
    {
        public static bool IsDateRangeOverLapping(CalendarDateRange dateRange1, CalendarDateRange dateRange2)
        {
            DateOnly dateRange1Start = new DateOnly(dateRange1.Start.Year, dateRange1.Start.Month, dateRange1.Start.Day);
            DateOnly dateRange1End = new DateOnly(dateRange1.End.Year, dateRange1.End.Month, dateRange1.End.Day);
            DateOnly dateRange2Start = new DateOnly(dateRange2.Start.Year, dateRange2.Start.Month, dateRange2.Start.Day);
            DateOnly dateRange2End = new DateOnly(dateRange2.End.Year, dateRange2.End.Month, dateRange2.End.Day);
            if (dateRange1Start > dateRange2End || dateRange1End < dateRange2Start)
                return false;
            return true;
        }

        public static void sortCalendarDateRanges(List<CalendarDateRange> ranges, bool ascendingOrder = true)
        {
            if (ascendingOrder)
            {
                ranges.Sort( 
                    delegate (CalendarDateRange c1, CalendarDateRange c2) { return c1.Start < c2.Start ? -1 : 1; }
                );
                return;
            }
            ranges.Sort(
                delegate (CalendarDateRange c1, CalendarDateRange c2) { return c1.Start < c2.Start ? 1 : -1; }
            );
        }

        private static DateOnly Min(DateOnly date1, DateTime date2)
        {
            return date1 < DateOnly.FromDateTime(date2) ? date1 : DateOnly.FromDateTime(date2);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"> Is included in result</param>
        /// <param name="end"> Is included in result</param>
        /// <param name="stayingPeriod">end-stayingPeriod is the last available date</param>
        /// <returns>List of date ranges used for booking an accommodation</returns>
        public static List<CalendarDateRange> GetAvailableDateRanges(DateOnly start, DateOnly end, int stayingPeriod = 1)
        {
            if (end < start.AddDays(stayingPeriod - 1))
                return new List<CalendarDateRange>();

            int numberOfAvailableDateRangesInBetween = end.DayNumber - start.DayNumber - stayingPeriod + 2;
            List<CalendarDateRange> result = new List<CalendarDateRange>(numberOfAvailableDateRangesInBetween);//this for some reason does not allocate the memory... tf is it doing then?

            for (int i = 0; i < numberOfAvailableDateRangesInBetween; i++)
            {
                DateTime currentRangeStartingPeriod = new DateTime(start.Year, start.Month, start.Day);
                DateTime currentRangeEndingPeriod = new DateTime(start.AddDays(stayingPeriod - 1).Year, start.AddDays(stayingPeriod - 1).Month, start.AddDays(stayingPeriod - 1).Day);
                result.Add(new CalendarDateRange(currentRangeStartingPeriod, currentRangeEndingPeriod));
                start = start.AddDays(1);
            }
            return result;
        }

        private static List<CalendarDateRange> GetGapsBetweenReservationDateRanges(List<CalendarDateRange> unavailableDateRanges, DateOnly freeReservationSearchRangeStart, DateOnly freeReservationSearchRangeEnd, int stayingPeriod)
        {
            List<CalendarDateRange> freeReservationDateRanges = new List<CalendarDateRange>();
            //start of search -> first unavailable date
            freeReservationDateRanges.AddRange(DateRangeUtility.GetAvailableDateRanges(freeReservationSearchRangeStart, Min(freeReservationSearchRangeEnd, unavailableDateRanges[0].Start.AddDays(-1)).AddDays(-stayingPeriod + 1), stayingPeriod));

            //Add gaps between unavailable dates
            for (int i = 0; i < unavailableDateRanges.Count - 1; ++i)
            {
                if (DateOnly.FromDateTime(unavailableDateRanges[i].End.AddDays(1)) < freeReservationSearchRangeStart)
                    continue;
                
                if (freeReservationSearchRangeEnd <= DateOnly.FromDateTime(unavailableDateRanges[i].Start))
                    break;
                
                freeReservationDateRanges.AddRange(
                    DateRangeUtility.GetAvailableDateRanges(DateOnly.FromDateTime(unavailableDateRanges[i].End.AddDays(1)), Min(freeReservationSearchRangeEnd, unavailableDateRanges[i + 1].Start.AddDays(-1)).AddDays(-stayingPeriod + 1), stayingPeriod));
            }
            //end of last unavailable -> end of search range
            if (freeReservationSearchRangeEnd > DateOnly.FromDateTime(unavailableDateRanges[^1].End))
            {
                freeReservationDateRanges.AddRange(DateRangeUtility.GetAvailableDateRanges(DateOnly.FromDateTime(unavailableDateRanges[^1].End.AddDays(1)), freeReservationSearchRangeEnd.AddDays(-stayingPeriod + 1), stayingPeriod));
            }
            return freeReservationDateRanges;
        }

        private static List<CalendarDateRange> GetFirstFreeReservationDateRangePastSearchRange(List<CalendarDateRange> unavailableDateRanges, DateOnly freeReservationSearchRangeStart, DateOnly freeReservationSearchRangeEnd, int stayingPeriod)
        {
            List<CalendarDateRange> freeReservationDateRanges = new List<CalendarDateRange>();
            for (int i = 0; i < unavailableDateRanges.Count; ++i)
            {
                if (DateOnly.FromDateTime(unavailableDateRanges[i].End.AddDays(1)) < freeReservationSearchRangeEnd)
                    continue;
                
                //freeReservationSearchRangeEnd is now pointing to a free date
                if (freeReservationSearchRangeEnd.AddDays(stayingPeriod) > DateOnly.FromDateTime(unavailableDateRanges[i].Start.AddDays(-1)))
                {
                    //gap is not big enough
                    freeReservationSearchRangeEnd = DateOnly.FromDateTime(unavailableDateRanges[i].End.AddDays(1));
                    //freeReservationSearchRangeEnd is now pointing to the next free date
                }
            }
            //freeReservationSearchRangeEnd is pointing to a free date
            freeReservationDateRanges.AddRange(DateRangeUtility.GetAvailableDateRanges(freeReservationSearchRangeEnd, freeReservationSearchRangeEnd.AddDays(-stayingPeriod + 1), stayingPeriod));
            return freeReservationDateRanges;
        }

        public static List<CalendarDateRange> FindFreeReservationDateRanges(List<CalendarDateRange> unavailableDateRanges, DateTime accommodationReservationSearchFromDate, DateTime accommodationReservationSearchToDate, int stayingPeriod)
        {
            DateRangeUtility.sortCalendarDateRanges(unavailableDateRanges, true);   //true = Sorts in ascending order
            List<CalendarDateRange> freeReservationDateRanges = new List<CalendarDateRange>();  //return value of function
            DateOnly freeReservationSearchRangeStart = DateOnly.FromDateTime(accommodationReservationSearchFromDate);
            DateOnly freeReservationSearchRangeEnd = DateOnly.FromDateTime(accommodationReservationSearchToDate);
            //The search range does not containt any unavailable dates
            if (unavailableDateRanges.Count <= 0
                || DateOnly.FromDateTime(unavailableDateRanges[0].Start) > freeReservationSearchRangeEnd
                || DateOnly.FromDateTime(unavailableDateRanges[^1].End) < freeReservationSearchRangeStart )
            {                
                freeReservationDateRanges.AddRange(DateRangeUtility.GetAvailableDateRanges( freeReservationSearchRangeStart, freeReservationSearchRangeEnd.AddDays( -stayingPeriod ), stayingPeriod));
                return freeReservationDateRanges;        
            }
            freeReservationDateRanges.AddRange(GetGapsBetweenReservationDateRanges(unavailableDateRanges, freeReservationSearchRangeStart, freeReservationSearchRangeEnd, stayingPeriod));
            //Add one outside the range
            if (freeReservationDateRanges.Count <= 0)
                freeReservationDateRanges.AddRange(GetFirstFreeReservationDateRangePastSearchRange(unavailableDateRanges, freeReservationSearchRangeStart, freeReservationSearchRangeEnd, stayingPeriod));
            
            return freeReservationDateRanges;
        }

    }
}
