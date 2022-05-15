using System.Globalization;
using System.Text.RegularExpressions;

namespace SimpleNewsReader.Domain.Utilties;

public static class PersianDateTime
{
    public static string PersianDate()
    {
        var sDate = DateTime.Now;
        var shamsi = new PersianCalendar();
        var mYear = shamsi.GetYear(sDate).ToString(CultureInfo.InvariantCulture);
        var mMonth = shamsi.GetMonth(sDate).ToString(CultureInfo.InvariantCulture);
        var mDay = shamsi.GetDayOfMonth(sDate).ToString(CultureInfo.InvariantCulture);
        if (mMonth.Trim().Length == 1)
            mMonth = "0" + mMonth.Trim();
        if (mDay.Trim().Length == 1)
            mDay = "0" + mDay.Trim();
        return mYear + "/" + mMonth + "/" + mDay;
    }

    public static DateTime ConvertPersianDateToGregorianDate(string persianDate)
    {
        if (!Validate(persianDate))
            return DateTime.Now;
        var shamsi = new PersianCalendar();
        var year = int.Parse(persianDate.Trim().Substring(0, 4));
        var month = int.Parse(persianDate.Trim().Substring(5, 2));
        var day = int.Parse(persianDate.Trim().Substring(8, 2));
        if (!IsLeapYear(year) && day >= 30 && month == 12)
        {
            day = 29;
        }
        return shamsi.ToDateTime(year, month, day, 0, 0, 0, 0);

    }

    public static string ConvertGregorianDateToPersianDate(string gregorianDate)
    {
        try
        {
            var sDate = DateTime.Parse(gregorianDate);
            var shamsi = new PersianCalendar();
            var mYear = shamsi.GetYear(sDate).ToString(CultureInfo.InvariantCulture);
            var mMonth = shamsi.GetMonth(sDate).ToString(CultureInfo.InvariantCulture);
            var mDay = shamsi.GetDayOfMonth(sDate).ToString(CultureInfo.InvariantCulture);
            if (mMonth.Trim().Length == 1)
                mMonth = "0" + mMonth.Trim();
            if (mDay.Trim().Length == 1)
                mDay = "0" + mDay.Trim();
            return mYear + "/" + mMonth + "/" + mDay;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string ConvertGregorianDateToPersianDate(DateTime gregorianDate)
    {
        return ConvertGregorianDateToPersianDate(gregorianDate.ToString("G"));
    }

    public static bool IsLeapYear(int year)
    {
        var remain = year % 33;
        return remain == 1 || remain == 5 || remain == 9 || remain == 13 || remain == 18 || remain == 22 ||
               remain == 26 || remain == 30;
    }

    public static bool Validate(string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            return false;
        }
        Regex rx = new Regex("^1[34][0-9][0-9]\\/((0?[1-6]\\/((3[0-1])|([1-2][0-9])|(0?[1-9])))|((1[0-2]|(0?[7-9]))\\/(30|([1-2][0-9])|(0?[1-9]))))$");
        if (rx.IsMatch(date))
        {
            CorrectDate(date);
            return true;
        }
        else
        {
            return false;
        }
    }

    private static void CorrectDate(string date)
    {
        if (date.Length < 10)
        {
            string[] parts = date.Split('/');
            date =$"{parts[0]}/{(parts[1].Length == 1 ? "0" : "")}{parts[1]}/{(parts[2].Length == 1 ? "0" : "")}{parts[2]}";
        }
    }
}