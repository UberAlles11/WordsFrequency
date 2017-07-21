using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using WordsFrequency.Common.DAL;
using WordsFrequency.Common.DAL.Entities;
using System.Linq;
using WordsFrequency.Common.Extensions;

namespace WFUnitTests
{
    [TestClass]
    public class DbUnitTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Database.SetInitializer(new DataInitializer());
            using (var db = new DataContext())
            {
                db.Database.Initialize(true);
            }
        }

        [TestMethod]
        public void GetInitialTextFromRepositoryTest()
        {
            using (var uow = new UnitOfWork())
            {
                var source = uow.All<SourceTextBase>().FirstOrDefault();
                Assert.IsNotNull(source);
                Assert.IsFalse(source.Text.IsNullOrEmpty());
            }
        }

        [TestMethod]
        public void ClearAllWordsFromRepositoryTest()
        {
            using (var uow = new UnitOfWork())
            {
                uow.RemoveAll<WordsCountBase>();
                var source = uow.All<WordsCountBase>();
                Assert.IsNotNull(source);
                Assert.IsFalse(source.Any());
            }
        }

        [TestMethod]
        public void AddWordToRepositoryTest()
        {
            using (var uow = new UnitOfWork())
            {
                uow.Add(new WordsCountBase() { Word = "TEST1", Count = 123 } );
                uow.Add(new WordsCountBase() { Word = "TEST2", Count = 124 });
                uow.SaveChanges();
                var source = uow.All<WordsCountBase>().AsQueryable().OrderByDescending(x => x.Id).Take(1).ToList().Single();
                Assert.IsNotNull(source);
                Assert.AreEqual(source.Word, "TEST2");
                Assert.AreEqual(source.Count, 124);
            }
        }
    }
}
