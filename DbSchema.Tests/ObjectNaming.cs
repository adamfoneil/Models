using AO.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbSchema.Tests
{
    [TestClass]
    public class ObjectNaming
    {
        [TestMethod]
        public void SampleObjectNames()
        {
            Assert.IsTrue(ObjectName.FromType(typeof(Employee)).ToString().Equals("dbo.Employee"));
            Assert.IsTrue(ObjectName.FromType(typeof(Whatever)).ToString().Equals("hoopla.Whatever"));
            Assert.IsTrue(ObjectName.FromType(typeof(UserProfile)).ToString().Equals("dbo.AspNetUsers"));
            Assert.IsTrue(ObjectName.FromType(typeof(Deleted<int>)).ToString().Equals("delete_log.Deleted_int"));
        }
    }

    internal class Employee { }

    [Schema("hoopla")]
    internal class Whatever { }

    [Table("AspNetUsers")]
    internal class UserProfile { }

    [Schema("delete_log")]
    internal class Deleted<T> { }
}
