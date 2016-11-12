using NUnit.Framework;

namespace Frankentime.Test
{
    public class TestBase
    {
        private TFSysTimeFake _sysTimeFake;
        protected TFAnalyticsFake AnalyticsFake;

        [SetUp]
        public virtual void SetUp()
        {
            _sysTimeFake = new TFSysTimeFake();
            _sysTimeFake.InjectIntoSystem();
            AnalyticsFake = new TFAnalyticsFake();
        }

    }
}
