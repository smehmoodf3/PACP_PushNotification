using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Application
/// </summary>
public class ClsApplication
{
    public ClsApplication()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string DatetoString(DateTime? UserDate)
    {
        if (UserDate == null)
        {
            return "";
        }
        else
        {
            string Day = UserDate.Value.Day.ToString();
            string Month = UserDate.Value.Month.ToString();
            string Year = UserDate.Value.Year.ToString();
            if (Day.Length == 1)
            {
                Day = "0" + Day;
            }
            return Day + "/" + Month + "/" + Year;
        }
    }
}