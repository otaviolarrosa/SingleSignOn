using NUnit.Framework;

namespace SingleSignOn.Tests.Shared
{
    [TestFixture]
    public abstract class BaseTest
    {
        [OneTimeSetUp]
        protected virtual void OneTimeSetup() { }

        [OneTimeTearDown]
        protected virtual void OneTimeTearDown() { }

        [SetUp]
        protected virtual void SetupTest() { }

        [TearDown]
        protected virtual void TearDownTest() { }

        public BaseTest() { }
    }
}
