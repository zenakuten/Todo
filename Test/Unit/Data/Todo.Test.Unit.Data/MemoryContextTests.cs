using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Todo.Test.Unit.Data
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Todo.Data.Context.MemoryContext;
    using Todo.Data.Interfaces;
    using Todo.Domain.Models;

    class TestContextConfiguration : IContextConfiguration
    {
        public TestContextConfiguration(string dataFile)
        {
            Settings["DataFile"] = dataFile;
        }

        private Dictionary<string, string> _settings = new Dictionary<string, string>();
        public Dictionary<string, string> Settings => _settings;
    }


    /// <summary>
    /// Simple unit tests for the memorycontext
    /// 
    /// cheat a little and use serializeobject to do a quick memberwise compare
    /// </summary>
    [TestClass]
    public class MemoryContextTests
    {
        public string TestDataFolder
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData\\");
            }
        }

        /// <summary>
        /// Verify Load() works against known data
        /// </summary>
        [TestMethod]
        public void MemoryContext_TestLoadGoodData()
        {
            var target = new ListItemModel() { Id = 1, Deadline = DateTime.Parse("2018-09-23T16:56:12.3412954-07:00"),  Value = "Test" };

            var context = new MemoryContext(new TestContextConfiguration(TestDataFolder + "MemoryContextData.json"));
            context.Load();
            Assert.AreEqual(context.StoreFor<ListItemModel>().Count, 1);
            Assert.AreEqual(context.StoreFor<ListModel>().Count, 0);
            Assert.AreEqual(context.StoreFor<UserModel>().Count, 0);
            Assert.AreEqual(JsonConvert.SerializeObject(target), JsonConvert.SerializeObject(context.StoreFor<ListItemModel>()[0]));
        }

        /// <summary>
        /// Verify Load() fails when given bad configuration
        /// </summary>
        [TestMethod]
        public void MemoryContext_TestLoadNoFile()
        {
            Assert.IsFalse(File.Exists(TestDataFolder + "NoData.json"));
            var configuration = new TestContextConfiguration(TestDataFolder + "NoData.json");
            var context = new MemoryContext(configuration);
            try
            {
                context.Load();
                Assert.Fail("Load should not have succeeded.");
            }
            catch (FileNotFoundException)
            {
            }
        }

        /// <summary>
        /// Basic smoke tests
        /// </summary>
        [TestMethod]
        public void MemoryContext_TestUserRepository()
        {
            var context = new MemoryContext(new TestContextConfiguration(TestDataFolder + "UserRepositoryTest.json"));
            var Users = new List<UserModel>
                {
                    new UserModel { Id = 1, UserGuid = Guid.NewGuid(), Username = "Bob"},
                    new UserModel { Id = 2, UserGuid = Guid.NewGuid(), Username = "Connie"},
                    new UserModel { Id = 3, UserGuid = Guid.NewGuid(), Username = "Marry" }
                };

            Users.ForEach(u => Assert.AreEqual(JsonConvert.SerializeObject(context.Users.Create(u)), JsonConvert.SerializeObject(u)));
            Users.ForEach(u => Assert.AreEqual(JsonConvert.SerializeObject(context.Users.Read(u.Id)), JsonConvert.SerializeObject(u)));
            Users.ForEach(u => Assert.AreEqual(JsonConvert.SerializeObject(context.Users.GetByUsername(u.Username)), JsonConvert.SerializeObject(u)));
            Users.ForEach(u =>
            {
                var item = new UserModel { Id = u.Id, UserGuid = u.UserGuid, Username = u.Username + "Updated" };
                var updated = context.Users.Update(item);
                Assert.AreEqual(JsonConvert.SerializeObject(updated), JsonConvert.SerializeObject(item));
            });
            Users.ForEach(u => Assert.IsTrue(context.Users.Delete(u)));
            Assert.AreEqual(context.StoreFor<UserModel>().Count, 0);
        }
    }
}
