using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Todo.Test.Unit.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using Todo.Domain.Interfaces;
    using Todo.Domain.Services;
    using Todo.Data.Interfaces;
    using Todo.Domain.Models;
    using Newtonsoft.Json;
    

    [TestClass]
    public class UserServiceTests
    {
        /// <summary>
        /// Setup a mock repository using Moq
        /// </summary>
        /// <returns></returns>
        private IUserRepository GetUserRepository()
        {
            var store = new List<UserModel>();
            var moq = new Moq.Mock<IUserRepository>();
            moq.Setup(repo => repo.Create(It.IsAny<UserModel>()))
                .Returns<UserModel>(um =>
                {
                    store.Add(um);
                    return store.FirstOrDefault(s => s.Id == um.Id);
                });

            moq.Setup(repo => repo.Read(It.IsAny<int>()))
                .Returns<int>((id) => store.FirstOrDefault(s => s.Id == id));

            moq.Setup(repo => repo.Update(It.IsAny<UserModel>()))
                .Returns<UserModel>(um =>
                {
                    store[store.FindIndex(s => s.Id == um.Id)] = um;
                    return store.FirstOrDefault(s => s.Id == um.Id);
                });

            moq.Setup(repo => repo.Delete(It.IsAny<UserModel>())).Returns<UserModel>(um => { return store.Remove(store.FirstOrDefault(s => s.Id == um.Id)); });
            moq.Setup(repo => repo.GetByUsername(It.IsAny<string>())).Returns<string>(username => { return store.FirstOrDefault(s => s.Username == username); });

            return moq.Object;

        }

        private IListServices GetListServices()
        {
            var moq = new Moq.Mock<IListServices>();
            return moq.Object;
        }

        [TestMethod]
        public void UserServiceTests_TestInterface()
        {
            var userServices = new UserServices(GetUserRepository(), GetListServices());
            var Users = new List<UserModel>
                {
                    new UserModel { Id = 1, UserGuid = Guid.NewGuid(), Username = "Bob" },
                    new UserModel { Id = 2, UserGuid = Guid.NewGuid(), Username = "Connie"},
                    new UserModel { Id = 3, UserGuid = Guid.NewGuid(), Username = "Marry" }
                };

            //Load services using Mock repo and test results
            Users.ForEach(u => Assert.AreEqual(JsonConvert.SerializeObject(userServices.Create(u)), JsonConvert.SerializeObject(u)));
            Users.ForEach(u => Assert.AreEqual(JsonConvert.SerializeObject(userServices.Read(u.Id)), JsonConvert.SerializeObject(u)));
            Users.ForEach(u => Assert.AreEqual(JsonConvert.SerializeObject(userServices.GetByUsername(u.Username)), JsonConvert.SerializeObject(u)));
            Users.ForEach(u => Assert.AreEqual(JsonConvert.SerializeObject(userServices.Login(u.Username, string.Empty)), JsonConvert.SerializeObject(u)));
            Users.ForEach(u =>
            {
                var item = new UserModel { Id = u.Id, UserGuid = u.UserGuid, Username = u.Username + "Updated" };
                var updated = userServices.Update(item);
                Assert.AreEqual(JsonConvert.SerializeObject(updated), JsonConvert.SerializeObject(item));
            });
            Users.ForEach(u => Assert.IsTrue(userServices.Delete(u)));
        }
    }
}
