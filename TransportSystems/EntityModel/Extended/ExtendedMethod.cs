using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TransportSystems.EntityModel;

namespace TransportSystems.EntityModel.Extended
{

    public static class ExtendedMethod
    {

        private static TransportModel db = new TransportModel();
        public static AspUser LoginedUser=new AspUser();
        public static AspUser GetUserData(string UserName)
        {
            try {
                AspUser Logined = db.AspUser.ToList().Where(o => o.Username == UserName).First();
                return Logined;
            } catch (Exception ex) {
                return null;
            }

        }
        public static string GetDateToday()
        {
            var date = DateTime.Now;
            if (date == null) { return null; }
            var year = ((date.Year).ToString().Length == 1) ? '0' + date.Year.ToString() : date.Year.ToString();
            var month = ((date.Month).ToString().Length == 1) ? '0' + (date.Month.ToString()) : (date.Month.ToString());
            var day = ((date.Day).ToString().Length == 1) ? '0' + date.Day.ToString() : date.Day.ToString();
            return year.ToString() + '-' + month.ToString() + '-' + day.ToString();
        }
        public static string ParseDateToString(DateTime date)
        {
            return date.Year + "-" + date.Month + "-" + date.Day;
        }
        public static DateTime FormatDate(string date)
        {
            return DateTime.Parse(date, CultureInfo.CreateSpecificCulture("ar-EG"));
        }

    }

    public class ResponseStringAndNumber
    {
        public double MyNumber;
        public string Mystring;
        public bool State;
    }
}