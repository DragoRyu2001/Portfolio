using System;
using UnityEngine.Serialization;

namespace Utilities
{
    [System.Serializable]
    public struct CustomCalendar
    {
        [FormerlySerializedAs("date")] public int Date;
        [FormerlySerializedAs("month")] public int Month;
        [FormerlySerializedAs("year")] public int Year;
        public DateTime GetDateTime()
        {
            return new DateTime(Year, Month, Date);
        }
        public string GetDateTimeText()
        {
            return Date + "/" + Month + "/" + Year;
        }
    }
}
