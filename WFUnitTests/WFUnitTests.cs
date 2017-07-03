using System;
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
            using (var data = new DbDataRepository<SourceTextBase>())
            {
                var source = data.GetAll().FirstOrDefault();
                Assert.IsNotNull(source);
                Assert.IsFalse(source.Text.IsNullOrEmpty());
            }
        }

        [TestMethod]
        public void ClearAllWordsFromRepositoryTest()
        {
            using (var data = new DbDataRepository<WordsCountBase>())
            {
                data.DeleteAll();
                var source = data.GetAll();
                Assert.IsNotNull(source);
                Assert.IsFalse(source.Any());
            }
        }

        [TestMethod]
        public void AddWordToRepositoryTest()
        {
            using (var data = new DbDataRepository<WordsCountBase>())
            {
                data.Add(new WordsCountBase() { Word = "TEST1", Count = 123 } );
                data.Add(new WordsCountBase() { Word = "TEST2", Count = 124 });
                data.Save();
                var source = data.GetAllQueryable().OrderByDescending(x => x.Id).Take(1).ToList().Single();
                Assert.IsNotNull(source);
                Assert.AreEqual(source.Word, "TEST2");
                Assert.AreEqual(source.Count, 124);
            }
        }
    }
}
