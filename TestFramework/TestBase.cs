using NUnit.Framework;

namespace Frankentime.Test
{
    public class TestBase
    {
        protected TFSysTimeFake SysTimeFake;
        protected TFAnalyticsFake AnalyticsFake;

        [SetUp]
        public virtual void SetUp()
        {
            SysTimeFake = new TFSysTimeFake();
            SysTimeFake.InjectIntoSystem();
            AnalyticsFake = new TFAnalyticsFake();
        }

    }
}
