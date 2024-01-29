using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class SystemLog
    {
        public static void Log(string log)
        {
            System.IO.File.WriteAllText(@"C:\\Log\\CustomerLog.txt", log);
        }
    }
}
