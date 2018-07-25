﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilitySharp.Tests
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void CleanAndConvert_Int32()
        {
            var expected = 7510;
            var actual = StringHelper.CleanAndConvert<Int32>("Hmmm... I would give it a 7.5 out of 10.");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanAndConvert_DateTime()
        {
            var expected = new DateTime(2018, 7, 24, 1, 26, 0);
            var actual = StringHelper.CleanAndConvert<DateTime>("The date is 24/07/2018 01:26!", "([0-9]{2}/[0-9]{2}/[0-9]{4}\\s[0-9]{2}:[0-9]{2})", Enums.RegexAction.Select, "dd/MM/yyyy HH:mm");
            Assert.AreEqual(expected, actual);
        }
    }
}