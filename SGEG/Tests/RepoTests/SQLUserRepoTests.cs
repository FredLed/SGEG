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
    public class SQLUserRepoTests
    {
        [Test]
        public void CanCreateUser()
        {
            IUser user = new User();

            Assert.That(user, Is.Not.Null);
        }

        [Test]
        public void CanCreateUserRepository()
        {
            IUserRepo userRepo = new SQLUserRepo();

            Assert.That(userRepo, Is.Not.Null);
        }

        [Test]
        public void CanSaveUser()
        {
            IUser user = SQLRepoTestsHelper.CreateValidUser();
            IUserRepo repo = new SQLUserRepo();

            bool res = repo.SaveUser(user);
            Assert.That(res, Is.True);
        }

        [Test]
        public void SaveUserActuallySaves()
        {
            IUser user = SQLRepoTestsHelper.CreateValidUser();
            IUserRepo repo = new SQLUserRepo();

            int count = repo.Users.Count;
            repo.SaveUser(user);
            int finalCount = repo.Users.Count;

            Assert.That(count + 1, Is.EqualTo(finalCount));
        }

        [Test]
        public void DataFromDbIsEqual()
        {
            IUser user = SQLRepoTestsHelper.CreateValidUser();
            IUserRepo repo = new SQLUserRepo();
            
            repo.SaveUser(user);
            IUser dbUser = repo.GetUserByID(user.ID);

            var res = SQLRepoTestsHelper.Compare(user, dbUser);

            Assert.That(res, Is.True);
        }

        [Test]
        public void CanUpdateUser()
        {
            IUser user = SQLRepoTestsHelper.CreateValidUser();
            IUserRepo repo = new SQLUserRepo();

            repo.SaveUser(user);
            User dbUser = (User)repo.GetUserByID(user.ID);
            dbUser.Email += "_Changed";
            dbUser.Address += "_Changed";

            var res = repo.SaveUser(dbUser);

            Assert.That(res, Is.True);
        }
    }
}
