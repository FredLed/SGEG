using SGEGService.Model;
using SGEGService.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.RepoTests
{
    public static class SQLRepoTestsHelper
    {
        #region Factories
        static public IProduct CreateValidProduct()
        {
            return new Product()
            {
                ID = Guid.NewGuid(),
                Name = "LightSaber",
                CreationDate = DateTime.Now,
                CUP = "AONDNDOASNDIONSD129812839",
                MSRP = 123123.123123,
                Description = "YOLO"
            };
        }

        static public IUser CreateValidUser()
        {
            return new User()
            {
                ID = Guid.NewGuid(),
                Address = "098 The deathstar",
                CreationDate = DateTime.Now,
                Email = "lordVaderLover@hothothot.force",
                Password = "somethin",
                UserName = "ABC"
            };
        }

        static public ICategory CreateValidCategory()
        {
            return new Category()
            {
                ID = Guid.NewGuid(),
                Name = "Weapons",
                Description = "Pow pow"
            };
        }
        #endregion


        #region Compare
        static public bool Compare(IProduct data1, IProduct data2)
        {
            return false;
        }

        static public bool Compare(IUser data1, IUser data2)
        {
            return false;
        }

        static public bool Compare(ICategory data1, ICategory data2)
        {
            return false; //TODO
        }
        #endregion
    }
}
