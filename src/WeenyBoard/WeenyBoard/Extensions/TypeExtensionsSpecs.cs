using NUnit.Framework;

namespace WeenyBoard.Extensions
{
    [TestFixture]
    public class TypeExtensionsSpecs
    {
        [Test]
        public void HasDefaultConstructor_is_true_for_class_with_default_constructor()
        {
            Assert.IsTrue(typeof(ClassWithDefaultConstructor).HasDefaultConstructor());
        }

        [Test]
        public void HasDefaultConstructor_is_false_for_class_without_default_constructor()
        {
            Assert.IsFalse(typeof(ClassWithoutDefaultConstructor).HasDefaultConstructor());
        }


        private class ClassWithDefaultConstructor
        {
        }

        private class ClassWithoutDefaultConstructor
        {
            public ClassWithoutDefaultConstructor(int value)
            {
            }
        }
    }
}