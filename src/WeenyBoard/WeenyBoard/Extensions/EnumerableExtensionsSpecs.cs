using NUnit.Framework;

namespace WeenyBoard.Extensions
{
    [TestFixture] 
    public class EnumerableExtensionsSpecs
    {
        [Test]
        public void Enumerable_without_items_is_empty()
        {
            Assert.IsTrue(new string[0].IsEmpty());
        }

        [Test]
        public void Enumerable_with_items_is_not_empty()
        {
            Assert.IsFalse(new string[1].IsEmpty());
        }
    }
}