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
            var mock = new Mock<ITextProvider>();
            mock.Setup(a => a.Text).Returns("Я знаю три слова");

            _processor = new SimpleTextProcessorRegex(mock.Object);
            var words = _processor.GetWords();

            Assert.IsTrue(words.Any());
            Assert.AreEqual(words.Count(), 4);
        }

        [TestMethod]
        public void SimpleTextProcessorTest()
        {
            var mock = new Mock<ITextProvider>();
            mock.Setup(a => a.Text).Returns("Я знаю три слова");

            _processor = new SimpleTextProcessor(mock.Object);
            var words = _processor.GetWords();

            Assert.IsTrue(words.Any());
            Assert.AreEqual(words.Count(), 4);
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}
