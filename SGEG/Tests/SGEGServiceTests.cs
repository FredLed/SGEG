using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGEGService;
using SService = SGEGService.SGEGService;

namespace Tests
{
    [TestFixture]
    public class SGEGServiceTests
    {
        [Test]
        public void CanCreateSGEGService()
        {
            var service = new SService();

            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void CanAddProduct()
        {
            Assert.Fail();
        }

        [Test]
        public void CanAddProductCategory()
        {
            Assert.Fail();
        }

        [Test]
        public void CanSignUp()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetAllCategories()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetAllItems()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetAllProducts()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetCategoryByID()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetItemByID()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetItemsByProduct()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetProductsByID()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetProductsByCategory()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetUser()
        {
            Assert.Fail();
        }

        [Test]
        public void CanLogin()
        {
            Assert.Fail();
        }

        [Test]
        public void CanLogout()
        {
            Assert.Fail();
        }

    }
}
