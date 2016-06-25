﻿using System;
using System.Text;
using NUnit.Framework;
using PylonLog.Core;



namespace PylonLog.UnitTest
{
    [TestFixture]
    class LogSessionTest
    {
        [Test]
        public void TestPopulateNameFromData()
        {

            string expectedPlaneName = "TestPlane";

            LogSession logSession = new LogSession();

            logSession.mainHeader = Encoding.ASCII.GetBytes("012345678901TestPlane\0");

            logSession.poplulateNameFromMainHeader();

            Assert.AreEqual(expectedPlaneName, logSession.planeName);
        }
    }
}