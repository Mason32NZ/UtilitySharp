using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilitySharp.Tests
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void CleanAndConvert_Int32()
        {
            var expected = 8;
            var actual = StringHelper.CleanAndConvert<Int32>("Hmmm... I would give it a 7.5 out of 10.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanAndConvert_Double()
        {
            var expected = 7.5;
            var actual = StringHelper.CleanAndConvert<Double>("Hmmm... I would give it a 7.5 out of 10.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanAndConvert_DateTime()
        {
            var expected = new DateTime(2018, 7, 24, 1, 26, 0);
            var actual = StringHelper.CleanAndConvert<DateTime>("The date is 24/07/2018 01:26!", "dd/MM/yyyy HH:mm");
            Assert.AreEqual(expected, actual);
        }
    }
}
