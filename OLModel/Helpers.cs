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
        public static String getTimeStamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
