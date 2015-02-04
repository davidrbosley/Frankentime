using System;
using System.Collections.Generic;
using System.Reflection;
using Frankentime.Domain.Analytics;
using NUnit.Framework;

namespace Frankentime.Test
{
    public class TFAnalyticsFake : IAnalytics
    {
        readonly Dictionary<string, int> _callCounts = new Dictionary<string, int>();
        private void AddCallCount(string methodName)
        {
            if (_callCounts.ContainsKey(methodName))
                _callCounts[methodName]++;
            else
                _callCounts.Add(methodName, 1);

        }

        public void VerifyCallCount(string methodName, int expectedCallCount)
        {
            Assert.AreEqual(expectedCallCount, _callCounts.ContainsKey(methodName) ? _callCounts[methodName] : 0, 
                Environment.NewLine + methodName + "call count Incorrect");
        }

        public void ApplicationStart()
        {
            AddCallCount(MethodBase.GetCurrentMethod().Name);
        }

        public void ApplicationEnd()
        {
            AddCallCount(MethodBase.GetCurrentMethod().Name);
        }
    }
}
