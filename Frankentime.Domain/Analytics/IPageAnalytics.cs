using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankentime.Domain.Analytics
{
    public interface IPageAnalytics
    {
        void FeatureUsed(string featureName);
        void End();
    }
}
