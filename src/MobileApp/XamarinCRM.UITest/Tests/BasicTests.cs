using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinCRM.UITest
{
    public class BasicTests : AbstractSetup
    {
        public BasicTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        [Ignore]
        public void Repl()
        {
            app.Repl();
        }
    }
}

