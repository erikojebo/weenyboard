using NUnit.Framework;

namespace WeenyBoard.Extensions
{
    [TestFixture]
    public class StringExtensionsSpecs
    {
        [Test]
        public void Camel_casing_an_empty_string_yields_an_empty_string()
        {
            Assert.AreEqual("", "".ToCamelCase());
        }

        [Test]
        public void Camel_casing_a_single_lowercase_word_yields_the_same_string()
        {
            Assert.AreEqual("string", "string".ToCamelCase());
        }

        [Test]
        public void Camel_casing_a_string_with_only_the_first_letter_uppercase_returns_an_all_lowercase_string()
        {
            Assert.AreEqual("string", "String".ToCamelCase());
        }

        [Test]
        public void Camel_casing_a_pascal_case_string_yields_same_string_but_with_first_letter_lowercase()
        {
            Assert.AreEqual("pascalCase", "PascalCase".ToCamelCase());
        }

        [Test]
        public void Camel_casing_single_uppercase_letter_yields_same_letter_as_lowercase()
        {
            Assert.AreEqual("a", "A".ToCamelCase());
        }
    }
}