
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankentime.Domain
{
    /// <summary>
    /// A simple time collector
    /// </summary>
    public class FrankenTimer
    {
        private DateTime _currentStart;
        private readonly List<TimeSpan> _trackedTimes = new List<TimeSpan>();
        private bool _isRunning;

        public void Start()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _currentStart = SysTime.UtcNow;
            }
        }

        public void Stop()
        {
            if (_isRunning)
            {
                _isRunning = false;
                _trackedTimes.Add(SysTime.UtcNow.Subtract(_currentStart));
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public TimeSpan TotalTime
        {
            get
            {
                var total = _trackedTimes.Count == 0 ? TimeSpan.Zero : _trackedTimes.Aggregate((current, trackedTime) => current.Add(trackedTime));

                if (_isRunning)
                    total = total.Add(SysTime.UtcNow.Subtract(_currentStart));

                return total;
            }
        }

        public void Reset()
        {
            _trackedTimes.Clear();
            _currentStart = SysTime.UtcNow;
        }

        public override string ToString()
        {
            return TotalTime.ToString();
        }

    }
    
}
