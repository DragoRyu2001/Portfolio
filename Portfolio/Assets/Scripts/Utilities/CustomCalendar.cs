using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CustomCalendar
{
    public int date;
    public int month;
    public int year;
    public DateTime GetDateTime()
    {
        return new DateTime(year, month, date);
    }
    public string GetDateTimeText()
    {
        return date + "/" + month + "/" + year;
    }
}
