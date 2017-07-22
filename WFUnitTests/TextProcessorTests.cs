using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordsFrequency.Common.DAL;
using Autofac;
using System.Data.Entity;
using WFUnitTests;
using WordsFrequency.UI;
using WordsFrequency.Common.Text;
using System.Linq;
using Moq;

namespace WFUnitTestss
{
    [TestClass]
    public class UITest
    {
        ITextProcessor _processor;

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void SimpleTextProcessorRegexTest()
        {
            _processor = new SimpleTextProcessorRegex();
            var words = _processor.GetWords("Я знаю три слова");

            Assert.IsTrue(words.Any());
            Assert.AreEqual(words.Count(), 4);
        }

        [TestMethod]
        public void SimpleTextProcessorTest()
        {
            _processor = new SimpleTextProcessor();
            var words = _processor.GetWords("Я знаю три слова");

            Assert.IsTrue(words.Any());
            Assert.AreEqual(words.Count(), 4);
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}
