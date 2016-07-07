using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLModel
{
    public class Helpers
    {
        public static String getTimeStamp() { return getTimeStamp(DateTime.Now); }
        private static String getTimeStamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static void AdminLog(string format, params object[] vals)
        {
            string time = OLModel.Helpers.getTimeStamp();
            string log = String.Format(format, vals);
            Model.Instance.AdminLog.Add(time + " - " + log);
        }

        public static void PublicLog(string format, params object[] vals)
        {
            string time = OLModel.Helpers.getTimeStamp();
            string log = String.Format(format, vals);
            Model.Instance.UserLog.Add(time + " - " + log);
        }
    }
}
