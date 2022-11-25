using benjaminshi.gs1;

namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDownload()
        {
            Assert.IsTrue(GCPLength.Download());
        }

        [Test]
        public void TestRefersh()
        {
            Assert.IsTrue(GCPLength.Refresh());
        }

        [Test]
        public void TestExists()
        {
            Assert.IsTrue(GCPLength.Exists("690123"));
            Assert.IsFalse(GCPLength.Exists("a690123"));
        }

        [Test]
        public void TestFind()
        {
            Assert.IsTrue(7 == GCPLength.Find("690123"));
            Assert.IsTrue(0 == GCPLength.Find("a690123"));
            Assert.IsTrue(0 == GCPLength.Find(""));
            Assert.IsTrue(7 == GCPLength.Find("690123a"));
            Assert.IsTrue(0 == GCPLength.Find("6901a23"));
        }
    }
}