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
    class SQLCategoryRepoTests
    {
        [Test]
        public void CanCreateCategory()
        {
            ICategory category = new Category();

            Assert.That(category, Is.Not.Null);
        }

        [Test]
        public void CanCreateCategoryRepository()
        {
            ICategoryRepo categoryRepo = new SQLCategoryRepo();

            Assert.That(categoryRepo, Is.Not.Null);
        }

        [Test]
        public void CanSaveCategory()
        {
            ICategory category = SQLRepoTestsHelper.CreateValidCategory();
            ICategoryRepo repo = new SQLCategoryRepo();

            bool res = repo.SaveCategory(category);
            Assert.That(res, Is.True);
        }

        [Test]
        public void SaveCategoryActuallySavesIntoDb()
        {
            ICategory category = SQLRepoTestsHelper.CreateValidCategory();
            ICategoryRepo repo = new SQLCategoryRepo();

            int count = repo.Caterogies.Count;
            repo.SaveCategory(category);
            int finalCount = repo.Caterogies.Count;

            Assert.That(count + 1, Is.EqualTo(finalCount));
        }

        [Test]
        public void DataFromDbIsEqual()
        {
            ICategory category = SQLRepoTestsHelper.CreateValidCategory();
            ICategoryRepo repo = new SQLCategoryRepo();

            repo.SaveCategory(category);
            var dbCategory = repo.Caterogies.First(p => p.ID == category.ID);

            var res = SQLRepoTestsHelper.Compare(dbCategory, category);

            Assert.That(res, Is.True);
        }

        [Test]
        public void CanUpdateCategory()
        {
            ICategory category = SQLRepoTestsHelper.CreateValidCategory();
            ICategoryRepo repo = new SQLCategoryRepo();

            repo.SaveCategory(category);
            Category dbCat = (Category)repo.GetCategoryByID(category.ID);
            dbCat.Description += "_Changed";
            dbCat.Name += "_Changed";

            var res = repo.SaveCategory(dbCat);

            Assert.That(res, Is.True);
        }
    }
}
