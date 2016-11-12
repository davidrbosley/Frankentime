using Frankentime.Test;

namespace Frankentime.WPF.UnitTests.ViewModelTests
{
    public class TimerViewBase : TestBase
    {
        protected TFFrameNavigationFake FrameNavigationFake;

        public override void SetUp()
        {
            base.SetUp();

            FrameNavigationFake = new TFFrameNavigationFake();
        }
    }
}
