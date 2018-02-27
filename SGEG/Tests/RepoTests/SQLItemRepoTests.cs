using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGEGService.Model;
using SGEGService.Model.Interface;
using SGEGService.Repository.SQLRepo;

namespace Tests.RepoTests
{
    [TestFixture]
    public class SQLItemRepoTests
    {
        [Test]
        public void CanCreateItem()
        {
            var item = new Item();

            Assert.That(item, Is.Not.Null);
        }

        [Test]
        public void CanCreateItemRepository()
        {
            IItemRepo itemRepo = new SQLItemRepo();

            Assert.That(itemRepo, Is.Not.Null);
        }

        [Test]
        public void CanSaveItem()
        {
            IItem item = SQLRepoTestsHelper.CreateValidItem();
            IItemRepo repo = new SQLItemRepo();

            bool res = repo.SaveItem(item);
            Assert.That(res, Is.True);
        }

        [Test]
        public void SaveItemActuallySavesIntoDb()
        {
            IItem item = SQLRepoTestsHelper.CreateValidItem();
            IItemRepo repo = new SQLItemRepo();

            int count = repo.Items.Count;
            repo.SaveItem(item);
            int finalCount = repo.Items.Count;

            Assert.That(count + 1, Is.EqualTo(finalCount));
        }

        [Test]
        public void DataFromDbIsEqual()
        {
            IItem item = SQLRepoTestsHelper.CreateValidItem();
            IItemRepo repo = new SQLItemRepo();

            repo.SaveItem(item);
            var dbItem = repo.GetItemByID(item.ID);

            var res = SQLRepoTestsHelper.Compare(dbItem, item);

            Assert.That(res, Is.True);
        }

        [Test]
        public void CanUpdateItem()
        {
            IItem item = SQLRepoTestsHelper.CreateValidItem();
            IItemRepo repo = new SQLItemRepo();

            repo.SaveItem(item);
            Item dbItem = (Item)repo.GetItemByID(item.ID);
            dbItem.SerialNumber += "_Changed";
            dbItem.ReceptionDate = DateTime.Now;

            var res = repo.SaveItem(dbItem);

            Assert.That(res, Is.True);
        }
    }
}
