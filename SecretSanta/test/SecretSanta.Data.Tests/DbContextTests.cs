using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class DbContextTests
    {
        [TestMethod]
        public void AddGift()
        {
            DbContext dbContext = new DbContext();
            int beforeCount = dbContext.Groups.Count();
            dbContext.Groups.Add(new Group(){Id=42, Name="Colgate"});
            
            Assert.AreEqual<int>(beforeCount+1, dbContext.Groups.Count());
        }
    }
}
