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
    class SQLProductRepoTests
    {
        [Test]
        public void CanCreateProduct()
        {
            IProduct product = new Product();

            Assert.That(product, Is.Not.Null);
        }

        [Test]
        public void CanCreateProductRepository()
        {
            IProductRepo productRepo = new SQLProductRepo();

            Assert.That(productRepo, Is.Not.Null);
        }

        [Test]
        public void CanSaveProduct()
        {
            IProduct product = SQLRepoTestsHelper.CreateValidProduct();
            IProductRepo repo = new SQLProductRepo();

            bool res = repo.SaveProduct(product);
            Assert.That(res, Is.True);
        }

        [Test]
        public void SaveProductActuallySavesIntoDb()
        {
            IProduct product = SQLRepoTestsHelper.CreateValidProduct();
            IProductRepo repo = new SQLProductRepo();

            int count = repo.Products.Count;
            repo.SaveProduct(product);
            int finalCount = repo.Products.Count;

            Assert.That(count + 1, Is.EqualTo(finalCount));
        }

        [Test]
        public void DataFromDbIsEqual()
        {
            IProduct product = SQLRepoTestsHelper.CreateValidProduct();
            IProductRepo repo = new SQLProductRepo();

            repo.SaveProduct(product);
            var dbProduct = repo.Products.First(p => p.ID == product.ID);

            var res = SQLRepoTestsHelper.Compare(dbProduct, product);

            Assert.That(res, Is.True);
        }

    }
}
