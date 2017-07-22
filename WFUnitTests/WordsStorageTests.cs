using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordsFrequency.Common.DAL;
using Autofac;
using System.Data.Entity;
using System.Collections.Generic;
using WordsFrequency.Common.DAL.Entities;
using System.Linq;
using WordsFrequency.Common.Extensions;

namespace WFUnitTests
{
    [TestClass]
    public class WordsStorageTest
    {
        IContainer _container;

        Dictionary<string, int> _wordsCount = new Dictionary<string, int>()
        {
            { "первыйнах", 1},
            { "второйнах", 2},
            { "третийнах", 3},
            { "четвертыйнах", 4},
            { "пятыйнах", 5}
        };

        [TestInitialize]
        public void TestInitialize()
        {
            Database.SetInitializer(new DataInitializer());
            using (var db = new DataContext())
            {
                db.Database.Initialize(true);
            }

            var builder = new ContainerBuilder();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<WordsFrequencyDbStorage>().As<IWordsFrequencyStorage>();

            _container = builder.Build();
        }

        [TestMethod]
        public void DbStorageTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var storage = scope.Resolve<IWordsFrequencyStorage>();
                storage.Commit(_wordsCount);
                
                var uow = scope.Resolve<IUnitOfWork>();
                var wordsCountStorage = uow.All<WordsCountBase>().ToDictionary(wcb => wcb.Word, wcb => wcb.Count);
                Assert.AreEqual(_wordsCount.Count, wordsCountStorage.Count);
                _wordsCount.ForEach(wc => Assert.IsTrue(wordsCountStorage.Contains(wc)));
            }
        }
    }
}
