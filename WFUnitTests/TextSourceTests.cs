using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordsFrequency.Common.DAL;
using Autofac;
using System.Data.Entity;
using WFUnitTests;
using WordsFrequency.UI;
using WordsFrequency.Common.Text;
using Moq;

namespace WFUnitTestss
{
    [TestClass]
    public class TextSourceTest
    {
        IContainer _container;
        TestFile _testFile;

        [TestInitialize]
        public void TestInitialize()
        {
            Database.SetInitializer(new DataInitializer());
            using (var db = new DataContext())
            {
                db.Database.Initialize(true);
            }

            var testFilePath = "test.txt";
            var testText = "Я знаю три слова";

            _testFile = new TestFile(testFilePath, testText);

            var fileProviderMock = new Mock<IFilePathProvider>();
            fileProviderMock.Setup(a => a.GetPath()).Returns(testFilePath);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(fileProviderMock.Object).As<IFilePathProvider>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<FileTextSource>().Named<ITextSource>("file");
            builder.RegisterType<DbTextSource>().Named<ITextSource>("db");

            _container = builder.Build();
        }

        [TestMethod]
        public void FileSourceTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var source = scope.ResolveNamed<ITextSource>("file");
                var text = source.ReadTextToBuffer();
                var text2 = source.GetBufferedText();

                Assert.IsFalse(string.IsNullOrEmpty(text));
                Assert.IsFalse(string.IsNullOrEmpty(text2));
                Assert.AreEqual(text, text2);
                Assert.AreEqual(text2, _testFile.Text);
            }
        }

        [TestMethod]
        public void DbSourceTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var source = scope.ResolveNamed<ITextSource>("db");
                var text = source.ReadTextToBuffer();
                var text2 = source.GetBufferedText();

                Assert.IsFalse(string.IsNullOrEmpty(text));                
                Assert.IsFalse(string.IsNullOrEmpty(text2));
                Assert.AreEqual(text, text2);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_container != null)
                _container.Dispose();

            if (_testFile != null)
                _testFile.Delete();
        }
    }
}
