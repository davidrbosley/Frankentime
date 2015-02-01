using System;
using System.Collections.Generic;
using Frankentime.Domain;

namespace Frankentime.Test
{
    public class TFSysTimeFake : ISysTime
    {

        public static TFSysTimeFake Instance { get; private set; }

        public static TFSysTimeFake InjectInSystem()
        {
            Instance = new TFSysTimeFake();
            Instance.InjectIntoSystem();
            return Instance;
        }

        private readonly List<DateTime> _dateTimeList = new List<DateTime>(); 

        private void InjectIntoSystem()
        {
            SysTime.Instance = this;
        }


        public DateTime UtcNow
        {
            get
            {
                if (_dateTimeList.Count > 0)
                {
                    var returnTime = _dateTimeList[0];
                    if (_dateTimeList.Count > 1)
                        _dateTimeList.RemoveAt(0);

                    return returnTime;
                }

                return DateTime.UtcNow;
            }
        }

        public void ReturnTimeOf(params DateTime[] dateTimes)
        {
            _dateTimeList.Clear();
            _dateTimeList.AddRange(dateTimes);
        }
    }
}
