using System;

namespace Frankentime.Domain
{
    public interface ISysTime
    {
        DateTime UtcNow { get; }
    }

    public class SysTime : ISysTime
    {
        public static ISysTime Instance { get; set; }

        public static DateTime UtcNow
        {
            get
            {
                if (Instance == null)
                    Instance = new SysTime();

                return Instance.UtcNow;
            }
        }

        DateTime ISysTime.UtcNow => DateTime.UtcNow;
    }
}
