using System;
using System.Text;
using System.Collections.Generic;
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


    /// <summary>
    /// Summary description for ListServiceTests
    /// </summary>
    [TestClass]
    public class ListServiceTests
    {
        public ListServiceTests()
        {
        }

        /// <summary>
        /// Setup a mock repository using Moq
        /// </summary>
        /// 
        /// TODO refactor this into common test setup code
        /// <returns></returns>
        private IListRepository GetListRepository(List<ListModel> lists)
        {
            var store = new List<ListModel>(lists);
            var moq = new Moq.Mock<IListRepository>();
            moq.Setup(repo => repo.Create(It.IsAny<ListModel>()))
                .Returns<ListModel>(um =>
                {
                    store.Add(um);
                    return store.FirstOrDefault(s => s.Id == um.Id);
                });

            moq.Setup(repo => repo.Read(It.IsAny<int>()))
                .Returns<int>((id) => store.FirstOrDefault(s => s.Id == id));

            moq.Setup(repo => repo.Update(It.IsAny<ListModel>()))
                .Returns<ListModel>(um =>
                {
                    store[store.FindIndex(s => s.Id == um.Id)] = um;
                    return store.FirstOrDefault(s => s.Id == um.Id);
                });

            moq.Setup(repo => repo.Delete(It.IsAny<ListModel>())).Returns<ListModel>(um => { return store.Remove(store.FirstOrDefault(s => s.Id == um.Id)); });
            return moq.Object;
        }

        private IListItemServices GetListItemServices()
        {
            var moq = new Moq.Mock<IListItemServices>();
            moq.Setup(lis => lis.GetByList(It.IsAny<ListModel>())).Returns(new List<ListItemModel>());
            return moq.Object;
        }

        private List<ListModel> GetTestData()
        {
            var lists = new List<ListModel>
                {
                    new ListModel { Id = 1, UserId = 1 },
                    new ListModel { Id = 2, UserId = 2 },
                    new ListModel { Id = 3, UserId = 3 }
                };

            return lists;
        }

        [TestMethod]
        public void ListServicesTest_TestCreate()
        {
            var Lists = GetTestData();
            var listServices = new ListServices(GetListRepository(Lists), GetListItemServices());
            Lists.ForEach(li => Assert.AreEqual(JsonConvert.SerializeObject(listServices.Create(li)), JsonConvert.SerializeObject(li)));
        }

        [TestMethod]
        public void ListServicesTest_TestRead()
        {
            var Lists = GetTestData();
            var listServices = new ListServices(GetListRepository(Lists), GetListItemServices());
            Lists.ForEach(li => Assert.AreEqual(JsonConvert.SerializeObject(listServices.Read(li.Id)), JsonConvert.SerializeObject(li)));
        }

        [TestMethod]
        public void ListServicesTest_TestUpdate()
        {
            var Lists = GetTestData();
            var listServices = new ListServices(GetListRepository(Lists), GetListItemServices());
            Lists.ForEach(li =>
            {
                var item = new ListModel { Id = li.Id, UserId = li.UserId + 1 };
                var updated = listServices.Update(item);
                Assert.AreEqual(JsonConvert.SerializeObject(updated), JsonConvert.SerializeObject(item));
            });
        }

        [TestMethod]
        public void ListServicesTest_TestDelete()
        {
            var Lists = GetTestData();
            var listServices = new ListServices(GetListRepository(Lists), GetListItemServices());
            Lists.ForEach(li => Assert.IsTrue(listServices.Delete(li)));
        }

    }

}
